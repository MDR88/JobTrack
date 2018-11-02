using Microsoft.EntityFrameworkCore.Migrations;

namespace JobTrack.Data.Migrations
{
    public partial class removednamefromjobs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Job");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Job",
                maxLength: 25,
                nullable: false,
                defaultValue: "");
        }
    }
}
