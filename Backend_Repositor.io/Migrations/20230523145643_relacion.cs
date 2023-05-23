using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Repositor.io.Migrations
{
    public partial class relacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Archivo_Repositorios_RepositorioId",
                table: "Archivo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Archivo",
                table: "Archivo");

            migrationBuilder.RenameTable(
                name: "Archivo",
                newName: "Archivos");

            migrationBuilder.RenameIndex(
                name: "IX_Archivo_RepositorioId",
                table: "Archivos",
                newName: "IX_Archivos_RepositorioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Archivos",
                table: "Archivos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Relaciones",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserSEGUIDORid = table.Column<long>(type: "bigint", nullable: false),
                    UserSEGUIDOid = table.Column<long>(type: "bigint", nullable: false),
                    FechaMod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relaciones_Users_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relaciones_UsuarioId",
                table: "Relaciones",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Archivos_Repositorios_RepositorioId",
                table: "Archivos",
                column: "RepositorioId",
                principalTable: "Repositorios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Archivos_Repositorios_RepositorioId",
                table: "Archivos");

            migrationBuilder.DropTable(
                name: "Relaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Archivos",
                table: "Archivos");

            migrationBuilder.RenameTable(
                name: "Archivos",
                newName: "Archivo");

            migrationBuilder.RenameIndex(
                name: "IX_Archivos_RepositorioId",
                table: "Archivo",
                newName: "IX_Archivo_RepositorioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Archivo",
                table: "Archivo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Archivo_Repositorios_RepositorioId",
                table: "Archivo",
                column: "RepositorioId",
                principalTable: "Repositorios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
