using CinemaTicket.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CinemaTicket.ViewModels
{

    public class ViewModelForMovieAndScreening
    {
        public List<Movie> Movies { get; set; }
        public List<Screening> Screening { get; set; }

        // Movie Properties
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public int Duration { get; set; }
        public string ImageUrl { get; set; }
        public string Image2Url { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public int Trafic { get; set; }
        public List<SelectListItem> Genres { get; set; }
        // Screening Properties
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string HallNumber { get; set; }
        public int SeatNumber { get; set; }
        public bool IsAvailable { get; set; }
    }
}
