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
        private readonly UserManager<TinkerAppProjectUser> _userManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(UserManager<TinkerAppProjectUser> userManager, ILogger<HomeController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                if (user.Name == null)
                {
                    user.Name = user.UserName;
                }
                return View(user);
            }
            catch
            {
                return View();
            }
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
