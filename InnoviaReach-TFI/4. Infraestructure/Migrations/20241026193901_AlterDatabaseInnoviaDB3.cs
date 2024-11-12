using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4._Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterDatabaseInnoviaDB3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videojuego_Plataforma_Plataforma_ID",
                table: "Videojuego");

            migrationBuilder.DropIndex(
                name: "IX_Videojuego_Plataforma_ID",
                table: "Videojuego");

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
                name: "Plataforma_ID",
                table: "Videojuego");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Genero");

            migrationBuilder.RenameColumn(
                name: "SteamAppid",
                table: "Videojuego",
                newName: "AppRawgId");

            migrationBuilder.RenameColumn(
                name: "Recomendaciones",
                table: "Videojuego",
                newName: "Metacritic");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaSalida",
                table: "Videojuego",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Videojuego",
                type: "varchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Videojuego",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Videojuego",
                type: "varchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Plataforma",
                type: "varchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AddColumn<int>(
                name: "PlatformRawgID",
                table: "Plataforma",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Plataforma",
                type: "varchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GenreRawgID",
                table: "Genero",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Genero",
                type: "varchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Genero",
                type: "varchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Rating_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "varchar(max)", nullable: false),
                    CantidadVotos = table.Column<int>(type: "int", nullable: false),
                    Porcentaje = table.Column<double>(type: "float", nullable: false),
                    VideojuegoModelVideojuego_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.Rating_ID);
                    table.ForeignKey(
                        name: "FK_Rating_Videojuego_VideojuegoModelVideojuego_ID",
                        column: x => x.VideojuegoModelVideojuego_ID,
                        principalTable: "Videojuego",
                        principalColumn: "Videojuego_ID");
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Tag_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagRawgId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Tag_ID);
                });

            migrationBuilder.CreateTable(
                name: "Tienda",
                columns: table => new
                {
                    Tienda_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreRawgId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "varchar(max)", nullable: false),
                    Dominio = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tienda", x => x.Tienda_ID);
                });

            migrationBuilder.CreateTable(
                name: "VideojuegoPlataformaModel",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Plataforma_ID = table.Column<int>(type: "int", nullable: false),
                    Videojuego_ID = table.Column<int>(type: "int", nullable: false),
                    Videojuego_ID1 = table.Column<int>(type: "int", nullable: true),
                    plataformaModelPlataforma_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideojuegoPlataformaModel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VideojuegoPlataformaModel_Plataforma_plataformaModelPlataforma_ID",
                        column: x => x.plataformaModelPlataforma_ID,
                        principalTable: "Plataforma",
                        principalColumn: "Plataforma_ID");
                    table.ForeignKey(
                        name: "FK_VideojuegoPlataformaModel_Videojuego_Videojuego_ID1",
                        column: x => x.Videojuego_ID1,
                        principalTable: "Videojuego",
                        principalColumn: "Videojuego_ID");
                });

            migrationBuilder.CreateTable(
                name: "VideojuegoTag",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tag_ID = table.Column<int>(type: "int", nullable: false),
                    Videojuego_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideojuegoTag", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VideojuegoTag_Tag_Tag_ID",
                        column: x => x.Tag_ID,
                        principalTable: "Tag",
                        principalColumn: "Tag_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideojuegoTag_Videojuego_Videojuego_ID",
                        column: x => x.Videojuego_ID,
                        principalTable: "Videojuego",
                        principalColumn: "Videojuego_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideojuegoTienda",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tienda_ID = table.Column<int>(type: "int", nullable: false),
                    Videojuego_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideojuegoTienda", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VideojuegoTienda_Tienda_Tienda_ID",
                        column: x => x.Tienda_ID,
                        principalTable: "Tienda",
                        principalColumn: "Tienda_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideojuegoTienda_Videojuego_Videojuego_ID",
                        column: x => x.Videojuego_ID,
                        principalTable: "Videojuego",
                        principalColumn: "Videojuego_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rating_VideojuegoModelVideojuego_ID",
                table: "Rating",
                column: "VideojuegoModelVideojuego_ID");

            migrationBuilder.CreateIndex(
                name: "IX_VideojuegoPlataformaModel_plataformaModelPlataforma_ID",
                table: "VideojuegoPlataformaModel",
                column: "plataformaModelPlataforma_ID");

            migrationBuilder.CreateIndex(
                name: "IX_VideojuegoPlataformaModel_Videojuego_ID1",
                table: "VideojuegoPlataformaModel",
                column: "Videojuego_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_VideojuegoTag_Tag_ID",
                table: "VideojuegoTag",
                column: "Tag_ID");

            migrationBuilder.CreateIndex(
                name: "IX_VideojuegoTag_Videojuego_ID",
                table: "VideojuegoTag",
                column: "Videojuego_ID");

            migrationBuilder.CreateIndex(
                name: "IX_VideojuegoTienda_Tienda_ID",
                table: "VideojuegoTienda",
                column: "Tienda_ID");

            migrationBuilder.CreateIndex(
                name: "IX_VideojuegoTienda_Videojuego_ID",
                table: "VideojuegoTienda",
                column: "Videojuego_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "VideojuegoPlataformaModel");

            migrationBuilder.DropTable(
                name: "VideojuegoTag");

            migrationBuilder.DropTable(
                name: "VideojuegoTienda");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Tienda");

            migrationBuilder.DropColumn(
                name: "FechaSalida",
                table: "Videojuego");

            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Videojuego");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Videojuego");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Videojuego");

            migrationBuilder.DropColumn(
                name: "PlatformRawgID",
                table: "Plataforma");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Plataforma");

            migrationBuilder.DropColumn(
                name: "GenreRawgID",
                table: "Genero");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Genero");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Genero");

            migrationBuilder.RenameColumn(
                name: "Metacritic",
                table: "Videojuego",
                newName: "Recomendaciones");

            migrationBuilder.RenameColumn(
                name: "AppRawgId",
                table: "Videojuego",
                newName: "SteamAppid");

            migrationBuilder.AddColumn<string>(
                name: "Header_image",
                table: "Videojuego",
                type: "varchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Metacritic_score",
                table: "Videojuego",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Metacritic_url",
                table: "Videojuego",
                type: "varchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Plataforma_ID",
                table: "Videojuego",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Plataforma",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Genero",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Videojuego_Plataforma_ID",
                table: "Videojuego",
                column: "Plataforma_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Videojuego_Plataforma_Plataforma_ID",
                table: "Videojuego",
                column: "Plataforma_ID",
                principalTable: "Plataforma",
                principalColumn: "Plataforma_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
