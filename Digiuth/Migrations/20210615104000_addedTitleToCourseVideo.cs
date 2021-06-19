using Microsoft.EntityFrameworkCore.Migrations;

namespace Digiuth.Migrations
{
    public partial class addedTitleToCourseVideo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "CourseVideos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "CourseVideos");
        }
    }
}
