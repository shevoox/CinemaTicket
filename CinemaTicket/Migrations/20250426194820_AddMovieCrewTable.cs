using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaTicket.Migrations
{
	/// <inheritdoc />
	public partial class AddMovieCrewTable : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{



			migrationBuilder.AddColumn<string>(
				name: "Image2Url",
				table: "Movies",
				type: "nvarchar(max)",
				nullable: false,
				defaultValue: "");

			migrationBuilder.CreateTable(
				name: "MovieCrew",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
					MovieId = table.Column<int>(type: "int", nullable: false),
					ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MovieCrew", x => x.Id);
					table.ForeignKey(
						name: "FK_MovieCrew_Movies_MovieId",
						column: x => x.MovieId,
						principalTable: "Movies",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_MovieCrew_MovieId",
				table: "MovieCrew",
				column: "MovieId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "MovieCrew");

			migrationBuilder.DropColumn(
				name: "EndDate",
				table: "Screenings");

			migrationBuilder.DropColumn(
				name: "StartDate",
				table: "Screenings");

			migrationBuilder.DropColumn(
				name: "Image2Url",
				table: "Movies");

			migrationBuilder.AddColumn<DateTime>(
				name: "ShowTime",
				table: "Screenings",
				type: "datetime2",
				nullable: false,
				defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
		}
	}
}
