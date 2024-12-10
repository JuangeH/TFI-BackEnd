using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4._Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableBannedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Actualizaciones",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Descuentos",
                table: "Users",
                newName: "CommunityBanned");

            migrationBuilder.CreateTable(
                name: "UsuarioBaneado",
                columns: table => new
                {
                    Baneo_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserAdmin_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioBaneado", x => x.Baneo_ID);
                    table.ForeignKey(
                        name: "FK_UsuarioBaneado_Users_UserAdmin_ID",
                        column: x => x.UserAdmin_ID,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsuarioBaneado_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioBaneado_User_ID",
                table: "UsuarioBaneado",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioBaneado_UserAdmin_ID",
                table: "UsuarioBaneado",
                column: "UserAdmin_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioBaneado");

            migrationBuilder.RenameColumn(
                name: "CommunityBanned",
                table: "Users",
                newName: "Descuentos");

            migrationBuilder.AddColumn<bool>(
                name: "Actualizaciones",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
