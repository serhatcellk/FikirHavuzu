using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FikirHavuzu.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class OdulSistemi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OdulPuani",
                table: "Kullanicilar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "OdulVerildi",
                table: "Fikirler",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OdulPuani",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "OdulVerildi",
                table: "Fikirler");
        }
    }
}
