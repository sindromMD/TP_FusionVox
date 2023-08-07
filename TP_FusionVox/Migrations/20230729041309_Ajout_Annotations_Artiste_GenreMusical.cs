using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP_FusionVox.Migrations
{
    public partial class Ajout_Annotations_Artiste_GenreMusical : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GenreMusical",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreMusical", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pays = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    DebutCarrier = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Biographie = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    EstVedette = table.Column<bool>(type: "bit", nullable: false),
                    NbAbonnees = table.Column<int>(type: "int", nullable: false),
                    NbChansons = table.Column<int>(type: "int", nullable: false),
                    Agent = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    IdGenreMusical = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artists_GenreMusical_IdGenreMusical",
                        column: x => x.IdGenreMusical,
                        principalTable: "GenreMusical",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artists_IdGenreMusical",
                table: "Artists",
                column: "IdGenreMusical");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropTable(
                name: "GenreMusical");
        }
    }
}
