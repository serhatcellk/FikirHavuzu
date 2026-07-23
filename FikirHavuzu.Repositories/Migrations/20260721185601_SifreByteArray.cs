using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FikirHavuzu.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class SifreByteArray : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
{
    migrationBuilder.DropColumn(name: "SifreHash", table: "Kullanicilar");
    migrationBuilder.DropColumn(name: "SifreSalt", table: "Kullanicilar");

    migrationBuilder.AddColumn<byte[]>(
        name: "SifreHash", table: "Kullanicilar",
        type: "varbinary(max)", nullable: false, defaultValue: new byte[0]);

    migrationBuilder.AddColumn<byte[]>(
        name: "SifreSalt", table: "Kullanicilar",
        type: "varbinary(max)", nullable: false, defaultValue: new byte[0]);
}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SifreSalt",
                table: "Kullanicilar",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SifreHash",
                table: "Kullanicilar",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");
        }
    }
}
