using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Repositor.io.Migrations
{
    public partial class repositorios_fk2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repositorios_Users_UsuarioId",
                table: "Repositorios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Repositorios",
                table: "Repositorios");

            migrationBuilder.DropIndex(
                name: "IX_Repositorios_UsuarioId",
                table: "Repositorios");

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioId",
                table: "Repositorios",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Repositorios",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "UsuarioId1",
                table: "Repositorios",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Repositorios",
                table: "Repositorios",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Repositorios_UsuarioId1",
                table: "Repositorios",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Repositorios_Users_UsuarioId1",
                table: "Repositorios",
                column: "UsuarioId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repositorios_Users_UsuarioId1",
                table: "Repositorios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Repositorios",
                table: "Repositorios");

            migrationBuilder.DropIndex(
                name: "IX_Repositorios_UsuarioId1",
                table: "Repositorios");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Repositorios");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Repositorios",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioId",
                table: "Repositorios",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Repositorios",
                table: "Repositorios",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Repositorios_UsuarioId",
                table: "Repositorios",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Repositorios_Users_UsuarioId",
                table: "Repositorios",
                column: "UsuarioId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
