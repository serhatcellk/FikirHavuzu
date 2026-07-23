using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FikirHavuzu.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class YetkiSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Yetkiler",
                columns: new[] { "Id", "Ad" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Degerlendirici" },
                    { 3, "Kullanici" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Yetkiler",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Yetkiler",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Yetkiler",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
