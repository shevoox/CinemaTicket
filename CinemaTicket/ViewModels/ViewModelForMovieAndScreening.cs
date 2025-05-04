using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CinemaTicket.ViewModels
{

    public class ViewModelForMovieAndScreening
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please select a genre")]
        public string Genre { get; set; }

        [Range(1, 500)]
        public int Duration { get; set; }

        public IFormFile ImageUrl { get; set; }
        public IFormFile Image2Url { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Range(1, 1000)]
        public decimal Price { get; set; }

        public int Trafic { get; set; } = 0;

        public List<SelectListItem> Genres { get; set; } = [];

        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        [Required]
        public string HallNumber { get; set; }

        [Range(1, 500)]
        public int SeatNumber { get; set; }

        public bool IsAvailable { get; set; }
    }

}
