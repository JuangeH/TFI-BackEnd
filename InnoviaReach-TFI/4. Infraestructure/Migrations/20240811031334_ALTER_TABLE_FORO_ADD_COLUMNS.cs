using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4._Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class ALTER_TABLE_FORO_ADD_COLUMNS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UltimaFecha",
                table: "TiempoDeJuego");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Adquisicion");

            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "Adquisicion");

            migrationBuilder.RenameColumn(
                name: "CantidadHoras",
                table: "TiempoDeJuego",
                newName: "CantidadMinutos");

            migrationBuilder.AddColumn<int>(
                name: "Recomendaciones",
                table: "Videojuego",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Foro",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Foro",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreado",
                table: "Foro",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "Foro",
                type: "nvarchar(250)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Visitas",
                table: "Foro",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LogTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime", nullable: true),
                    Exception = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    LogEvent = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    ReferenceNumber = table.Column<int>(type: "int", nullable: true),
                    ReferenceType = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogTable", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogTable");

            migrationBuilder.DropColumn(
                name: "Recomendaciones",
                table: "Videojuego");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Foro");

            migrationBuilder.DropColumn(
                name: "FechaCreado",
                table: "Foro");

            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "Foro");

            migrationBuilder.DropColumn(
                name: "Visitas",
                table: "Foro");

            migrationBuilder.RenameColumn(
                name: "CantidadMinutos",
                table: "TiempoDeJuego",
                newName: "CantidadHoras");

            migrationBuilder.AddColumn<DateTime>(
                name: "UltimaFecha",
                table: "TiempoDeJuego",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Foro",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Adquisicion",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "Adquisicion",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
