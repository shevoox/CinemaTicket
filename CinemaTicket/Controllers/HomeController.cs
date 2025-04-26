using CinemaTicket.Data;
using CinemaTicket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CinemaTicket.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDBContext _dbContext = new();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int page = 1)
        {
            /* pagination */
            int PageSize = 5;
            var totalMovies = _dbContext.Movies.Count();
            var AllMovies = _dbContext.Movies.Include(e => e.Screenings).Skip((page - 1) * PageSize).Take(PageSize).ToList();
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalMovies / PageSize);
            ViewBag.CurrentPage = page;
            return View(AllMovies);
        }
        public IActionResult Details()
        {
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
