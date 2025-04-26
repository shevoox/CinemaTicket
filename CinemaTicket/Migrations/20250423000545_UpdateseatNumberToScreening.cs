using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaTicket.Migrations
{
    /// <inheritdoc />
    public partial class UpdateseatNumberToScreening : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeatNumber",
                table: "Screenings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeatNumber",
                table: "Screenings");
        }
    }
}
