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

        public IActionResult Index(string? SearchResult, string Genre, int page = 1)
        {
            int PageSize = 5;

            var query = _dbContext.Movies.Include(e => e.Screenings).AsQueryable();

            if (!string.IsNullOrEmpty(SearchResult))
            {
                query = query.Where(m => m.Title.Contains(SearchResult));
            }
            if (!string.IsNullOrEmpty(Genre))
            {
                query = query.Where(m => m.Genre.Contains(Genre));
            }



            /* pagination */
            var totalMovies = query.Count();
            var AllMovies = query
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();


            ViewBag.NoResults = !AllMovies.Any();
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalMovies / PageSize);
            ViewBag.CurrentPage = page;
            ViewBag.SearchResult = SearchResult;
            ViewBag.Genre = Genre;
            ViewBag.Traffic = _dbContext.Movies.OrderByDescending(e => e.Trafic).FirstOrDefault();



            return View(AllMovies);
        }

        [HttpPost]
        public IActionResult IncreaseTraffic(int movieId)
        {

            var movie = _dbContext.Movies.FirstOrDefault(m => m.Id == movieId);

            if (movie != null)
            {

                movie.Trafic += 1;


                _dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var ModelFromController = _dbContext.Movies.Include(e => e.Screenings).Include(e => e.MovieCrews).FirstOrDefault(e => e.Id == id);
            return View(ModelFromController);
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
