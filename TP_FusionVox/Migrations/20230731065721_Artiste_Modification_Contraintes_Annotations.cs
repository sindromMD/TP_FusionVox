using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP_FusionVox.Migrations
{
    public partial class Artiste_Modification_Contraintes_Annotations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageURL",
                table: "Artistes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Biographie",
                table: "Artistes",
                type: "nvarchar(2500)",
                maxLength: 2500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2500)",
                oldMaxLength: 2500);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageURL",
                table: "Artistes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Biographie",
                table: "Artistes",
                type: "nvarchar(2500)",
                maxLength: 2500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(2500)",
                oldMaxLength: 2500,
                oldNullable: true);
        }
    }
}
