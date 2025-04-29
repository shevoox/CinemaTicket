using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaTicket.Migrations
{
    /// <inheritdoc />
    public partial class AddTraficToMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Trafic",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Trafic",
                table: "Movies");
        }
    }
}
