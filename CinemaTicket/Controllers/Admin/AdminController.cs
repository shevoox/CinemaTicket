using CinemaTicket.Data;
using CinemaTicket.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicket.Controllers.Admin
{
    public class AdminController : Controller
    {
        private readonly ApplicationDBContext _dbContext = new();
        public IActionResult Admin()
        {
            var Movies = _dbContext.Movies.Include(e => e.Screenings);
            return View(Movies.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ViewModelForMovieAndScreening viewModel)
        {
            return View();
        }
    }
}
