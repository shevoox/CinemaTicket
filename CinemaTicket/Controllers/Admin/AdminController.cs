using CinemaTicket.Data;
using CinemaTicket.Models;
using CinemaTicket.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var genres = _dbContext.Movies
                    .Select(m => m.Genre)
                    .Distinct()
                    .ToList();

            var viewModel = new ViewModelForMovieAndScreening
            {
                Genres = genres.Select(g => new SelectListItem
                {
                    Value = g,
                    Text = g
                }).ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Create(ViewModelForMovieAndScreening viewModel)
        {
            if (!ModelState.IsValid)
            {

                viewModel.Genres = _dbContext.Movies
                    .Select(m => m.Genre)
                    .Distinct()
                    .Select(g => new SelectListItem { Value = g, Text = g })
                    .ToList();

                return View(viewModel);
            }


            var movie = new Movie
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                Genre = viewModel.Genre,
                Duration = viewModel.Duration,
                ImageUrl = viewModel.ImageUrl,
                Image2Url = viewModel.Image2Url,
                ReleaseDate = viewModel.ReleaseDate,
                Price = viewModel.Price,
                Trafic = viewModel.Trafic,
            };

            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();

            var screening = new Screening
            {
                MovieId = movie.Id,
                StartDate = viewModel.StartDate,
                EndDate = viewModel.EndDate,
                HallNumber = viewModel.HallNumber,
                SeatNumber = viewModel.SeatNumber,
                IsAvailable = viewModel.IsAvailable,
            };

            _dbContext.Screenings.Add(screening);
            _dbContext.SaveChanges();

            return RedirectToAction("Admin");
        }

    }
}
