using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP_FusionVox.Migrations
{
    public partial class AjoutSeedConcert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Concert",
                columns: new[] { "Id", "DateConcert", "Description", "ImageUrl", "NbBilletsVendu", "NbTotalBillets", "Nom", "Pays", "PrixBillet", "Scene", "Ville" },
                values: new object[] { 1, new DateTime(2023, 10, 3, 20, 0, 0, 0, DateTimeKind.Unspecified), "Informations importantes sur l'événement: En achetant des billets pour cet événement, vous acceptez de respecter les mesures de santé et de sécurité en vigueur lors de l'événement, qui peuvent inclure, notamment, le port du masque. Notez que ces règlements s'appliquent à tous les utilisateurs de billets. Directives sujettes à changement. Vérifiez régulièrement le site de la salle de votre événement.", "", null, 1000, "Hope Tour", "Canada", 100m, "Place Bell", "Laval" });

            migrationBuilder.InsertData(
                table: "Concert",
                columns: new[] { "Id", "DateConcert", "Description", "ImageUrl", "NbBilletsVendu", "NbTotalBillets", "Nom", "Pays", "PrixBillet", "Scene", "Ville" },
                values: new object[] { 2, new DateTime(2023, 10, 21, 20, 0, 0, 0, DateTimeKind.Unspecified), "Informations importantes sur l'événement: En achetant des billets pour cet événement, vous acceptez de respecter les mesures de santé et de sécurité en vigueur lors de l'événement, qui peuvent inclure, notamment, le port du masque. Notez que ces règlements s'appliquent à tous les utilisateurs de billets. Directives sujettes à changement. Vérifiez régulièrement le site de la salle de votre événement.", "", null, 300, "Guitar Story", "Canada", 35m, "Le Club Square Dix30", "Brossard" });

            migrationBuilder.InsertData(
                table: "Concert",
                columns: new[] { "Id", "DateConcert", "Description", "ImageUrl", "NbBilletsVendu", "NbTotalBillets", "Nom", "Pays", "PrixBillet", "Scene", "Ville" },
                values: new object[] { 3, new DateTime(2023, 10, 31, 20, 0, 0, 0, DateTimeKind.Unspecified), "Informations importantes sur l'événement: En achetant des billets pour cet événement, vous acceptez de respecter les mesures de santé et de sécurité en vigueur lors de l'événement, qui peuvent inclure, notamment, le port du masque. Notez que ces règlements s'appliquent à tous les utilisateurs de billets. Directives sujettes à changement. Vérifiez régulièrement le site de la salle de votre événement.", "", null, 600, "MetalCore", "Canada", 45m, "L'Etoile", "Montreal" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Concert",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Concert",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Concert",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
