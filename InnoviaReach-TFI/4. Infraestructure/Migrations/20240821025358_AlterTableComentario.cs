using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4._Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableComentario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Comentario");

            migrationBuilder.AddColumn<int>(
                name: "ComentarioPadre_ID",
                table: "Comentario",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contenido",
                table: "Comentario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Comentario",
                type: "datetime",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_ComentarioPadre_ID",
                table: "Comentario",
                column: "ComentarioPadre_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentario_Comentario_ComentarioPadre_ID",
                table: "Comentario",
                column: "ComentarioPadre_ID",
                principalTable: "Comentario",
                principalColumn: "Comentario_ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentario_Comentario_ComentarioPadre_ID",
                table: "Comentario");

            migrationBuilder.DropIndex(
                name: "IX_Comentario_ComentarioPadre_ID",
                table: "Comentario");

            migrationBuilder.DropColumn(
                name: "ComentarioPadre_ID",
                table: "Comentario");

            migrationBuilder.DropColumn(
                name: "Contenido",
                table: "Comentario");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Comentario");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Comentario",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");
        }
    }
}
