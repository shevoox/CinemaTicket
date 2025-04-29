using CinemaTicket.Models;

namespace CinemaTicket.ViewModels
{
    public class ViewModelForMovieAndScreening
    {
        public List<Movie> Movies { get; set; }
        public List<Screening> Screening { get; set; }
    }
}
