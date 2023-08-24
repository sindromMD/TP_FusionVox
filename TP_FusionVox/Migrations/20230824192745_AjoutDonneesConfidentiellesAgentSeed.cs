using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP_FusionVox.Migrations
{
    public partial class AjoutDonneesConfidentiellesAgentSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DonneesConfidentiellesAgentID",
                table: "Agent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DonneesConfidentiellesAgent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroDeContrat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankAccountInfo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonneesConfidentiellesAgent", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DonneesConfidentiellesAgent",
                columns: new[] { "Id", "BankAccountInfo", "NumeroDeContrat" },
                values: new object[] { 1, "1234589765478963", "2222-44445555" });

            migrationBuilder.InsertData(
                table: "DonneesConfidentiellesAgent",
                columns: new[] { "Id", "BankAccountInfo", "NumeroDeContrat" },
                values: new object[] { 2, "1234589765478963", "2222-44445555" });

            migrationBuilder.InsertData(
                table: "DonneesConfidentiellesAgent",
                columns: new[] { "Id", "BankAccountInfo", "NumeroDeContrat" },
                values: new object[] { 3, "1234589765478963", "2222-44445555" });

            migrationBuilder.UpdateData(
                table: "Agent",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DonneesConfidentiellesAgentID", "Nom" },
                values: new object[] { 1, "Alex Culaxiz" });

            migrationBuilder.UpdateData(
                table: "Agent",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DonneesConfidentiellesAgentID", "Nom" },
                values: new object[] { 2, "Simion Beblea" });

            migrationBuilder.UpdateData(
                table: "Agent",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DonneesConfidentiellesAgentID", "Nom" },
                values: new object[] { 3, "Mirza Iurii" });

            migrationBuilder.CreateIndex(
                name: "IX_Agent_DonneesConfidentiellesAgentID",
                table: "Agent",
                column: "DonneesConfidentiellesAgentID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Agent_DonneesConfidentiellesAgent_DonneesConfidentiellesAgentID",
                table: "Agent",
                column: "DonneesConfidentiellesAgentID",
                principalTable: "DonneesConfidentiellesAgent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agent_DonneesConfidentiellesAgent_DonneesConfidentiellesAgentID",
                table: "Agent");

            migrationBuilder.DropTable(
                name: "DonneesConfidentiellesAgent");

            migrationBuilder.DropIndex(
                name: "IX_Agent_DonneesConfidentiellesAgentID",
                table: "Agent");

            migrationBuilder.DropColumn(
                name: "DonneesConfidentiellesAgentID",
                table: "Agent");

            migrationBuilder.UpdateData(
                table: "Agent",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nom",
                value: "Alex");

            migrationBuilder.UpdateData(
                table: "Agent",
                keyColumn: "Id",
                keyValue: 2,
                column: "Nom",
                value: "Sinion");

            migrationBuilder.UpdateData(
                table: "Agent",
                keyColumn: "Id",
                keyValue: 3,
                column: "Nom",
                value: "Cristi");
        }
    }
}
