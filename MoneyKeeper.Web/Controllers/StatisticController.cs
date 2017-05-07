using System.Web.Mvc;
using MoneyKeeper.BusinessLogic.Services;
using MoneyKeeper.Web.ActionResults;
using MoneyKeeper.Web.Extensions;

namespace MoneyKeeper.Web.Controllers
{
    public class StatisticController : Controller
    {
        private readonly IStatisticService statisticService;

        public StatisticController(IStatisticService statisticService)
        {
            this.statisticService = statisticService;
        }

        [HttpGet]
        public CustomJsonResult GetGeneralMonthlyReport()
        {
            long userId = this.Session.GetCurrentUserId();
            return this.CustomJson(this.statisticService.GetGeneralMonthlyStatistic(userId));
        }

        [HttpGet]
        public CustomJsonResult GetReportByCategories()
        {
            long userId = this.Session.GetCurrentUserId();
            return this.CustomJson(this.statisticService.GetReportByCategories(userId));
        }
    }
}