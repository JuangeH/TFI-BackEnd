using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4._Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class alterTableForoAddColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "User_ID",
                table: "Foro",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Foro_User_ID",
                table: "Foro",
                column: "User_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Foro_Users_User_ID",
                table: "Foro",
                column: "User_ID",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foro_Users_User_ID",
                table: "Foro");

            migrationBuilder.DropIndex(
                name: "IX_Foro_User_ID",
                table: "Foro");

            migrationBuilder.DropColumn(
                name: "User_ID",
                table: "Foro");
        }
    }
}
