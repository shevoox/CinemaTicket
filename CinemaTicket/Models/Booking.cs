namespace CinemaTicket.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public int ScreeningId { get; set; }
        public Screening Screening { get; set; }
        public List<Ticket> Tickets { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime BookingTime { get; set; }
        public string BookingStatus { get; set; }  // e.g., "Confirmed", "Cancelled", "Pending"
    }
}
