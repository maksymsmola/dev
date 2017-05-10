using System;
using MoneyKeeper.Core.Entities;

namespace MoneyKeeper.BusinessLogic.Dto.Synchronization.FinOperation
{
    public class FinOperationSyncDto
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public double Value { get; set; }

        public string Description { get; set; }

        public FinOperationType Type { get; set; }

        public EntityState State { get; set; }
    }
}