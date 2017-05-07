using System;

namespace MoneyKeeper.BusinessLogic.Dto.Statistic
{
    public class MonthlyReportItemModel
    {
        public DateTime Date { get; set; }

        public double ExpenseValue { get; set; }

        public double IncomeValue { get; set; }

        public MonthlyReportItemModel(DateTime date, double expenseValue, double incomeValue)
        {
            this.Date = date;
            this.ExpenseValue = expenseValue;
            this.IncomeValue = incomeValue;
        }
    }
}