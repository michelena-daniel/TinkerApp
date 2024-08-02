using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinkerAppProject.Models.Charting;

namespace TinkerAppProject.Controllers
{
    [Authorize]
    public class ChartingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GenerateChart(ChartModel model)
        {
            return View(model);
        }
    }
}
