using System;
using System.Collections.Generic;
using System.Linq;
using MoneyKeeper.BusinessLogic.Dto.Synchronization;
using MoneyKeeper.BusinessLogic.Dto.Synchronization.FinOperation;
using MoneyKeeper.BusinessLogic.Mappings;
using MoneyKeeper.Core.Entities;
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

            SyncResponse syncResponse = new SyncResponse();

            this.SyncAddedOnClientToServer(dataFromClient.UserId, dataFromClient.AddedFinOperations);
            syncResponse.AddedFinOperations
                = this.SyncAddedOnServerToClient(modifiedFinOperationsOnServer.Where(x => x.State == EntityState.Added).ToList());

            throw new NotImplementedException();
        }

        private List<FinOperationSyncDto> SyncAddedOnServerToClient(List<FinancialOperation> addedOnServer)
        {
            List<FinOperationSyncDto> goesToClient = new List<FinOperationSyncDto>();
            foreach (var finOperation in addedOnServer)
            {
                finOperation.State = EntityState.Synchronized;
                goesToClient.Add(finOperation.ToFinOperationSyncDto());
            }

            return goesToClient;
        }

        private void SyncAddedOnClientToServer(long userId, List<FinOperationSyncDto> addedOnClient)
        {
            var toAddFromClient = addedOnClient.Select(x => x.ToFinOperationSyncDto(userId)).ToList();
            this.repository.AddRange(toAddFromClient);
        }

        private List<long> SyncDeletedOnServerToClient(
            List<FinancialOperation> deletedOnServer,
            List<FinOperationSyncDto> deletedOnCLient)
        {
            throw new NotImplementedException();
        }

        private List<long> SyncDeletedOnClientoServer(
            List<FinancialOperation> deletedOnServer,
            List<FinOperationSyncDto> deletedOnCLient)
        {
            throw new NotImplementedException();
        }

        private List<FinOperationSyncDto> SyncUpdatedOnServerToClient(
            List<FinancialOperation> updatedOnServer,
            List<FinOperationSyncDto> updatedOnCLient)
        {
            throw new NotImplementedException();
        }

        private List<long> SyncUpdatedOnClientToServer(
            List<FinancialOperation> updatedOnServer,
            List<FinOperationSyncDto> updatedOnCLient)
        {
            throw new NotImplementedException();
        }
    }
}