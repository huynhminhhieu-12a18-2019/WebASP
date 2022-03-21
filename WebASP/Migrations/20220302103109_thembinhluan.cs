using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebASP.Migrations
{
    public partial class thembinhluan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BinhLuans",
                columns: table => new
                {
                    BinhLuanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    noidung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trangthai = table.Column<bool>(type: "bit", nullable: false),
                    thoigian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaiKhoanId = table.Column<int>(type: "int", nullable: false),
                    SanPhamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinhLuans", x => x.BinhLuanId);
                    table.ForeignKey(
                        name: "FK_BinhLuans_SanPhams_SanPhamId",
                        column: x => x.SanPhamId,
                        principalTable: "SanPhams",
                        principalColumn: "SanPhamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BinhLuans_TaiKhoans_TaiKhoanId",
                        column: x => x.TaiKhoanId,
                        principalTable: "TaiKhoans",
                        principalColumn: "TaiKhoanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuans_SanPhamId",
                table: "BinhLuans",
                column: "SanPhamId");

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuans_TaiKhoanId",
                table: "BinhLuans",
                column: "TaiKhoanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BinhLuans");
        }
    }
}
