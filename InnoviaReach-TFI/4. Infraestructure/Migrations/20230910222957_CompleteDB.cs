using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4._Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class CompleteDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estilo_preferido",
                table: "Users",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Genero_preferido",
                table: "Users",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Idioma",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Notificacion_ID",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Adquisicion",
                columns: table => new
                {
                    Adquisicion_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(50)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Videojuego_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adquisicion", x => x.Adquisicion_id);
                    table.ForeignKey(
                        name: "FK_Adquisicion_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Adquisicion_Videojuego_Videojuego_ID",
                        column: x => x.Videojuego_ID,
                        principalTable: "Videojuego",
                        principalColumn: "Videojuego_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estilo",
                columns: table => new
                {
                    Estilo_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estilo", x => x.Estilo_ID);
                });

            migrationBuilder.CreateTable(
                name: "Foro",
                columns: table => new
                {
                    Foro_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(50)", nullable: false),
                    Videojuego_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foro", x => x.Foro_ID);
                    table.ForeignKey(
                        name: "FK_Foro_Videojuego_Videojuego_ID",
                        column: x => x.Videojuego_ID,
                        principalTable: "Videojuego",
                        principalColumn: "Videojuego_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Genero",
                columns: table => new
                {
                    Genero_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero", x => x.Genero_ID);
                });

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

            migrationBuilder.CreateTable(
                name: "Reseña",
                columns: table => new
                {
                    Reseña_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(50)", nullable: false),
                    Videojuego_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reseña", x => x.Reseña_ID);
                    table.ForeignKey(
                        name: "FK_Reseña_Videojuego_Videojuego_ID",
                        column: x => x.Videojuego_ID,
                        principalTable: "Videojuego",
                        principalColumn: "Videojuego_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suscripcion",
                columns: table => new
                {
                    Suscripcion_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suscripcion", x => x.Suscripcion_ID);
                });

            migrationBuilder.CreateTable(
                name: "TiempoDeJuego",
                columns: table => new
                {
                    Tiempo_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CantidadHoras = table.Column<int>(type: "int", nullable: false),
                    UltimaFecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Videojuego_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiempoDeJuego", x => x.Tiempo_ID);
                    table.ForeignKey(
                        name: "FK_TiempoDeJuego_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TiempoDeJuego_Videojuego_Videojuego_ID",
                        column: x => x.Videojuego_ID,
                        principalTable: "Videojuego",
                        principalColumn: "Videojuego_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TipoPago",
                columns: table => new
                {
                    TipoPago_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPago", x => x.TipoPago_ID);
                });

            migrationBuilder.CreateTable(
                name: "Trofeo",
                columns: table => new
                {
                    Trofeo_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(50)", nullable: false),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Videojuego_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trofeo", x => x.Trofeo_ID);
                    table.ForeignKey(
                        name: "FK_Trofeo_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trofeo_Videojuego_Videojuego_ID",
                        column: x => x.Videojuego_ID,
                        principalTable: "Videojuego",
                        principalColumn: "Videojuego_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Valoracion",
                columns: table => new
                {
                    Valoracion_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Puntuacion = table.Column<int>(type: "int", nullable: false),
                    Videojuego_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Valoracion", x => x.Valoracion_ID);
                    table.ForeignKey(
                        name: "FK_Valoracion_Videojuego_Videojuego_ID",
                        column: x => x.Videojuego_ID,
                        principalTable: "Videojuego",
                        principalColumn: "Videojuego_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideojuegoInteres",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Videojuego_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideojuegoInteres", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VideojuegoInteres_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideojuegoInteres_Videojuego_Videojuego_ID",
                        column: x => x.Videojuego_ID,
                        principalTable: "Videojuego",
                        principalColumn: "Videojuego_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideojuegoEstilo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estilo_ID = table.Column<int>(type: "int", nullable: false),
                    Videojuego_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideojuegoEstilo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VideojuegoEstilo_Estilo_Estilo_ID",
                        column: x => x.Estilo_ID,
                        principalTable: "Estilo",
                        principalColumn: "Estilo_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideojuegoEstilo_Videojuego_Videojuego_ID",
                        column: x => x.Videojuego_ID,
                        principalTable: "Videojuego",
                        principalColumn: "Videojuego_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comentario",
                columns: table => new
                {
                    Comentario_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(50)", nullable: false),
                    Foro_ID = table.Column<int>(type: "int", nullable: false),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentario", x => x.Comentario_ID);
                    table.ForeignKey(
                        name: "FK_Comentario_Foro_Foro_ID",
                        column: x => x.Foro_ID,
                        principalTable: "Foro",
                        principalColumn: "Foro_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comentario_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ForoUsuario",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Foro_ID = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForoUsuario", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ForoUsuario_Foro_Foro_ID",
                        column: x => x.Foro_ID,
                        principalTable: "Foro",
                        principalColumn: "Foro_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ForoUsuario_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideojuegoGenero",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Genero_ID = table.Column<int>(type: "int", nullable: false),
                    Videojuego_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideojuegoGenero", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VideojuegoGenero_Genero_Genero_ID",
                        column: x => x.Genero_ID,
                        principalTable: "Genero",
                        principalColumn: "Genero_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideojuegoGenero_Videojuego_Videojuego_ID",
                        column: x => x.Videojuego_ID,
                        principalTable: "Videojuego",
                        principalColumn: "Videojuego_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SuscripcionUsuario",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Suscripcion_ID = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuscripcionUsuario", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SuscripcionUsuario_Suscripcion_Suscripcion_ID",
                        column: x => x.Suscripcion_ID,
                        principalTable: "Suscripcion",
                        principalColumn: "Suscripcion_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SuscripcionUsuario_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedioDePago",
                columns: table => new
                {
                    Medio_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cod_Postal = table.Column<int>(type: "int", nullable: false),
                    Cod_Verificador = table.Column<int>(type: "int", nullable: false),
                    Direccion = table.Column<string>(type: "varchar(50)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    TipoPago_ID = table.Column<int>(type: "int", nullable: false),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedioDePago", x => x.Medio_ID);
                    table.ForeignKey(
                        name: "FK_MedioDePago_TipoPago_TipoPago_ID",
                        column: x => x.TipoPago_ID,
                        principalTable: "TipoPago",
                        principalColumn: "TipoPago_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedioDePago_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Puntuacion",
                columns: table => new
                {
                    Puntuacion_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comentario_ID = table.Column<int>(type: "int", nullable: false),
                    Puntaje = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Puntuacion", x => x.Puntuacion_ID);
                    table.ForeignKey(
                        name: "FK_Puntuacion_Comentario_Comentario_ID",
                        column: x => x.Comentario_ID,
                        principalTable: "Comentario",
                        principalColumn: "Comentario_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Notificacion_ID",
                table: "Users",
                column: "Notificacion_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Adquisicion_User_ID",
                table: "Adquisicion",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Adquisicion_Videojuego_ID",
                table: "Adquisicion",
                column: "Videojuego_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_Foro_ID",
                table: "Comentario",
                column: "Foro_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_User_ID",
                table: "Comentario",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Foro_Videojuego_ID",
                table: "Foro",
                column: "Videojuego_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ForoUsuario_Foro_ID",
                table: "ForoUsuario",
                column: "Foro_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ForoUsuario_User_ID",
                table: "ForoUsuario",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MedioDePago_TipoPago_ID",
                table: "MedioDePago",
                column: "TipoPago_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MedioDePago_User_ID",
                table: "MedioDePago",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Puntuacion_Comentario_ID",
                table: "Puntuacion",
                column: "Comentario_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Reseña_Videojuego_ID",
                table: "Reseña",
                column: "Videojuego_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SuscripcionUsuario_Suscripcion_ID",
                table: "SuscripcionUsuario",
                column: "Suscripcion_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SuscripcionUsuario_User_ID",
                table: "SuscripcionUsuario",
                column: "User_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TiempoDeJuego_User_ID",
                table: "TiempoDeJuego",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TiempoDeJuego_Videojuego_ID",
                table: "TiempoDeJuego",
                column: "Videojuego_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Trofeo_User_ID",
                table: "Trofeo",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Trofeo_Videojuego_ID",
                table: "Trofeo",
                column: "Videojuego_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Valoracion_Videojuego_ID",
                table: "Valoracion",
                column: "Videojuego_ID");

            migrationBuilder.CreateIndex(
                name: "IX_VideojuegoEstilo_Estilo_ID",
                table: "VideojuegoEstilo",
                column: "Estilo_ID");

            migrationBuilder.CreateIndex(
                name: "IX_VideojuegoEstilo_Videojuego_ID",
                table: "VideojuegoEstilo",
                column: "Videojuego_ID");

            migrationBuilder.CreateIndex(
                name: "IX_VideojuegoGenero_Genero_ID",
                table: "VideojuegoGenero",
                column: "Genero_ID");

            migrationBuilder.CreateIndex(
                name: "IX_VideojuegoGenero_Videojuego_ID",
                table: "VideojuegoGenero",
                column: "Videojuego_ID");

            migrationBuilder.CreateIndex(
                name: "IX_VideojuegoInteres_User_ID",
                table: "VideojuegoInteres",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_VideojuegoInteres_Videojuego_ID",
                table: "VideojuegoInteres",
                column: "Videojuego_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Notificacion_Notificacion_ID",
                table: "Users",
                column: "Notificacion_ID",
                principalTable: "Notificacion",
                principalColumn: "Notificacion_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Notificacion_Notificacion_ID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Adquisicion");

            migrationBuilder.DropTable(
                name: "ForoUsuario");

            migrationBuilder.DropTable(
                name: "MedioDePago");

            migrationBuilder.DropTable(
                name: "Notificacion");

            migrationBuilder.DropTable(
                name: "Puntuacion");

            migrationBuilder.DropTable(
                name: "Reseña");

            migrationBuilder.DropTable(
                name: "SuscripcionUsuario");

            migrationBuilder.DropTable(
                name: "TiempoDeJuego");

            migrationBuilder.DropTable(
                name: "Trofeo");

            migrationBuilder.DropTable(
                name: "Valoracion");

            migrationBuilder.DropTable(
                name: "VideojuegoEstilo");

            migrationBuilder.DropTable(
                name: "VideojuegoGenero");

            migrationBuilder.DropTable(
                name: "VideojuegoInteres");

            migrationBuilder.DropTable(
                name: "TipoPago");

            migrationBuilder.DropTable(
                name: "Comentario");

            migrationBuilder.DropTable(
                name: "Suscripcion");

            migrationBuilder.DropTable(
                name: "Estilo");

            migrationBuilder.DropTable(
                name: "Genero");

            migrationBuilder.DropTable(
                name: "Foro");

            migrationBuilder.DropIndex(
                name: "IX_Users_Notificacion_ID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Estilo_preferido",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Genero_preferido",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Idioma",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Notificacion_ID",
                table: "Users");
        }
    }
}
