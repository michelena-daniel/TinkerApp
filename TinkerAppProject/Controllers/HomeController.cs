using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TinkerAppProject.Areas.Identity.Data;
using TinkerAppProject.Models;

namespace TinkerAppProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private UserManager<TinkerAppProjectUser> _userManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(UserManager<TinkerAppProjectUser> userManager, ILogger<HomeController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
