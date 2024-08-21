using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4._Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class TablesForoUsuarioVisitFav : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForoUsuario_Foro_Foro_ID",
                table: "ForoUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_ForoUsuario_Users_User_ID",
                table: "ForoUsuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ForoUsuario",
                table: "ForoUsuario");

            migrationBuilder.RenameTable(
                name: "ForoUsuario",
                newName: "ForoUsuarioVisita");

            migrationBuilder.RenameIndex(
                name: "IX_ForoUsuario_Foro_ID",
                table: "ForoUsuarioVisita",
                newName: "IX_ForoUsuarioVisita_Foro_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ForoUsuarioVisita",
                table: "ForoUsuarioVisita",
                columns: new[] { "User_ID", "Foro_ID" });

            migrationBuilder.CreateTable(
                name: "ForoUsuarioFavorito",
                columns: table => new
                {
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Foro_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForoUsuarioFavorito", x => new { x.User_ID, x.Foro_ID });
                    table.ForeignKey(
                        name: "FK_ForoUsuarioFavorito_Foro_Foro_ID",
                        column: x => x.Foro_ID,
                        principalTable: "Foro",
                        principalColumn: "Foro_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ForoUsuarioFavorito_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForoUsuarioFavorito_Foro_ID",
                table: "ForoUsuarioFavorito",
                column: "Foro_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ForoUsuarioVisita_Foro_Foro_ID",
                table: "ForoUsuarioVisita",
                column: "Foro_ID",
                principalTable: "Foro",
                principalColumn: "Foro_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ForoUsuarioVisita_Users_User_ID",
                table: "ForoUsuarioVisita",
                column: "User_ID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForoUsuarioVisita_Foro_Foro_ID",
                table: "ForoUsuarioVisita");

            migrationBuilder.DropForeignKey(
                name: "FK_ForoUsuarioVisita_Users_User_ID",
                table: "ForoUsuarioVisita");

            migrationBuilder.DropTable(
                name: "ForoUsuarioFavorito");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ForoUsuarioVisita",
                table: "ForoUsuarioVisita");

            migrationBuilder.RenameTable(
                name: "ForoUsuarioVisita",
                newName: "ForoUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_ForoUsuarioVisita_Foro_ID",
                table: "ForoUsuario",
                newName: "IX_ForoUsuario_Foro_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ForoUsuario",
                table: "ForoUsuario",
                columns: new[] { "User_ID", "Foro_ID" });

            migrationBuilder.AddForeignKey(
                name: "FK_ForoUsuario_Foro_Foro_ID",
                table: "ForoUsuario",
                column: "Foro_ID",
                principalTable: "Foro",
                principalColumn: "Foro_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ForoUsuario_Users_User_ID",
                table: "ForoUsuario",
                column: "User_ID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
