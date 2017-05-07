using System;
using MoneyKeeper.Core.Entities;

namespace MoneyKeeper.BusinessLogic.Dto.Statistic
{
    public class GeneralFinOperationModel
    {
        public double Value { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public FinOperationType Type { get; set; }
    }
}