using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4._Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterDatabaseInnoviaDB3Again : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideojuegoPlataformaModel_Plataforma_plataformaModelPlataforma_ID",
                table: "VideojuegoPlataformaModel");

            migrationBuilder.DropForeignKey(
                name: "FK_VideojuegoPlataformaModel_Videojuego_Videojuego_ID1",
                table: "VideojuegoPlataformaModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VideojuegoPlataformaModel",
                table: "VideojuegoPlataformaModel");

            migrationBuilder.DropIndex(
                name: "IX_VideojuegoPlataformaModel_plataformaModelPlataforma_ID",
                table: "VideojuegoPlataformaModel");

            migrationBuilder.DropIndex(
                name: "IX_VideojuegoPlataformaModel_Videojuego_ID1",
                table: "VideojuegoPlataformaModel");

            migrationBuilder.DropColumn(
                name: "Videojuego_ID1",
                table: "VideojuegoPlataformaModel");

            migrationBuilder.DropColumn(
                name: "plataformaModelPlataforma_ID",
                table: "VideojuegoPlataformaModel");

            migrationBuilder.RenameTable(
                name: "VideojuegoPlataformaModel",
                newName: "VideojuegoPlataforma");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VideojuegoPlataforma",
                table: "VideojuegoPlataforma",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_VideojuegoPlataforma_Plataforma_ID",
                table: "VideojuegoPlataforma",
                column: "Plataforma_ID");

            migrationBuilder.CreateIndex(
                name: "IX_VideojuegoPlataforma_Videojuego_ID",
                table: "VideojuegoPlataforma",
                column: "Videojuego_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_VideojuegoPlataforma_Plataforma_Plataforma_ID",
                table: "VideojuegoPlataforma",
                column: "Plataforma_ID",
                principalTable: "Plataforma",
                principalColumn: "Plataforma_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VideojuegoPlataforma_Videojuego_Videojuego_ID",
                table: "VideojuegoPlataforma",
                column: "Videojuego_ID",
                principalTable: "Videojuego",
                principalColumn: "Videojuego_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideojuegoPlataforma_Plataforma_Plataforma_ID",
                table: "VideojuegoPlataforma");

            migrationBuilder.DropForeignKey(
                name: "FK_VideojuegoPlataforma_Videojuego_Videojuego_ID",
                table: "VideojuegoPlataforma");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VideojuegoPlataforma",
                table: "VideojuegoPlataforma");

            migrationBuilder.DropIndex(
                name: "IX_VideojuegoPlataforma_Plataforma_ID",
                table: "VideojuegoPlataforma");

            migrationBuilder.DropIndex(
                name: "IX_VideojuegoPlataforma_Videojuego_ID",
                table: "VideojuegoPlataforma");

            migrationBuilder.RenameTable(
                name: "VideojuegoPlataforma",
                newName: "VideojuegoPlataformaModel");

            migrationBuilder.AddColumn<int>(
                name: "Videojuego_ID1",
                table: "VideojuegoPlataformaModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "plataformaModelPlataforma_ID",
                table: "VideojuegoPlataformaModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VideojuegoPlataformaModel",
                table: "VideojuegoPlataformaModel",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_VideojuegoPlataformaModel_plataformaModelPlataforma_ID",
                table: "VideojuegoPlataformaModel",
                column: "plataformaModelPlataforma_ID");

            migrationBuilder.CreateIndex(
                name: "IX_VideojuegoPlataformaModel_Videojuego_ID1",
                table: "VideojuegoPlataformaModel",
                column: "Videojuego_ID1");

            migrationBuilder.AddForeignKey(
                name: "FK_VideojuegoPlataformaModel_Plataforma_plataformaModelPlataforma_ID",
                table: "VideojuegoPlataformaModel",
                column: "plataformaModelPlataforma_ID",
                principalTable: "Plataforma",
                principalColumn: "Plataforma_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_VideojuegoPlataformaModel_Videojuego_Videojuego_ID1",
                table: "VideojuegoPlataformaModel",
                column: "Videojuego_ID1",
                principalTable: "Videojuego",
                principalColumn: "Videojuego_ID");
        }
    }
}
