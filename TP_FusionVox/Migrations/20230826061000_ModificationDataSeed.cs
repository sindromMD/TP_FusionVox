using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP_FusionVox.Migrations
{
    public partial class ModificationDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.UpdateData(
                table: "DonneesConfidentiellesAgent",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BankAccountInfo", "NumeroDeContrat" },
                values: new object[] { "1278162365478963", "2522-45445576" });

            migrationBuilder.UpdateData(
                table: "DonneesConfidentiellesAgent",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BankAccountInfo", "NumeroDeContrat" },
                values: new object[] { "8904589765450963", "2252-44745655" });

            migrationBuilder.UpdateData(
                table: "DonneesConfidentiellesAgent",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BankAccountInfo", "NumeroDeContrat" },
                values: new object[] { "4255897697478343", "2426-48244589" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "DonneesConfidentiellesAgent",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BankAccountInfo", "NumeroDeContrat" },
                values: new object[] { "1234589765478963", "2222-44445555" });

            migrationBuilder.UpdateData(
                table: "DonneesConfidentiellesAgent",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BankAccountInfo", "NumeroDeContrat" },
                values: new object[] { "1234589765478963", "2222-44445555" });

            migrationBuilder.UpdateData(
                table: "DonneesConfidentiellesAgent",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BankAccountInfo", "NumeroDeContrat" },
                values: new object[] { "1234589765478963", "2222-44445555" });
        }
    }
}
