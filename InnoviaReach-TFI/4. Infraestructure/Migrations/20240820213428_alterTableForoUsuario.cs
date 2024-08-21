using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4._Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class alterTableForoUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ForoUsuario",
                table: "ForoUsuario");

            migrationBuilder.DropIndex(
                name: "IX_ForoUsuario_User_ID",
                table: "ForoUsuario");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "ForoUsuario");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "ForoUsuario");

            migrationBuilder.DropColumn(
                name: "Visitas",
                table: "Foro");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ForoUsuario",
                table: "ForoUsuario",
                columns: new[] { "User_ID", "Foro_ID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ForoUsuario",
                table: "ForoUsuario");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "ForoUsuario",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<bool>(
                name: "Tipo",
                table: "ForoUsuario",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Visitas",
                table: "Foro",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ForoUsuario",
                table: "ForoUsuario",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_ForoUsuario_User_ID",
                table: "ForoUsuario",
                column: "User_ID");
        }
    }
}
