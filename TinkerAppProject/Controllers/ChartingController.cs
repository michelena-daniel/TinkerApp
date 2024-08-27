using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TinkerAppProject.Areas.Identity.Data;
using TinkerAppProject.Models.Charting;
using TinkerAppProject.Services.Charting;

namespace TinkerAppProject.Controllers
{
    [Authorize]
    public class ChartingController : Controller
    {
        private IChartGenerationService _chartGenerationService;
        private readonly UserManager<TinkerAppProjectUser> _userManager;

        public ChartingController(IChartGenerationService chartGenerationService, UserManager<TinkerAppProjectUser> userManager)
        {
            _chartGenerationService = chartGenerationService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GenerateChart(ChartModel model)
        {
            var userId = _userManager.GetUserId(User);
            var result = new ChartResponse();
            result = await _chartGenerationService.GenerateChartByMonths(model, userId);
            return View(result);
        }
    }
}
