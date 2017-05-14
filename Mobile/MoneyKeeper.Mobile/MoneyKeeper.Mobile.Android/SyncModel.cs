using System.Collections.Generic;
using MoneyKeeper.Mobile.Android.DataAccess;

namespace MoneyKeeper.Mobile.Android
{
    public class SyncModel
    {
        public List<long> DeletedFinOperationsIds { get; set; }

        public List<FinOperation> AddedFinOperations { get; set; }

        public List<FinOperation> UpdatedFinOperations { get; set; }

        public SyncModel()
        {
            this.DeletedFinOperationsIds = new List<long>();
            this.AddedFinOperations = new List<FinOperation>();
            this.UpdatedFinOperations = new List<FinOperation>();
        }
    }
}