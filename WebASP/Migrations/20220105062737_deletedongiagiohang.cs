using Microsoft.EntityFrameworkCore.Migrations;

namespace WebASP.Migrations
{
    public partial class deletedongiagiohang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonGia",
                table: "GioHangs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "DonGia",
                table: "GioHangs",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
