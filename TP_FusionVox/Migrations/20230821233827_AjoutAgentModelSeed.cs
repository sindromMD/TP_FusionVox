using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP_FusionVox.Migrations
{
    public partial class AjoutAgentModelSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgentReprId",
                table: "Artistes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Agent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pays = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    DateNaissance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Biographie = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: true),
                    SalaireMensuel = table.Column<float>(type: "real", nullable: false),
                    Curriel = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agent", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Agent",
                columns: new[] { "Id", "Biographie", "Curriel", "DateNaissance", "ImageURL", "Nom", "Pays", "SalaireMensuel" },
                values: new object[] { 1, "Abel Tesfaye, dit The Weeknd, né le 16 février 1990 à Toronto (Canada), est un chanteur, acteur, auteur-compositeur-interprète et producteur canadien. Il commence sa carrière musicale en 2009 en publiant anonymement de la musique sur YouTube.\r\n\r\nIl fonde le label XO en 2011 et publie ses premières mixtapes House of Balloons, Thursday, et Echoes of Silence. Il acquiert rapidement du succès et la reconnaissance de plusieurs médias en raison de son style de RnB contemporain sombre et de la part de mystère entourant son identité.\r\n\r\nEn 2012, il signe un contrat avec le label Republic Records et réédite les mixtapes dans l'album compilation Trilogy. Son premier album studio, Kiss Land, sort en 2013. Son deuxième album, Beauty Behind the Madness, mis en vente en 2015 et comprenant les singles classés en tête du Billboard Hot 100 Can't Feel My Face et The Hills, remporte le prix du « meilleur album urbain contemporain » aux Grammy Awards. Il fait partie des albums les plus vendus de l'année. Starboy, le troisième album, connait un succès commercial et remporte également le prix du meilleur album urbain contemporain aux Grammy Awards. Son quatrième album, After Hours, affiche plusieurs chansons en tête du classement Billboard Hot 100 telles que Heartless, Save Your Tears et Blinding Lights ; ce single est d’ailleurs devenu la première chanson de l'histoire à passer plus d'un an dans le top 10 du Billboard Hot 100 et est la chanson la plus écoutée sur la plateforme musicale Spotify en 2020.", "TEST@TEST.TEST", new DateTime(1999, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Alex", "Canada", 3000f });

            migrationBuilder.InsertData(
                table: "Agent",
                columns: new[] { "Id", "Biographie", "Curriel", "DateNaissance", "ImageURL", "Nom", "Pays", "SalaireMensuel" },
                values: new object[] { 2, "Abel Tesfaye, dit The Weeknd, né le 16 février 1990 à Toronto (Canada), est un chanteur, acteur, auteur-compositeur-interprète et producteur canadien. Il commence sa carrière musicale en 2009 en publiant anonymement de la musique sur YouTube.\r\n\r\nIl fonde le label XO en 2011 et publie ses premières mixtapes House of Balloons, Thursday, et Echoes of Silence. Il acquiert rapidement du succès et la reconnaissance de plusieurs médias en raison de son style de RnB contemporain sombre et de la part de mystère entourant son identité.\r\n\r\nEn 2012, il signe un contrat avec le label Republic Records et réédite les mixtapes dans l'album compilation Trilogy. Son premier album studio, Kiss Land, sort en 2013. Son deuxième album, Beauty Behind the Madness, mis en vente en 2015 et comprenant les singles classés en tête du Billboard Hot 100 Can't Feel My Face et The Hills, remporte le prix du « meilleur album urbain contemporain » aux Grammy Awards. Il fait partie des albums les plus vendus de l'année. Starboy, le troisième album, connait un succès commercial et remporte également le prix du meilleur album urbain contemporain aux Grammy Awards. Son quatrième album, After Hours, affiche plusieurs chansons en tête du classement Billboard Hot 100 telles que Heartless, Save Your Tears et Blinding Lights ; ce single est d’ailleurs devenu la première chanson de l'histoire à passer plus d'un an dans le top 10 du Billboard Hot 100 et est la chanson la plus écoutée sur la plateforme musicale Spotify en 2020.", "TEST@TEST.TEST", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Sinion", "Canada", 3000f });

            migrationBuilder.InsertData(
                table: "Agent",
                columns: new[] { "Id", "Biographie", "Curriel", "DateNaissance", "ImageURL", "Nom", "Pays", "SalaireMensuel" },
                values: new object[] { 3, "Abel Tesfaye, dit The Weeknd, né le 16 février 1990 à Toronto (Canada), est un chanteur, acteur, auteur-compositeur-interprète et producteur canadien. Il commence sa carrière musicale en 2009 en publiant anonymement de la musique sur YouTube.\r\n\r\nIl fonde le label XO en 2011 et publie ses premières mixtapes House of Balloons, Thursday, et Echoes of Silence. Il acquiert rapidement du succès et la reconnaissance de plusieurs médias en raison de son style de RnB contemporain sombre et de la part de mystère entourant son identité.\r\n\r\nEn 2012, il signe un contrat avec le label Republic Records et réédite les mixtapes dans l'album compilation Trilogy. Son premier album studio, Kiss Land, sort en 2013. Son deuxième album, Beauty Behind the Madness, mis en vente en 2015 et comprenant les singles classés en tête du Billboard Hot 100 Can't Feel My Face et The Hills, remporte le prix du « meilleur album urbain contemporain » aux Grammy Awards. Il fait partie des albums les plus vendus de l'année. Starboy, le troisième album, connait un succès commercial et remporte également le prix du meilleur album urbain contemporain aux Grammy Awards. Son quatrième album, After Hours, affiche plusieurs chansons en tête du classement Billboard Hot 100 telles que Heartless, Save Your Tears et Blinding Lights ; ce single est d’ailleurs devenu la première chanson de l'histoire à passer plus d'un an dans le top 10 du Billboard Hot 100 et est la chanson la plus écoutée sur la plateforme musicale Spotify en 2020.", "TEST@TEST.TEST", new DateTime(1998, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Cristi", "Canada", 3000f });

            migrationBuilder.UpdateData(
                table: "Artistes",
                keyColumn: "Id",
                keyValue: 1,
                column: "AgentReprId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Artistes",
                keyColumn: "Id",
                keyValue: 2,
                column: "AgentReprId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Artistes",
                keyColumn: "Id",
                keyValue: 3,
                column: "AgentReprId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Artistes",
                keyColumn: "Id",
                keyValue: 4,
                column: "AgentReprId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Artistes",
                keyColumn: "Id",
                keyValue: 5,
                column: "AgentReprId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Artistes",
                keyColumn: "Id",
                keyValue: 6,
                column: "AgentReprId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Artistes",
                keyColumn: "Id",
                keyValue: 7,
                column: "AgentReprId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Artistes",
                keyColumn: "Id",
                keyValue: 8,
                column: "AgentReprId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Artistes",
                keyColumn: "Id",
                keyValue: 9,
                column: "AgentReprId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Artistes",
                keyColumn: "Id",
                keyValue: 10,
                column: "AgentReprId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Artistes",
                keyColumn: "Id",
                keyValue: 11,
                column: "AgentReprId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Artistes",
                keyColumn: "Id",
                keyValue: 12,
                column: "AgentReprId",
                value: 3);

            migrationBuilder.CreateIndex(
                name: "IX_Artistes_AgentReprId",
                table: "Artistes",
                column: "AgentReprId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artistes_Agent_AgentReprId",
                table: "Artistes",
                column: "AgentReprId",
                principalTable: "Agent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artistes_Agent_AgentReprId",
                table: "Artistes");

            migrationBuilder.DropTable(
                name: "Agent");

            migrationBuilder.DropIndex(
                name: "IX_Artistes_AgentReprId",
                table: "Artistes");

            migrationBuilder.DropColumn(
                name: "AgentReprId",
                table: "Artistes");
        }
    }
}
