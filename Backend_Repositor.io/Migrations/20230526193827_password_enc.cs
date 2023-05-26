using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Repositor.io.Migrations
{
    public partial class password_enc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordKey",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordKey",
                table: "Users");
        }
    }
}
