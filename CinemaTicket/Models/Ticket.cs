namespace CinemaTicket.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int ScreeningId { get; set; }
        public Screening Screening { get; set; }
        public string SeatNumber { get; set; }
        public decimal Price { get; set; }
        public DateTime BookingTime { get; set; }
        public string CustomerName { get; set; }  // Instead of UserId, we'll use customer name
        public string CustomerEmail { get; set; }  // Basic customer information
        public string CustomerPhone { get; set; }  // Basic customer information
    }
}
