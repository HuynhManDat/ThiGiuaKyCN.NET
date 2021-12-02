using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Akatsuki_ThiGiuaKy.Migrations
{
    public partial class DbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Phims",
                columns: table => new
                {
                    MaPhim = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenPhim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phims", x => x.MaPhim);
                });

            migrationBuilder.CreateTable(
                name: "Raps",
                columns: table => new
                {
                    MaRap = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenRap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoChoTrong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raps", x => x.MaRap);
                });

            migrationBuilder.CreateTable(
                name: "LichChieuPhims",
                columns: table => new
                {
                    MaLichChieuPhim = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaRap = table.Column<int>(type: "int", nullable: false),
                    MaPhim = table.Column<int>(type: "int", nullable: false),
                    ThoiGianChieu = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichChieuPhims", x => x.MaLichChieuPhim);
                    table.ForeignKey(
                        name: "FK_LichChieuPhims_Phims_MaPhim",
                        column: x => x.MaPhim,
                        principalTable: "Phims",
                        principalColumn: "MaPhim",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LichChieuPhims_Raps_MaRap",
                        column: x => x.MaRap,
                        principalTable: "Raps",
                        principalColumn: "MaRap",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DatVes",
                columns: table => new
                {
                    MaDatVe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaLichChieuPhim = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    ThoiGianDat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatVes", x => x.MaDatVe);
                    table.ForeignKey(
                        name: "FK_DatVes_LichChieuPhims_MaLichChieuPhim",
                        column: x => x.MaLichChieuPhim,
                        principalTable: "LichChieuPhims",
                        principalColumn: "MaLichChieuPhim",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DatVes_MaLichChieuPhim",
                table: "DatVes",
                column: "MaLichChieuPhim");

            migrationBuilder.CreateIndex(
                name: "IX_LichChieuPhims_MaPhim",
                table: "LichChieuPhims",
                column: "MaPhim");

            migrationBuilder.CreateIndex(
                name: "IX_LichChieuPhims_MaRap",
                table: "LichChieuPhims",
                column: "MaRap");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatVes");

            migrationBuilder.DropTable(
                name: "LichChieuPhims");

            migrationBuilder.DropTable(
                name: "Phims");

            migrationBuilder.DropTable(
                name: "Raps");
        }
    }
}
