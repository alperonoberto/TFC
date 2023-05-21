using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Repositor.io.Migrations
{
    public partial class repositorios_fk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repositorio_Users_UsuarioId",
                table: "Repositorio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Repositorio",
                table: "Repositorio");

            migrationBuilder.RenameTable(
                name: "Repositorio",
                newName: "Repositorios");

            migrationBuilder.RenameIndex(
                name: "IX_Repositorio_UsuarioId",
                table: "Repositorios",
                newName: "IX_Repositorios_UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Repositorios",
                table: "Repositorios",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Repositorios_Users_UsuarioId",
                table: "Repositorios",
                column: "UsuarioId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repositorios_Users_UsuarioId",
                table: "Repositorios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Repositorios",
                table: "Repositorios");

            migrationBuilder.RenameTable(
                name: "Repositorios",
                newName: "Repositorio");

            migrationBuilder.RenameIndex(
                name: "IX_Repositorios_UsuarioId",
                table: "Repositorio",
                newName: "IX_Repositorio_UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Repositorio",
                table: "Repositorio",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Repositorio_Users_UsuarioId",
                table: "Repositorio",
                column: "UsuarioId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
