using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4._Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterPuntuacionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "User_ID",
                table: "Puntuacion",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Puntuacion_User_ID",
                table: "Puntuacion",
                column: "User_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Puntuacion_Users_User_ID",
                table: "Puntuacion",
                column: "User_ID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Puntuacion_Users_User_ID",
                table: "Puntuacion");

            migrationBuilder.DropIndex(
                name: "IX_Puntuacion_User_ID",
                table: "Puntuacion");

            migrationBuilder.DropColumn(
                name: "User_ID",
                table: "Puntuacion");
        }
    }
}
