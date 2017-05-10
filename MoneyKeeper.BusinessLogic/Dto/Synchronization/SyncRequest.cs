using System.Collections.Generic;
using MoneyKeeper.BusinessLogic.Dto.Synchronization.FinOperation;

namespace MoneyKeeper.BusinessLogic.Dto.Synchronization
{
    public class SyncRequest
    {
        public long UserId { get; set; }

        public List<FinOperationSyncDto> AddedFinOperations { get; set; }

        public List<FinOperationSyncDto> UpdatedFinOperations { get; set; }

        public List<FinOperationSyncDto> DeletedFinOperations { get; set; }

        public SyncRequest()
        {
            this.AddedFinOperations = new List<FinOperationSyncDto>();
            this.UpdatedFinOperations = new List<FinOperationSyncDto>();
            this.DeletedFinOperations = new List<FinOperationSyncDto>();
        }
    }
}