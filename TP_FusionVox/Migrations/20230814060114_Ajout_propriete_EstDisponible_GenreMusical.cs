using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP_FusionVox.Migrations
{
    public partial class Ajout_propriete_EstDisponible_GenreMusical : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EstDisponible",
                table: "genresMusicaux",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "genresMusicaux",
                keyColumn: "Id",
                keyValue: 1,
                column: "EstDisponible",
                value: true);

            migrationBuilder.UpdateData(
                table: "genresMusicaux",
                keyColumn: "Id",
                keyValue: 2,
                column: "EstDisponible",
                value: true);

            migrationBuilder.UpdateData(
                table: "genresMusicaux",
                keyColumn: "Id",
                keyValue: 3,
                column: "EstDisponible",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstDisponible",
                table: "genresMusicaux");
        }
    }
}
