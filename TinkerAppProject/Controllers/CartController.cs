using Microsoft.AspNetCore.Mvc;

namespace TinkerAppProject.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
