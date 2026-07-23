using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FikirHavuzu.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class YetkiEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Yetkiler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yetkiler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KullaniciYetkileri",
                columns: table => new
                {
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    YetkiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KullaniciYetkileri", x => new { x.KullaniciId, x.YetkiId });
                    table.ForeignKey(
                        name: "FK_KullaniciYetkileri_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KullaniciYetkileri_Yetkiler_YetkiId",
                        column: x => x.YetkiId,
                        principalTable: "Yetkiler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KullaniciYetkileri_YetkiId",
                table: "KullaniciYetkileri",
                column: "YetkiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KullaniciYetkileri");

            migrationBuilder.DropTable(
                name: "Yetkiler");
        }
    }
}
