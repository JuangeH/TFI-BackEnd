using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4._Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Videojuego_VideojuegoModelVideojuego_ID",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_Rating_VideojuegoModelVideojuego_ID",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "VideojuegoModelVideojuego_ID",
                table: "Rating");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_Videojuego_ID",
                table: "Rating",
                column: "Videojuego_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Videojuego_Videojuego_ID",
                table: "Rating",
                column: "Videojuego_ID",
                principalTable: "Videojuego",
                principalColumn: "Videojuego_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Videojuego_Videojuego_ID",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_Rating_Videojuego_ID",
                table: "Rating");

            migrationBuilder.AddColumn<int>(
                name: "VideojuegoModelVideojuego_ID",
                table: "Rating",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rating_VideojuegoModelVideojuego_ID",
                table: "Rating",
                column: "VideojuegoModelVideojuego_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Videojuego_VideojuegoModelVideojuego_ID",
                table: "Rating",
                column: "VideojuegoModelVideojuego_ID",
                principalTable: "Videojuego",
                principalColumn: "Videojuego_ID");
        }
    }
}
