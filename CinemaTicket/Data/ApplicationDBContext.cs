using CinemaTicket.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicket.Data
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Screening> Screenings { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.;Database=CinemaTicketDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // تكوين العلاقات بين الجداول
            modelBuilder.Entity<Screening>()
    .HasOne(s => s.Movie)
    .WithMany(m => m.Screenings)
    .HasForeignKey(s => s.MovieId)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Screening)
                .WithMany()
                .HasForeignKey(t => t.ScreeningId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Screening)
                .WithMany()
                .HasForeignKey(b => b.ScreeningId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasMany(b => b.Tickets)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}


