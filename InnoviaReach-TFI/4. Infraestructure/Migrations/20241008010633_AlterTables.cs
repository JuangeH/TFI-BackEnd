using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4._Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Header_image",
                table: "Videojuego",
                type: "varchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Metacritic_score",
                table: "Videojuego",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Metacritic_url",
                table: "Videojuego",
                type: "varchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SteamAppid",
                table: "Videojuego",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CantidadLogros",
                table: "Adquisicion",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TiempoJuego",
                table: "Adquisicion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TiempoJuegoReciente",
                table: "Adquisicion",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SteamAccount",
                columns: table => new
                {
                    SteamAccount_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    steamid = table.Column<int>(type: "int", nullable: false),
                    personaname = table.Column<string>(type: "varchar(max)", nullable: false),
                    avatarfull = table.Column<string>(type: "varchar(max)", nullable: false),
                    profileurl = table.Column<string>(type: "varchar(max)", nullable: false),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SteamAccount", x => x.SteamAccount_ID);
                    table.ForeignKey(
                        name: "FK_SteamAccount_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SteamAccount_User_ID",
                table: "SteamAccount",
                column: "User_ID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SteamAccount");

            migrationBuilder.DropColumn(
                name: "Header_image",
                table: "Videojuego");

            migrationBuilder.DropColumn(
                name: "Metacritic_score",
                table: "Videojuego");

            migrationBuilder.DropColumn(
                name: "Metacritic_url",
                table: "Videojuego");

            migrationBuilder.DropColumn(
                name: "SteamAppid",
                table: "Videojuego");

            migrationBuilder.DropColumn(
                name: "CantidadLogros",
                table: "Adquisicion");

            migrationBuilder.DropColumn(
                name: "TiempoJuego",
                table: "Adquisicion");

            migrationBuilder.DropColumn(
                name: "TiempoJuegoReciente",
                table: "Adquisicion");
        }
    }
}
