using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TinkerAppProject.Controllers
{
    [Authorize]
    public class ChartingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
