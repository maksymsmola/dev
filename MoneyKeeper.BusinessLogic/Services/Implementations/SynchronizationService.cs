using System.Collections.Generic;
using System.Linq;
using MoneyKeeper.BusinessLogic.Dto.Synchronization;
using MoneyKeeper.BusinessLogic.Dto.Synchronization.FinOperation;
using MoneyKeeper.BusinessLogic.Mappings;
using MoneyKeeper.Core.Entities;
using MoneyKeeper.Core.Extensions;
using MoneyKeeper.DataAccess.Repository;

namespace MoneyKeeper.BusinessLogic.Services.Implementations
{
    public class SynchronizationService : ISynchronizationService
    {
        private readonly IRepository repository;

        public SynchronizationService(IRepository repository)
        {
            this.repository = repository;
        }

        public SyncResponse SynchronizeData(SyncRequest dataFromClient)
        {
            List<FinancialOperation> modifiedFinOperationsOnServer
                = this.repository.GetByCriteria<FinancialOperation>(
                    x => x.UserId == dataFromClient.UserId && x.State != EntityState.Synchronized);

            var syncResponse = new SyncResponse();

            List<FinOperationSyncDto> toAddFromServer
                = this.SyncAddedOnServerToClient(modifiedFinOperationsOnServer.Where(x => x.State == EntityState.Added).ToList());
            List<FinancialOperation> toAddFromClient = this.SyncAddedOnClientToServer(dataFromClient.UserId, dataFromClient.AddedFinOperations);

            this.repository.SaveChanges();

            syncResponse.AddedFinOperations = toAddFromServer.Union(toAddFromClient.Select(x => x.ToFinOperationSyncDto())).ToList();

            List<FinOperationSyncDto> updatedOnServer
                = this.SyncUpdatedOnServerToClient(modifiedFinOperationsOnServer.Where(x => x.State == EntityState.Deleted).ToList());

            List<long> deltedIdsOnServer
                = this.SyncDeletedOnServerToClient(modifiedFinOperationsOnServer.Where(x => x.State == EntityState.Deleted).ToList());

            List<long> updatedOnServerIds = updatedOnServer.Select(x => x.Id).ToList();
            List<long> updatedOrDeletedIdsOnServer = deltedIdsOnServer.Union(updatedOnServerIds).ToList();

            List<long> toDeleteOnClientIds
                = dataFromClient.DeletedFinOperationsIds.Where(x => !updatedOrDeletedIdsOnServer.Contains(x)).ToList();
            syncResponse.DeletedFinOperationsIds = toDeleteOnClientIds;
            this.SyncDeletedOnClientoServer(toDeleteOnClientIds);

            List<FinOperationSyncDto> toUpdateFromClient
                = dataFromClient.UpdatedFinOperations.Where(x => !updatedOrDeletedIdsOnServer.Contains(x.Id)).ToList();
            this.SyncUpdatedOnClientToServer(toUpdateFromClient);

            syncResponse.UpdatedFinOperations = toUpdateFromClient.Union(updatedOnServer).ToList();

            this.repository.SaveChanges();

            return syncResponse;
        }

        private List<FinOperationSyncDto> SyncAddedOnServerToClient(List<FinancialOperation> addedOnServer)
        {
            var goesToClient = new List<FinOperationSyncDto>();
            foreach (FinancialOperation finOperation in addedOnServer)
            {
                finOperation.State = EntityState.Synchronized;
                goesToClient.Add(finOperation.ToFinOperationSyncDto());
            }

            return goesToClient;
        }

        private List<FinancialOperation> SyncAddedOnClientToServer(long userId, List<FinOperationSyncDto> addedOnClient)
        {
            List<FinancialOperation> toAddFromClient = addedOnClient.Select(x => x.ToFinOperationSyncDto(userId)).ToList();
            this.repository.AddRange(toAddFromClient);

            return toAddFromClient;
        }

        private List<long> SyncDeletedOnServerToClient(List<FinancialOperation> deletedOnServer)
        {
            var deletedIds = new List<long>(deletedOnServer.Count);
            foreach (FinancialOperation operation in deletedOnServer)
            {
                operation.State = EntityState.Synchronized;
                deletedIds.Add(operation.Id);
            }

            return deletedIds;
        }

        // todo: SaveChanges() ?
        private void SyncDeletedOnClientoServer(List<long> deletedOnClientIds)
        {
            foreach (long id in deletedOnClientIds)
            {
                this.repository.DeleteById<FinancialOperation>(id);
            }
        }

        private List<FinOperationSyncDto> SyncUpdatedOnServerToClient(List<FinancialOperation> updatedOnServer)
        {
            var goesToClient = new List<FinOperationSyncDto>(updatedOnServer.Count);
            foreach (FinancialOperation operation in updatedOnServer)
            {
                operation.State = EntityState.Synchronized;
                goesToClient.Add(operation.ToFinOperationSyncDto());
            }

            return goesToClient;
        }

        // todo: SaveChanges() ?
        private void SyncUpdatedOnClientToServer(List<FinOperationSyncDto> updatedOnClient)
        {
            List<long> ids = updatedOnClient.SelectList(x => x.Id);
            List<FinancialOperation> operationsToUpdate
                = this.repository
                    .GetByCriteria<FinancialOperation>(x => ids.Contains(x.Id))
                    .OrderBy(x => x.Id)
                    .ToList();

            updatedOnClient = updatedOnClient.OrderBy(x => x.Id).ToList();

            for (int i = 0; i < updatedOnClient.Count; i++)
            {
                FinancialOperation currentOnServer = operationsToUpdate[i];
                FinOperationSyncDto currentOnClient = updatedOnClient[i];

                currentOnServer.Value = currentOnClient.Value;
                currentOnClient.Date = currentOnClient.Date;
                currentOnClient.Description = currentOnClient.Description;
            }
        }
    }
}