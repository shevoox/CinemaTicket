using CinemaTicket.Data;
using CinemaTicket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicket.Controllers.Admin
{
    public class CinemasController : Controller
    {
        private readonly ApplicationDBContext _context = new();



        public IActionResult Index()
        {
            var screenings = _context.Screenings
                                     .Include(s => s.Movie)
                                     .ToList();
            return View(screenings);
        }


        public IActionResult Details(int id)
        {
            var screening = _context.Screenings
                                    .Include(s => s.Movie)
                                    .FirstOrDefault(s => s.Id == id);

            if (screening == null)
                return NotFound();

            return View(screening);
        }


        public IActionResult Create()
        {
            Screening screening = new Screening();
            ViewBag.Movies = _context.Movies.ToList();
            return View(screening);
        }

        [HttpPost]
        public IActionResult Create(Screening screening)
        {
            if (ModelState.IsValid)
            {
                _context.Screenings.Add(screening);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(screening);
        }


        public IActionResult Edit(int id)
        {
            var screening = _context.Screenings.Find(id);
            if (screening == null)
                return NotFound();
            ViewBag.Movies = _context.Movies.ToList();
            return View(screening);
        }

        [HttpPost]
        public IActionResult Edit(Screening screening)
        {
            if (ModelState.IsValid)
            {
                _context.Screenings.Update(screening);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(screening);
        }


        public IActionResult Delete(int id)
        {
            var screening = _context.Screenings.Find(id);
            if (screening == null)
                return NotFound();

            _context.Screenings.Remove(screening);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
