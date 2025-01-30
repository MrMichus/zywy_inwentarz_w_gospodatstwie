using System.Diagnostics;
using Inwentarz.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Inwentarz.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly InwentarzDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, InwentarzDbContext inwentarzDbContext,
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _dbContext = inwentarzDbContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            // Pobieramy id zalogowanego u¿ytkownika
            var userId = _userManager.GetUserId(User);

            // Pobieramy stanowisko z bazy danych
            var stanowisko = _dbContext.Pracownik
                .Where(p => p.ApplicationUserId == userId)
                .Select(p => p.Stanowisko)
                .FirstOrDefault();

            // Przekazujemy stanowisko do widoku
            ViewBag.Stanowisko = stanowisko;

            return View();
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
