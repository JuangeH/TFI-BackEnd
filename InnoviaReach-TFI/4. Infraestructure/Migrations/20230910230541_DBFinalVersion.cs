using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4._Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class DBFinalVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Notificacion_Notificacion_ID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Notificacion");

            migrationBuilder.DropIndex(
                name: "IX_Users_Notificacion_ID",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "Actualizaciones",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Descuentos",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Actualizaciones",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Descuentos",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "Notificacion",
                columns: table => new
                {
                    Notificacion_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Actualizaciones = table.Column<bool>(type: "bit", nullable: false),
                    Descuentos = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacion", x => x.Notificacion_ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Notificacion_ID",
                table: "Users",
                column: "Notificacion_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Notificacion_Notificacion_ID",
                table: "Users",
                column: "Notificacion_ID",
                principalTable: "Notificacion",
                principalColumn: "Notificacion_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
