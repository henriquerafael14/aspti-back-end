﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Aspti.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Sistema");

            migrationBuilder.EnsureSchema(
                name: "Identidade");

            migrationBuilder.CreateTable(
                name: "Auditoria",
                schema: "Sistema",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DataAuditoria = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TipoAuditoria = table.Column<int>(type: "integer", nullable: false),
                    IdEntidade = table.Column<string>(type: "text", nullable: true),
                    NomeEntidade = table.Column<string>(type: "text", nullable: true),
                    NomeTabela = table.Column<string>(type: "text", nullable: true),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: true),
                    Usuario = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoriaTela",
                schema: "Sistema",
                columns: table => new
                {
                    DataCriacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    Ordem = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaTela", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                schema: "Sistema",
                columns: table => new
                {
                    DataCriacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Logradouro = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Numero = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CEP = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    Complemento = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Bairro = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CidadeNome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    EstadoNome = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CidadeCodigoIBGE = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Perfil",
                schema: "Identidade",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Administrador = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfil", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditoriaPropriedade",
                schema: "Sistema",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NomeDaPropriedade = table.Column<string>(type: "text", nullable: true),
                    ValorAntigo = table.Column<string>(type: "text", nullable: true),
                    ValorNovo = table.Column<string>(type: "text", nullable: true),
                    ChavePrimaria = table.Column<bool>(type: "boolean", nullable: false),
                    AuditoriaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditoriaPropriedade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditoriaPropriedade_Auditoria_AuditoriaId",
                        column: x => x.AuditoriaId,
                        principalSchema: "Sistema",
                        principalTable: "Auditoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tela",
                schema: "Sistema",
                columns: table => new
                {
                    DataCriacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "varchar(24)", nullable: false),
                    Icone = table.Column<string>(type: "text", nullable: true),
                    Ordem = table.Column<int>(type: "integer", nullable: false),
                    CategoriaTelaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tela", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tela_CategoriaTela_CategoriaTelaId",
                        column: x => x.CategoriaTelaId,
                        principalSchema: "Sistema",
                        principalTable: "CategoriaTela",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                schema: "Identidade",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Sobrenome = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    CPFCNPJ = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Image = table.Column<byte[]>(type: "bytea", nullable: true),
                    Status = table.Column<string>(type: "varchar", maxLength: 24, nullable: false),
                    EnderecoId = table.Column<Guid>(type: "uuid", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
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
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalSchema: "Sistema",
                        principalTable: "Endereco",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PerfilPermissao",
                schema: "Identidade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilPermissao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerfilPermissao_Perfil_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identidade",
                        principalTable: "Perfil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioLogin",
                schema: "Identidade",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UsuarioLogin_Usuario_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identidade",
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioPerfil",
                schema: "Identidade",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioPerfil", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UsuarioPerfil_Perfil_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identidade",
                        principalTable: "Perfil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioPerfil_Usuario_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identidade",
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioPerfil_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "Identidade",
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UsuarioPermissao",
                schema: "Identidade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioPermissao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioPermissao_Usuario_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identidade",
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioToken",
                schema: "Identidade",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UsuarioToken_Usuario_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identidade",
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditoriaPropriedade_AuditoriaId",
                schema: "Sistema",
                table: "AuditoriaPropriedade",
                column: "AuditoriaId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Identidade",
                table: "Perfil",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PerfilPermissao_RoleId",
                schema: "Identidade",
                table: "PerfilPermissao",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Tela_CategoriaTelaId",
                schema: "Sistema",
                table: "Tela",
                column: "CategoriaTelaId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Identidade",
                table: "Usuario",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_EnderecoId",
                schema: "Identidade",
                table: "Usuario",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Identidade",
                table: "Usuario",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioLogin_UserId",
                schema: "Identidade",
                table: "UsuarioLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioPerfil_RoleId",
                schema: "Identidade",
                table: "UsuarioPerfil",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioPerfil_UsuarioId",
                schema: "Identidade",
                table: "UsuarioPerfil",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioPermissao_UserId",
                schema: "Identidade",
                table: "UsuarioPermissao",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditoriaPropriedade",
                schema: "Sistema");

            migrationBuilder.DropTable(
                name: "PerfilPermissao",
                schema: "Identidade");

            migrationBuilder.DropTable(
                name: "Tela",
                schema: "Sistema");

            migrationBuilder.DropTable(
                name: "UsuarioLogin",
                schema: "Identidade");

            migrationBuilder.DropTable(
                name: "UsuarioPerfil",
                schema: "Identidade");

            migrationBuilder.DropTable(
                name: "UsuarioPermissao",
                schema: "Identidade");

            migrationBuilder.DropTable(
                name: "UsuarioToken",
                schema: "Identidade");

            migrationBuilder.DropTable(
                name: "Auditoria",
                schema: "Sistema");

            migrationBuilder.DropTable(
                name: "CategoriaTela",
                schema: "Sistema");

            migrationBuilder.DropTable(
                name: "Perfil",
                schema: "Identidade");

            migrationBuilder.DropTable(
                name: "Usuario",
                schema: "Identidade");

            migrationBuilder.DropTable(
                name: "Endereco",
                schema: "Sistema");
        }
    }
}
