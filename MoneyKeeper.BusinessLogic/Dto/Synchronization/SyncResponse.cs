using System.Collections.Generic;
using MoneyKeeper.BusinessLogic.Dto.Synchronization.FinOperation;

namespace MoneyKeeper.BusinessLogic.Dto.Synchronization
{
    public class SyncResponse
    {
        public List<FinOperationSyncDto> AddedFinOperations { get; set; }

        public List<FinOperationSyncDto> UpdatedFinOperations { get; set; }

        public List<long> DeletedFinOperationsIds { get; set; }

        public SyncResponse()
        {
            this.AddedFinOperations = new List<FinOperationSyncDto>();
            this.UpdatedFinOperations = new List<FinOperationSyncDto>();
            this.DeletedFinOperationsIds = new List<long>();
        }
    }
}