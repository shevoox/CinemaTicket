namespace CinemaTicket.Models
{
	public class Screening
	{
		public int Id { get; set; }
		public int MovieId { get; set; }        // رقم الفيلم
		public Movie Movie { get; set; }        // الفيلم نفسه
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public string HallNumber { get; set; }   // رقم القاعة
		public int SeatNumber { get; set; }   // عدد الكراسي
		public bool IsAvailable { get; set; }    // هل العرض متاح أم لا
	}
}
