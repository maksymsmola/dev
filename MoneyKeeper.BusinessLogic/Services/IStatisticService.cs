using System.Collections.Generic;
using MoneyKeeper.BusinessLogic.Dto.Statistic;
using MoneyKeeper.BusinessLogic.Services.Implementations;

namespace MoneyKeeper.BusinessLogic.Services
{
    public interface IStatisticService
    {
        List<MonthlyReportItemModel> GetGeneralMonthlyStatistic(long userId);

        CategoryReportModel GetReportByCategories(long userId);
    }
}