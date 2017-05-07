using System;
using System.Collections.Generic;
using System.Linq;
using MoneyKeeper.BusinessLogic.Dto.Statistic;
using MoneyKeeper.BusinessLogic.Mappings;
using MoneyKeeper.Core;
using MoneyKeeper.Core.Entities;
using MoneyKeeper.DataAccess.Repository;

namespace MoneyKeeper.BusinessLogic.Services.Implementations
{
    public class StatisticService : IStatisticService
    {
        private readonly IRepository repository;

        public StatisticService(IRepository repository)
        {
            this.repository = repository;
        }

        public List<MonthlyReportItemModel> GetGeneralMonthlyStatistic(long userId)
        {
            var monthAgo = DateTime.Now.AddMonths(-1);

            List<FinancialOperation> operationsForMonth
                = this.repository
                    .GetByCriteriaIncluding<FinancialOperation>(
                        x => x.UserId == userId && x.Date >= monthAgo,
                        x => x.Category)
                    .ToList();

            if (operationsForMonth.Count == 0)
            {
                return new List<MonthlyReportItemModel>();
            }

            var expensesForMonth = operationsForMonth
                .Where(x => x.Type == FinOperationType.Expense)
                .GroupBy(x => x.Date.Date)
                .ToArray();
            var incomesForMonth = operationsForMonth
                .Where(x => x.Type == FinOperationType.Income)
                .GroupBy(x => x.Date.Date)
                .ToArray();

            DateTime minDate = operationsForMonth.Min(x => x.Date);
            DateTime maxDate = operationsForMonth.Max(x => x.Date);

            var reportItems = new List<MonthlyReportItemModel>();
            for (DateTime i = minDate; i <= maxDate; i = i.AddDays(1))
            {
                DateTime currentDate = i.Date;
                double expenseValue
                    = expensesForMonth.FirstOrDefault(x => x.Key == currentDate)?.Sum(x => x.Value) ?? 0d;
                double incomeValue
                    = incomesForMonth.FirstOrDefault(x => x.Key == currentDate)?.Sum(x => x.Value) ?? 0d;

                reportItems.Add(new MonthlyReportItemModel(currentDate, expenseValue, incomeValue));
            }

            return reportItems;
        }

        public CategoryReportModel GetReportByCategories(long userId)
        {
            List<FinancialOperation> allOperations = this.repository.GetByCriteria<FinancialOperation>(x => x.UserId == userId);

            List<FinancialOperation> allExpenses = allOperations.Where(x => x.Type == FinOperationType.Expense).ToList();
            List<FinancialOperation> allIncomes = allOperations.Where(x => x.Type == FinOperationType.Income).ToList();

            return new CategoryReportModel
            {
                Expenses = this.GetDataForCategoriesReport(allExpenses),
                Incomes = this.GetDataForCategoriesReport(allIncomes)
            };
        }

        private List<CategoryReportItem> GetDataForCategoriesReport(List<FinancialOperation> operations)
        {
            var groupedOperations = operations.GroupBy(x => x.Category).Select(x => new
            {
                CategoryName = x.Key?.Name ?? Constants.NO_CATEGORY,
                Amount = x.Sum(finOp => finOp.Value)
            }).ToList();

            double totalOperationsAmount = groupedOperations.Sum(x => x.Amount);

            return groupedOperations.Select(x => new CategoryReportItem
            {
                CategoryName = x.CategoryName,
                Amount = x.Amount,
                Percent = Math.Truncate(((x.Amount * 100) / totalOperationsAmount) * 10) / 10
            }).ToList();
        }
    }
}