using System.Collections.Generic;

namespace MoneyKeeper.BusinessLogic.Dto.Statistic
{
    public class CategoryReportModel
    {
        public List<CategoryReportItem> Expenses { get; set; }

        public List<CategoryReportItem> Incomes { get; set; }

        public CategoryReportModel()
        {
            this.Expenses = new List<CategoryReportItem>();
            this.Incomes = new List<CategoryReportItem>();
        }
    }
}