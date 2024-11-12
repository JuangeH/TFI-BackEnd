using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4._Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableUsuarioVisitas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsuarioVisita",
                columns: table => new
                {
                    Visita_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Videojuego_ID = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioVisita", x => x.Visita_ID);
                    table.ForeignKey(
                        name: "FK_UsuarioVisita_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioVisita_Videojuego_Videojuego_ID",
                        column: x => x.Videojuego_ID,
                        principalTable: "Videojuego",
                        principalColumn: "Videojuego_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioVisita_User_ID",
                table: "UsuarioVisita",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioVisita_Videojuego_ID",
                table: "UsuarioVisita",
                column: "Videojuego_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioVisita");
        }
    }
}
