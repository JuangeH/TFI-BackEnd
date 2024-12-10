using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4._Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableUsuarioJuegoPerfil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClusterID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GameGenresJson",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GameHistoryJson",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GameTagsJson",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "UsuarioJuegoPerfil",
                columns: table => new
                {
                    Perfil_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClusterID = table.Column<int>(type: "int", nullable: true),
                    TipoRecomendacion = table.Column<string>(type: "varchar(max)", nullable: true),
                    GameGenresJson = table.Column<string>(type: "varchar(max)", nullable: true),
                    GameTagsJson = table.Column<string>(type: "varchar(max)", nullable: true),
                    GameHistoryJson = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioJuegoPerfil", x => x.Perfil_ID);
                    table.ForeignKey(
                        name: "FK_UsuarioJuegoPerfil_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioJuegoPerfil_User_ID",
                table: "UsuarioJuegoPerfil",
                column: "User_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioJuegoPerfil");

            migrationBuilder.AddColumn<int>(
                name: "ClusterID",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GameGenresJson",
                table: "Users",
                type: "varchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GameHistoryJson",
                table: "Users",
                type: "varchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GameTagsJson",
                table: "Users",
                type: "varchar(max)",
                nullable: true);
        }
    }
}
