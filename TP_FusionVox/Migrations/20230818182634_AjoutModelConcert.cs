using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP_FusionVox.Migrations
{
    public partial class AjoutModelConcert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Concert",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateConcert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pays = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Ville = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Scene = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    NbTotalBillets = table.Column<int>(type: "int", nullable: false),
                    NbBilletsVendu = table.Column<int>(type: "int", nullable: true),
                    PrixBillet = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concert", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArtisteConcert",
                columns: table => new
                {
                    ListArtistesId = table.Column<int>(type: "int", nullable: false),
                    ListConcertsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtisteConcert", x => new { x.ListArtistesId, x.ListConcertsId });
                    table.ForeignKey(
                        name: "FK_ArtisteConcert_Artistes_ListArtistesId",
                        column: x => x.ListArtistesId,
                        principalTable: "Artistes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtisteConcert_Concert_ListConcertsId",
                        column: x => x.ListConcertsId,
                        principalTable: "Concert",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtisteConcert_ListConcertsId",
                table: "ArtisteConcert",
                column: "ListConcertsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtisteConcert");

            migrationBuilder.DropTable(
                name: "Concert");
        }
    }
}
