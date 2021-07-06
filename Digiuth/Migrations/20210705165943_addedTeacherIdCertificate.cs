using Microsoft.EntityFrameworkCore.Migrations;

namespace Digiuth.Migrations
{
    public partial class addedTeacherIdCertificate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Certificates");

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "Certificates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherId",
                table: "Certificates",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShortDesc",
                table: "Blogs",
                maxLength: 110,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Certificates");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Certificates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShortDesc",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 110,
                oldNullable: true);
        }
    }
}
