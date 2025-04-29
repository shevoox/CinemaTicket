namespace CinemaTicket.Models
{
	public class MovieCrew
	{
		public int Id { get; set; }

		public string Name { get; set; }  // اسم الممثل أو عضو الطاقم
		public string Role { get; set; }  // دوره (مثل: Actor, Director, Producer)

		// ربطه مع الفيلم
		public int MovieId { get; set; }
		public Movie Movie { get; set; }  // علاقة One-to-Many: فيلم واحد له كذا ممثل

		// ممكن تضيف خصائص إضافية زي:
		public string ImageUrl { get; set; }  // صورة الممثل أو العضو (اختياري)
	}
}
