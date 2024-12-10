using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4._Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableRecomendacionUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecomendacionUsuario_Users_usuarioId",
                table: "RecomendacionUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_RecomendacionUsuario_Videojuego_Videojuego_ID",
                table: "RecomendacionUsuario");

            migrationBuilder.DropIndex(
                name: "IX_RecomendacionUsuario_usuarioId",
                table: "RecomendacionUsuario");

            migrationBuilder.DropIndex(
                name: "IX_RecomendacionUsuario_Videojuego_ID",
                table: "RecomendacionUsuario");

            migrationBuilder.DropColumn(
                name: "Videojuego_ID",
                table: "RecomendacionUsuario");

            migrationBuilder.DropColumn(
                name: "usuarioId",
                table: "RecomendacionUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Videojuego_ID",
                table: "RecomendacionUsuario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "usuarioId",
                table: "RecomendacionUsuario",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RecomendacionUsuario_usuarioId",
                table: "RecomendacionUsuario",
                column: "usuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_RecomendacionUsuario_Videojuego_ID",
                table: "RecomendacionUsuario",
                column: "Videojuego_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RecomendacionUsuario_Users_usuarioId",
                table: "RecomendacionUsuario",
                column: "usuarioId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecomendacionUsuario_Videojuego_Videojuego_ID",
                table: "RecomendacionUsuario",
                column: "Videojuego_ID",
                principalTable: "Videojuego",
                principalColumn: "Videojuego_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
