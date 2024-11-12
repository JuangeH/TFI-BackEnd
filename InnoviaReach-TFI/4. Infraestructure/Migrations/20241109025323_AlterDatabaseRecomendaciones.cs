using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4._Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterDatabaseRecomendaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CaracteristicasVector",
                table: "Videojuego",
                type: "varchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClusterID",
                table: "Videojuego",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateTable(
                name: "RecomendacionUsuario",
                columns: table => new
                {
                    RecomendacionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VideojuegoRecomendadoId = table.Column<int>(type: "int", nullable: false),
                    Frecuencia = table.Column<int>(type: "int", nullable: false),
                    TipoRecomendacion = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    FechaRecomendacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    usuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Videojuego_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecomendacionUsuario", x => x.RecomendacionId);
                    table.ForeignKey(
                        name: "FK_RecomendacionUsuario_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RecomendacionUsuario_Users_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecomendacionUsuario_Videojuego_VideojuegoRecomendadoId",
                        column: x => x.VideojuegoRecomendadoId,
                        principalTable: "Videojuego",
                        principalColumn: "Videojuego_ID");
                    table.ForeignKey(
                        name: "FK_RecomendacionUsuario_Videojuego_Videojuego_ID",
                        column: x => x.Videojuego_ID,
                        principalTable: "Videojuego",
                        principalColumn: "Videojuego_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecomendacionVideojuego",
                columns: table => new
                {
                    RecomendacionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VideojuegoReferenciaId = table.Column<int>(type: "int", nullable: false),
                    VideojuegoRecomendadoId = table.Column<int>(type: "int", nullable: false),
                    Similitud = table.Column<double>(type: "float", nullable: false),
                    TipoRecomendacion = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    FechaRecomendacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecomendacionVideojuego", x => x.RecomendacionId);
                    table.ForeignKey(
                        name: "FK_RecomendacionVideojuego_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RecomendacionVideojuego_Videojuego_VideojuegoRecomendadoId",
                        column: x => x.VideojuegoRecomendadoId,
                        principalTable: "Videojuego",
                        principalColumn: "Videojuego_ID");
                    table.ForeignKey(
                        name: "FK_RecomendacionVideojuego_Videojuego_VideojuegoReferenciaId",
                        column: x => x.VideojuegoReferenciaId,
                        principalTable: "Videojuego",
                        principalColumn: "Videojuego_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecomendacionUsuario_UserId",
                table: "RecomendacionUsuario",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RecomendacionUsuario_usuarioId",
                table: "RecomendacionUsuario",
                column: "usuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_RecomendacionUsuario_Videojuego_ID",
                table: "RecomendacionUsuario",
                column: "Videojuego_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RecomendacionUsuario_VideojuegoRecomendadoId",
                table: "RecomendacionUsuario",
                column: "VideojuegoRecomendadoId");

            migrationBuilder.CreateIndex(
                name: "IX_RecomendacionVideojuego_UserId",
                table: "RecomendacionVideojuego",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RecomendacionVideojuego_VideojuegoRecomendadoId",
                table: "RecomendacionVideojuego",
                column: "VideojuegoRecomendadoId");

            migrationBuilder.CreateIndex(
                name: "IX_RecomendacionVideojuego_VideojuegoReferenciaId",
                table: "RecomendacionVideojuego",
                column: "VideojuegoReferenciaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecomendacionUsuario");

            migrationBuilder.DropTable(
                name: "RecomendacionVideojuego");

            migrationBuilder.DropColumn(
                name: "CaracteristicasVector",
                table: "Videojuego");

            migrationBuilder.DropColumn(
                name: "ClusterID",
                table: "Videojuego");

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
        }
    }
}
