using System;

namespace MoneyKeeper.Mobile.Android.DataAccess
{
    public class FinOperation
    {
        public long Id { get; set; }

        public double Value { get; set; }

        public DateTime Date { get; set; }

        public FinOperationType Type { get; set; }
    }
}