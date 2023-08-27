using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP_FusionVox.Migrations
{
    public partial class AjoutApplicationUsersEtDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Pseudonymes",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Agent",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Biographie", "Curriel", "SalaireMensuel" },
                values: new object[] { "Le professionnalisme de Alexei Culaxiz se reflète dans sa capacité à naviguer dans les complexités des contrats et des négociations, ce qui permet d'obtenir des avantages optimaux pour les artistes et la maison de disques. Sa riche expérience et sa connaissance approfondie de l'industrie ont fait de lui une source d'inspiration pour les artistes comme pour ses collègues.", "Alexei@vox.ca", 3564.5f });

            migrationBuilder.UpdateData(
                table: "Agent",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Biographie", "Curriel", "Pays", "SalaireMensuel" },
                values: new object[] { "Connu pour son grand professionnalisme, sa passion indéniable et sa capacité à transformer les rêves des artistes en réalité, Simion Beblea est un agent exceptionnel dans l'industrie musicale. Avec une carrière solide et des réalisations impressionnantes, Simion a consolidé sa réputation de mentor dévoué et de soutien aux artistes.", "Simion@vox.ca", "Romania", 4268.1f });

            migrationBuilder.UpdateData(
                table: "Agent",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Biographie", "Curriel", "Pays", "SalaireMensuel" },
                values: new object[] { "Avec un dévouement inébranlable et une vision large de l'industrie musicale, Mirza Iurii continue à ajouter de la valeur à chaque projet dans lequel il s'implique. Qu'il s'agisse de jeunes talents ou d'artistes confirmés, Iurie est reconnu pour sa capacité à guider les carrières vers de nouveaux sommets de réussite.", "Iurii@vox.ca", "Moldova", 9856.99f });

            migrationBuilder.UpdateData(
                table: "Concert",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "NbBilletsVendu", "PrixBillet" },
                values: new object[] { 123, 100.25m });

            migrationBuilder.UpdateData(
                table: "Concert",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "NbBilletsVendu", "NbTotalBillets", "PrixBillet" },
                values: new object[] { 56, 323, 35.99m });

            migrationBuilder.UpdateData(
                table: "Concert",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "NbBilletsVendu", "PrixBillet" },
                values: new object[] { 72, 45.23m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Pseudonymes",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Agent",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Biographie", "Curriel", "SalaireMensuel" },
                values: new object[] { "Abel Tesfaye, dit The Weeknd, né le 16 février 1990 à Toronto (Canada), est un chanteur, acteur, auteur-compositeur-interprète et producteur canadien. Il commence sa carrière musicale en 2009 en publiant anonymement de la musique sur YouTube.\r\n\r\nIl fonde le label XO en 2011 et publie ses premières mixtapes House of Balloons, Thursday, et Echoes of Silence. Il acquiert rapidement du succès et la reconnaissance de plusieurs médias en raison de son style de RnB contemporain sombre et de la part de mystère entourant son identité.\r\n\r\nEn 2012, il signe un contrat avec le label Republic Records et réédite les mixtapes dans l'album compilation Trilogy. Son premier album studio, Kiss Land, sort en 2013. Son deuxième album, Beauty Behind the Madness, mis en vente en 2015 et comprenant les singles classés en tête du Billboard Hot 100 Can't Feel My Face et The Hills, remporte le prix du « meilleur album urbain contemporain » aux Grammy Awards. Il fait partie des albums les plus vendus de l'année. Starboy, le troisième album, connait un succès commercial et remporte également le prix du meilleur album urbain contemporain aux Grammy Awards. Son quatrième album, After Hours, affiche plusieurs chansons en tête du classement Billboard Hot 100 telles que Heartless, Save Your Tears et Blinding Lights ; ce single est d’ailleurs devenu la première chanson de l'histoire à passer plus d'un an dans le top 10 du Billboard Hot 100 et est la chanson la plus écoutée sur la plateforme musicale Spotify en 2020.", "TEST@TEST.TEST", 3000f });

            migrationBuilder.UpdateData(
                table: "Agent",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Biographie", "Curriel", "Pays", "SalaireMensuel" },
                values: new object[] { "Abel Tesfaye, dit The Weeknd, né le 16 février 1990 à Toronto (Canada), est un chanteur, acteur, auteur-compositeur-interprète et producteur canadien. Il commence sa carrière musicale en 2009 en publiant anonymement de la musique sur YouTube.\r\n\r\nIl fonde le label XO en 2011 et publie ses premières mixtapes House of Balloons, Thursday, et Echoes of Silence. Il acquiert rapidement du succès et la reconnaissance de plusieurs médias en raison de son style de RnB contemporain sombre et de la part de mystère entourant son identité.\r\n\r\nEn 2012, il signe un contrat avec le label Republic Records et réédite les mixtapes dans l'album compilation Trilogy. Son premier album studio, Kiss Land, sort en 2013. Son deuxième album, Beauty Behind the Madness, mis en vente en 2015 et comprenant les singles classés en tête du Billboard Hot 100 Can't Feel My Face et The Hills, remporte le prix du « meilleur album urbain contemporain » aux Grammy Awards. Il fait partie des albums les plus vendus de l'année. Starboy, le troisième album, connait un succès commercial et remporte également le prix du meilleur album urbain contemporain aux Grammy Awards. Son quatrième album, After Hours, affiche plusieurs chansons en tête du classement Billboard Hot 100 telles que Heartless, Save Your Tears et Blinding Lights ; ce single est d’ailleurs devenu la première chanson de l'histoire à passer plus d'un an dans le top 10 du Billboard Hot 100 et est la chanson la plus écoutée sur la plateforme musicale Spotify en 2020.", "TEST@TEST.TEST", "Canada", 3000f });

            migrationBuilder.UpdateData(
                table: "Agent",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Biographie", "Curriel", "Pays", "SalaireMensuel" },
                values: new object[] { "Abel Tesfaye, dit The Weeknd, né le 16 février 1990 à Toronto (Canada), est un chanteur, acteur, auteur-compositeur-interprète et producteur canadien. Il commence sa carrière musicale en 2009 en publiant anonymement de la musique sur YouTube.\r\n\r\nIl fonde le label XO en 2011 et publie ses premières mixtapes House of Balloons, Thursday, et Echoes of Silence. Il acquiert rapidement du succès et la reconnaissance de plusieurs médias en raison de son style de RnB contemporain sombre et de la part de mystère entourant son identité.\r\n\r\nEn 2012, il signe un contrat avec le label Republic Records et réédite les mixtapes dans l'album compilation Trilogy. Son premier album studio, Kiss Land, sort en 2013. Son deuxième album, Beauty Behind the Madness, mis en vente en 2015 et comprenant les singles classés en tête du Billboard Hot 100 Can't Feel My Face et The Hills, remporte le prix du « meilleur album urbain contemporain » aux Grammy Awards. Il fait partie des albums les plus vendus de l'année. Starboy, le troisième album, connait un succès commercial et remporte également le prix du meilleur album urbain contemporain aux Grammy Awards. Son quatrième album, After Hours, affiche plusieurs chansons en tête du classement Billboard Hot 100 telles que Heartless, Save Your Tears et Blinding Lights ; ce single est d’ailleurs devenu la première chanson de l'histoire à passer plus d'un an dans le top 10 du Billboard Hot 100 et est la chanson la plus écoutée sur la plateforme musicale Spotify en 2020.", "TEST@TEST.TEST", "Canada", 3000f });

            migrationBuilder.UpdateData(
                table: "Concert",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "NbBilletsVendu", "PrixBillet" },
                values: new object[] { null, 100m });

            migrationBuilder.UpdateData(
                table: "Concert",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "NbBilletsVendu", "NbTotalBillets", "PrixBillet" },
                values: new object[] { null, 300, 35m });

            migrationBuilder.UpdateData(
                table: "Concert",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "NbBilletsVendu", "PrixBillet" },
                values: new object[] { null, 45m });
        }
    }
}
