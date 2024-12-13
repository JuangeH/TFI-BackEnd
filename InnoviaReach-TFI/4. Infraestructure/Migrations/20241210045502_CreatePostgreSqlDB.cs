using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace _4._Infraestructure.Migrations
{
    public partial class CreatePostgreSqlDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estilo",
                columns: table => new
                {
                    Estilo_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estilo", x => x.Estilo_ID);
                });

            migrationBuilder.CreateTable(
                name: "Genero",
                columns: table => new
                {
                    Genero_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GenreRawgID = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero", x => x.Genero_ID);
                });

            migrationBuilder.CreateTable(
                name: "LogTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Message = table.Column<string>(type: "text", nullable: true),
                    Level = table.Column<string>(type: "text", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Exception = table.Column<string>(type: "text", nullable: true),
                    LogEvent = table.Column<string>(type: "text", nullable: true),
                    ReferenceNumber = table.Column<int>(type: "integer", nullable: true),
                    ReferenceType = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plataforma",
                columns: table => new
                {
                    Plataforma_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlatformRawgID = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plataforma", x => x.Plataforma_ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suscripcion",
                columns: table => new
                {
                    Suscripcion_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suscripcion", x => x.Suscripcion_ID);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Tag_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TagRawgId = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Tag_ID);
                });

            migrationBuilder.CreateTable(
                name: "Tienda",
                columns: table => new
                {
                    Tienda_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StoreRawgId = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    Dominio = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tienda", x => x.Tienda_ID);
                });

            migrationBuilder.CreateTable(
                name: "TipoPago",
                columns: table => new
                {
                    TipoPago_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPago", x => x.TipoPago_ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CommunityBanned = table.Column<bool>(type: "boolean", nullable: false),
                    Idioma = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Videojuego",
                columns: table => new
                {
                    Videojuego_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppRawgId = table.Column<int>(type: "integer", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    FechaSalida = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Imagen = table.Column<string>(type: "text", nullable: true),
                    Rating = table.Column<double>(type: "double precision", nullable: false),
                    Metacritic = table.Column<int>(type: "integer", nullable: true),
                    ClusterID = table.Column<int>(type: "integer", nullable: true),
                    CaracteristicasVector = table.Column<string>(type: "text", nullable: true),
                    Descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videojuego", x => x.Videojuego_ID);
                });

            migrationBuilder.CreateTable(
                name: "PrivilegeClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivilegeClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrivilegeClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedioDePago",
                columns: table => new
                {
                    Medio_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cod_Postal = table.Column<int>(type: "integer", nullable: false),
                    Cod_Verificador = table.Column<int>(type: "integer", nullable: false),
                    Direccion = table.Column<string>(type: "varchar(50)", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    Numero = table.Column<int>(type: "integer", nullable: false),
                    TipoPago_ID = table.Column<int>(type: "integer", nullable: false),
                    User_ID = table.Column<string>(type: "text", nullable: false)
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
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Token = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SteamAccount",
                columns: table => new
                {
                    SteamAccount_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    steamid = table.Column<string>(type: "text", nullable: false),
                    ApiKey = table.Column<string>(type: "text", nullable: true),
                    personaname = table.Column<string>(type: "text", nullable: false),
                    avatarfull = table.Column<string>(type: "text", nullable: false),
                    profileurl = table.Column<string>(type: "text", nullable: false),
                    User_ID = table.Column<string>(type: "text", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "SuscripcionUsuario",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    User_ID = table.Column<string>(type: "text", nullable: false),
                    Suscripcion_ID = table.Column<int>(type: "integer", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogin_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioBaneado",
                columns: table => new
                {
                    Baneo_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    User_ID = table.Column<string>(type: "text", nullable: false),
                    UserAdmin_ID = table.Column<string>(type: "text", nullable: false),
                    Motivo = table.Column<string>(type: "text", nullable: false),
                    FechaDeBaneo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioBaneado", x => x.Baneo_ID);
                    table.ForeignKey(
                        name: "FK_UsuarioBaneado_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsuarioBaneado_Users_UserAdmin_ID",
                        column: x => x.UserAdmin_ID,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UsuarioJuegoPerfil",
                columns: table => new
                {
                    Perfil_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    User_ID = table.Column<string>(type: "text", nullable: false),
                    ClusterID = table.Column<int>(type: "integer", nullable: true),
                    TipoRecomendacion = table.Column<string>(type: "text", nullable: true),
                    GameGenresJson = table.Column<string>(type: "text", nullable: true),
                    GameTagsJson = table.Column<string>(type: "text", nullable: true),
                    GameHistoryJson = table.Column<string>(type: "text", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Adquisicion",
                columns: table => new
                {
                    Adquisicion_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    User_ID = table.Column<string>(type: "text", nullable: false),
                    Videojuego_ID = table.Column<int>(type: "integer", nullable: false),
                    TiempoJuego = table.Column<int>(type: "integer", nullable: false),
                    CantidadLogros = table.Column<int>(type: "integer", nullable: true),
                    TiempoJuegoReciente = table.Column<int>(type: "integer", nullable: true)
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
                name: "Foro",
                columns: table => new
                {
                    Foro_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "varchar(250)", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    User_ID = table.Column<string>(type: "text", nullable: false),
                    Videojuego_ID = table.Column<int>(type: "integer", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foro", x => x.Foro_ID);
                    table.ForeignKey(
                        name: "FK_Foro_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Foro_Videojuego_Videojuego_ID",
                        column: x => x.Videojuego_ID,
                        principalTable: "Videojuego",
                        principalColumn: "Videojuego_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Novedad",
                columns: table => new
                {
                    Novedad_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "varchar(50)", nullable: false),
                    Videojuego_ID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Novedad", x => x.Novedad_ID);
                    table.ForeignKey(
                        name: "FK_Novedad_Videojuego_Videojuego_ID",
                        column: x => x.Videojuego_ID,
                        principalTable: "Videojuego",
                        principalColumn: "Videojuego_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Rating_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Videojuego_ID = table.Column<int>(type: "integer", nullable: false),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    CantidadVotos = table.Column<int>(type: "integer", nullable: false),
                    Porcentaje = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.Rating_ID);
                    table.ForeignKey(
                        name: "FK_Rating_Videojuego_Videojuego_ID",
                        column: x => x.Videojuego_ID,
                        principalTable: "Videojuego",
                        principalColumn: "Videojuego_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecomendacionUsuario",
                columns: table => new
                {
                    RecomendacionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    VideojuegoRecomendadoId = table.Column<int>(type: "integer", nullable: false),
                    Frecuencia = table.Column<int>(type: "integer", nullable: false),
                    TipoRecomendacion = table.Column<string>(type: "text", nullable: false),
                    FechaRecomendacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                        name: "FK_RecomendacionUsuario_Videojuego_VideojuegoRecomendadoId",
                        column: x => x.VideojuegoRecomendadoId,
                        principalTable: "Videojuego",
                        principalColumn: "Videojuego_ID");
                });

            migrationBuilder.CreateTable(
                name: "RecomendacionVideojuego",
                columns: table => new
                {
                    RecomendacionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    VideojuegoReferenciaId = table.Column<int>(type: "integer", nullable: false),
                    VideojuegoRecomendadoId = table.Column<int>(type: "integer", nullable: false),
                    Similitud = table.Column<double>(type: "double precision", nullable: false),
                    TipoRecomendacion = table.Column<string>(type: "text", nullable: false),
                    FechaRecomendacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Reseña",
                columns: table => new
                {
                    Reseña_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "varchar(50)", nullable: false),
                    Videojuego_ID = table.Column<int>(type: "integer", nullable: false)
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
                name: "TiempoDeJuego",
                columns: table => new
                {
                    Tiempo_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CantidadMinutos = table.Column<int>(type: "integer", nullable: false),
                    User_ID = table.Column<string>(type: "text", nullable: false),
                    Videojuego_ID = table.Column<int>(type: "integer", nullable: false)
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
                name: "Trofeo",
                columns: table => new
                {
                    Trofeo_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "varchar(50)", nullable: false),
                    User_ID = table.Column<string>(type: "text", nullable: false),
                    Videojuego_ID = table.Column<int>(type: "integer", nullable: false)
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
                name: "UsuarioVisita",
                columns: table => new
                {
                    Visita_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    User_ID = table.Column<string>(type: "text", nullable: false),
                    Videojuego_ID = table.Column<int>(type: "integer", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioVisita", x => x.Visita_ID);
                    table.ForeignKey(
                        name: "FK_UsuarioVisita_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioVisita_Videojuego_Videojuego_ID",
                        column: x => x.Videojuego_ID,
                        principalTable: "Videojuego",
                        principalColumn: "Videojuego_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Valoracion",
                columns: table => new
                {
                    Valoracion_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Puntuacion = table.Column<int>(type: "integer", nullable: false),
                    Videojuego_ID = table.Column<int>(type: "integer", nullable: false)
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
                name: "VideojuegoEstilo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Estilo_ID = table.Column<int>(type: "integer", nullable: false),
                    Videojuego_ID = table.Column<int>(type: "integer", nullable: false)
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
                name: "VideojuegoGenero",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Genero_ID = table.Column<int>(type: "integer", nullable: false),
                    Videojuego_ID = table.Column<int>(type: "integer", nullable: false)
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
                name: "VideojuegoInteres",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    User_ID = table.Column<string>(type: "text", nullable: false),
                    Videojuego_ID = table.Column<int>(type: "integer", nullable: false)
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
                name: "VideojuegoPlataforma",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Plataforma_ID = table.Column<int>(type: "integer", nullable: false),
                    Videojuego_ID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideojuegoPlataforma", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VideojuegoPlataforma_Plataforma_Plataforma_ID",
                        column: x => x.Plataforma_ID,
                        principalTable: "Plataforma",
                        principalColumn: "Plataforma_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideojuegoPlataforma_Videojuego_Videojuego_ID",
                        column: x => x.Videojuego_ID,
                        principalTable: "Videojuego",
                        principalColumn: "Videojuego_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideojuegoTag",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tag_ID = table.Column<int>(type: "integer", nullable: false),
                    Videojuego_ID = table.Column<int>(type: "integer", nullable: false)
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
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tienda_ID = table.Column<int>(type: "integer", nullable: false),
                    Videojuego_ID = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Comentario",
                columns: table => new
                {
                    Comentario_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Contenido = table.Column<string>(type: "text", nullable: false),
                    Foro_ID = table.Column<int>(type: "integer", nullable: false),
                    User_ID = table.Column<string>(type: "text", nullable: false),
                    ComentarioPadre_ID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentario", x => x.Comentario_ID);
                    table.ForeignKey(
                        name: "FK_Comentario_Comentario_ComentarioPadre_ID",
                        column: x => x.ComentarioPadre_ID,
                        principalTable: "Comentario",
                        principalColumn: "Comentario_ID",
                        onDelete: ReferentialAction.Restrict);
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
                name: "ForoUsuarioFavorito",
                columns: table => new
                {
                    User_ID = table.Column<string>(type: "text", nullable: false),
                    Foro_ID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForoUsuarioFavorito", x => new { x.User_ID, x.Foro_ID });
                    table.ForeignKey(
                        name: "FK_ForoUsuarioFavorito_Foro_Foro_ID",
                        column: x => x.Foro_ID,
                        principalTable: "Foro",
                        principalColumn: "Foro_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ForoUsuarioFavorito_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ForoUsuarioVisita",
                columns: table => new
                {
                    User_ID = table.Column<string>(type: "text", nullable: false),
                    Foro_ID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForoUsuarioVisita", x => new { x.User_ID, x.Foro_ID });
                    table.ForeignKey(
                        name: "FK_ForoUsuarioVisita_Foro_Foro_ID",
                        column: x => x.Foro_ID,
                        principalTable: "Foro",
                        principalColumn: "Foro_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ForoUsuarioVisita_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Puntuacion",
                columns: table => new
                {
                    Puntuacion_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Comentario_ID = table.Column<int>(type: "integer", nullable: false),
                    Puntaje = table.Column<int>(type: "integer", nullable: false),
                    User_ID = table.Column<string>(type: "text", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Puntuacion_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adquisicion_User_ID",
                table: "Adquisicion",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Adquisicion_Videojuego_ID",
                table: "Adquisicion",
                column: "Videojuego_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_ComentarioPadre_ID",
                table: "Comentario",
                column: "ComentarioPadre_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_Foro_ID",
                table: "Comentario",
                column: "Foro_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_User_ID",
                table: "Comentario",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Foro_User_ID",
                table: "Foro",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Foro_Videojuego_ID",
                table: "Foro",
                column: "Videojuego_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ForoUsuarioFavorito_Foro_ID",
                table: "ForoUsuarioFavorito",
                column: "Foro_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ForoUsuarioVisita_Foro_ID",
                table: "ForoUsuarioVisita",
                column: "Foro_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MedioDePago_TipoPago_ID",
                table: "MedioDePago",
                column: "TipoPago_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MedioDePago_User_ID",
                table: "MedioDePago",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Novedad_Videojuego_ID",
                table: "Novedad",
                column: "Videojuego_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PrivilegeClaims_RoleId",
                table: "PrivilegeClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Puntuacion_Comentario_ID",
                table: "Puntuacion",
                column: "Comentario_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Puntuacion_User_ID",
                table: "Puntuacion",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_Videojuego_ID",
                table: "Rating",
                column: "Videojuego_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RecomendacionUsuario_UserId",
                table: "RecomendacionUsuario",
                column: "UserId");

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

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reseña_Videojuego_ID",
                table: "Reseña",
                column: "Videojuego_ID");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SteamAccount_User_ID",
                table: "SteamAccount",
                column: "User_ID",
                unique: true);

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
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserId",
                table: "UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioBaneado_User_ID",
                table: "UsuarioBaneado",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioBaneado_UserAdmin_ID",
                table: "UsuarioBaneado",
                column: "UserAdmin_ID");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioJuegoPerfil_User_ID",
                table: "UsuarioJuegoPerfil",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioVisita_User_ID",
                table: "UsuarioVisita",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioVisita_Videojuego_ID",
                table: "UsuarioVisita",
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

            migrationBuilder.CreateIndex(
                name: "IX_VideojuegoPlataforma_Plataforma_ID",
                table: "VideojuegoPlataforma",
                column: "Plataforma_ID");

            migrationBuilder.CreateIndex(
                name: "IX_VideojuegoPlataforma_Videojuego_ID",
                table: "VideojuegoPlataforma",
                column: "Videojuego_ID");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adquisicion");

            migrationBuilder.DropTable(
                name: "ForoUsuarioFavorito");

            migrationBuilder.DropTable(
                name: "ForoUsuarioVisita");

            migrationBuilder.DropTable(
                name: "LogTable");

            migrationBuilder.DropTable(
                name: "MedioDePago");

            migrationBuilder.DropTable(
                name: "Novedad");

            migrationBuilder.DropTable(
                name: "PrivilegeClaims");

            migrationBuilder.DropTable(
                name: "Puntuacion");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "RecomendacionUsuario");

            migrationBuilder.DropTable(
                name: "RecomendacionVideojuego");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Reseña");

            migrationBuilder.DropTable(
                name: "SteamAccount");

            migrationBuilder.DropTable(
                name: "SuscripcionUsuario");

            migrationBuilder.DropTable(
                name: "TiempoDeJuego");

            migrationBuilder.DropTable(
                name: "Trofeo");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "UsuarioBaneado");

            migrationBuilder.DropTable(
                name: "UsuarioJuegoPerfil");

            migrationBuilder.DropTable(
                name: "UsuarioVisita");

            migrationBuilder.DropTable(
                name: "Valoracion");

            migrationBuilder.DropTable(
                name: "VideojuegoEstilo");

            migrationBuilder.DropTable(
                name: "VideojuegoGenero");

            migrationBuilder.DropTable(
                name: "VideojuegoInteres");

            migrationBuilder.DropTable(
                name: "VideojuegoPlataforma");

            migrationBuilder.DropTable(
                name: "VideojuegoTag");

            migrationBuilder.DropTable(
                name: "VideojuegoTienda");

            migrationBuilder.DropTable(
                name: "TipoPago");

            migrationBuilder.DropTable(
                name: "Comentario");

            migrationBuilder.DropTable(
                name: "Suscripcion");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Estilo");

            migrationBuilder.DropTable(
                name: "Genero");

            migrationBuilder.DropTable(
                name: "Plataforma");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Tienda");

            migrationBuilder.DropTable(
                name: "Foro");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Videojuego");
        }
    }
}
