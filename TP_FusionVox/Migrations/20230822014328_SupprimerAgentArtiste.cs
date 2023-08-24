using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP_FusionVox.Migrations
{
    public partial class SupprimerAgentArtiste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Agent",
                table: "Artistes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Agent",
                table: "Artistes",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Artistes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Agent",
                value: "Alexei Culaxiz");

            migrationBuilder.UpdateData(
                table: "Artistes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Agent",
                value: "Trace Dempsey Cyrus");

            migrationBuilder.UpdateData(
                table: "Artistes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Agent",
                value: "Mark Ronson");

            migrationBuilder.UpdateData(
                table: "Artistes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Agent",
                value: "Jack White");

            migrationBuilder.UpdateData(
                table: "Artistes",
                keyColumn: "Id",
                keyValue: 5,
                column: "Agent",
                value: "Bassmint Production");

            migrationBuilder.UpdateData(
                table: "Artistes",
                keyColumn: "Id",
                keyValue: 6,
                column: "Agent",
                value: "Cristi Ochiu");

            migrationBuilder.UpdateData(
                table: "Artistes",
                keyColumn: "Id",
                keyValue: 7,
                column: "Agent",
                value: "Lithia Springs");

            migrationBuilder.UpdateData(
                table: "Artistes",
                keyColumn: "Id",
                keyValue: 8,
                column: "Agent",
                value: "Nathan Feuerstein");

            migrationBuilder.UpdateData(
                table: "Artistes",
                keyColumn: "Id",
                keyValue: 9,
                column: "Agent",
                value: "Black Hole Recordings");

            migrationBuilder.UpdateData(
                table: "Artistes",
                keyColumn: "Id",
                keyValue: 10,
                column: "Agent",
                value: "Monstercat");

            migrationBuilder.UpdateData(
                table: "Artistes",
                keyColumn: "Id",
                keyValue: 11,
                column: "Agent",
                value: "Cristi Ochiu");

            migrationBuilder.UpdateData(
                table: "Artistes",
                keyColumn: "Id",
                keyValue: 12,
                column: "Agent",
                value: "Warner Music Group");
        }
    }
}
