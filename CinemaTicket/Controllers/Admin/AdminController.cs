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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ViewModelForMovieAndScreening vm, IFormFile ImageUrl, IFormFile Image2Url)
        {
            if (!ModelState.IsValid)
            {
                vm.Genres = GetGenres(); // عشان يرجع القائمة لو فيه خطأ
                return View(vm);
            }

            // حفظ الصور
            string img1Path = "";
            string img2Path = "";

            if (ImageUrl != null && ImageUrl.Length > 0)
            {
                var img1FileName = Guid.NewGuid() + Path.GetExtension(ImageUrl.FileName);
                var img1SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", img1FileName);
                using (var stream = new FileStream(img1SavePath, FileMode.Create))
                {
                    await ImageUrl.CopyToAsync(stream);
                }
                img1Path = "/images/" + img1FileName;
            }

            if (Image2Url != null && Image2Url.Length > 0)
            {
                var img2FileName = Guid.NewGuid() + Path.GetExtension(Image2Url.FileName);
                var img2SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", img2FileName);
                using (var stream = new FileStream(img2SavePath, FileMode.Create))
                {
                    await Image2Url.CopyToAsync(stream);
                }
                img2Path = "/images/" + img2FileName;
            }

            // إنشاء Movie
            var movie = new Movie
            {
                Title = vm.Title,
                Description = vm.Description,
                Genre = vm.Genre,
                Duration = vm.Duration,
                ImageUrl = img1Path,
                Image2Url = img2Path,
                ReleaseDate = vm.ReleaseDate,
                Price = vm.Price,
                Trafic = vm.Trafic,
                Screenings = new List<Screening>()
            };

            // إنشاء Screening
            var screening = new Screening
            {
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                HallNumber = vm.HallNumber,
                SeatNumber = vm.SeatNumber,
                IsAvailable = vm.IsAvailable,
                Movie = movie
            };

            movie.Screenings.Add(screening);

            _dbContext.Movies.Add(movie);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        private List<SelectListItem> GetGenres()
        {
            return _dbContext.Movies
                .Select(m => m.Genre)
                .Distinct()
                .OrderBy(g => g)
                .Select(g => new SelectListItem
                {
                    Text = g,
                    Value = g
                }).ToList();
        }



        public IActionResult Edit(int id)
        {
            var Movie = _dbContext.Movies.Find(id);
            ViewBag.Genres = _dbContext.Movies.Select(e => e.Genre).Distinct().ToList();

            return View(Movie);
        }
        [HttpPost]
        public IActionResult Edit(Movie movie)
        {
            _dbContext.Update(movie);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Admin));
        }

        public IActionResult Delete(int id)
        {
            var Movie = _dbContext.Movies.Find(id);
            _dbContext.Remove(Movie);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Admin));
        }
    }
}
