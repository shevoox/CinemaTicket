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
        private readonly ILogger<AdminController> _logger;
        private readonly ApplicationDBContext _dbContext = new();
        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;

        }
        public IActionResult Admin()
        {
            var Movies = _dbContext.Movies.Include(e => e.Screenings);
            return View(Movies.ToList());
        }

        public IActionResult Create()
        {
            var viewModel = new ViewModelForMovieAndScreening
            {
                Genres = GetGenres()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ViewModelForMovieAndScreening vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Genres = GetGenres();
                return View(vm);
            }


            string img1Path = "";
            string img2Path = "";

            if (vm.ImageUrl != null && vm.ImageUrl.Length > 0)
            {
                var img1FileName = Guid.NewGuid() + Path.GetExtension(vm.ImageUrl.FileName);
                var img1SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", img1FileName);

                //try
                //{
                using (var stream = new FileStream(img1SavePath, FileMode.Create))
                {
                    await vm.ImageUrl.CopyToAsync(stream);
                }
                img1Path = img1FileName;
                //}
                //catch (Exception ex)
                //{
                //    _logger.LogError(ex, "حدث خطأ أثناء رفع الصورة.");
                //    ModelState.AddModelError("", "حدث خطأ أثناء رفع الصورة: " + ex.Message);
                //    vm.Genres = GetGenres();
                //    return View(vm);
                //}
            }

            if (vm.Image2Url != null && vm.Image2Url.Length > 0)
            {
                var img2FileName = Guid.NewGuid() + Path.GetExtension(vm.Image2Url.FileName);
                var img2SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", img2FileName);
                using (var stream = new FileStream(img2SavePath, FileMode.Create))
                {
                    await vm.Image2Url.CopyToAsync(stream);
                }
                img2Path = img2FileName;
            }


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

            return RedirectToAction(nameof(Admin));
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
            if (Movie == null)
                return NotFound();

            ViewBag.Genres = _dbContext.Movies.Select(e => e.Genre).Distinct().ToList();
            return View(Movie);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Movie model, IFormFile ImageUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var movie = await _dbContext.Movies.FindAsync(model.Id);
            if (movie == null) return NotFound();


            movie.Title = model.Title;
            movie.Description = model.Description;
            movie.Genre = model.Genre;
            movie.Duration = model.Duration;
            movie.ReleaseDate = model.ReleaseDate;
            movie.Price = model.Price;
            movie.Image2Url = model.Image2Url;


            if (ImageUrl != null && ImageUrl.Length > 0)
            {

                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageUrl.FileName);
                var filePath = Path.Combine(uploads, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageUrl.CopyToAsync(stream);
                }

                movie.ImageUrl = "/images/" + uniqueFileName;
            }

            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Admin");
        }

        public IActionResult ReplaceImage(int id)
        {
            var movie = _dbContext.Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }


        public IActionResult DeleteImage(int id)
        {
            var movie = _dbContext.Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            movie.ImageUrl = null;
            _dbContext.Update(movie);
            _dbContext.SaveChanges();

            return RedirectToAction("Edit", new { id = id });
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            var Movie = _dbContext.Movies.Find(id);
            var Screen = _dbContext.Screenings.Find(id);
            if (Movie != null && Screen != null)
            {
                var oldFile = Movie.ImageUrl;
                var old2File = Movie.Image2Url;
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", oldFile);
                var old2Path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", old2File);
                if (System.IO.File.Exists(oldPath) && System.IO.File.Exists(old2Path))
                {
                    System.IO.File.Delete(oldPath);
                    System.IO.File.Delete(old2Path);
                }
                _dbContext.Remove(Screen);
                _dbContext.Remove(Movie);
                _dbContext.SaveChanges();
            }

            return RedirectToAction(nameof(Admin));
        }
    }
}
