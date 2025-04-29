using CinemaTicket.Data;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicket.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDBContext _dbContext = new();
        public IActionResult Index()
        {
            var categorie = _dbContext.Movies;
            return View(categorie.ToList());
        }
    }
}
