namespace CinemaTicket.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public int Duration { get; set; }  // in minutes
        public string ImageUrl { get; set; }
        public string Image2Url { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public int? Trafic { get; set; } = 0;
        public ICollection<Screening> Screenings { get; set; }
        public ICollection<MovieCrew> MovieCrews { get; set; }


    }
}