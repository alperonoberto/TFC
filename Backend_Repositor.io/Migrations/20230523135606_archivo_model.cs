using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Repositor.io.Migrations
{
    public partial class archivo_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Archivo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Filename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Filepath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaSubida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RepositorioId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Archivo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Archivo_Repositorios_RepositorioId",
                        column: x => x.RepositorioId,
                        principalTable: "Repositorios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Archivo_RepositorioId",
                table: "Archivo",
                column: "RepositorioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Archivo");
        }
    }
}
