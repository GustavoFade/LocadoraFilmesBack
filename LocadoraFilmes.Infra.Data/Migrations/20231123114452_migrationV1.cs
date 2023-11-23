using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LocadoraFilmes.Infra.Data.Migrations
{
    public partial class migrationV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cpf = table.Column<string>(type: "VARCHAR(14)", maxLength: 14, nullable: false),
                    Senha = table.Column<string>(type: "VARCHAR(350)", maxLength: 350, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                    table.UniqueConstraint("AK_Cliente_Cpf", x => x.Cpf);
                });

            migrationBuilder.CreateTable(
                name: "Filme",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(200)", maxLength: 200, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "GETDATE()"),
                    FlgAtivo = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filme", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genero",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "GETDATE()"),
                    FlgAtivo = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdFilmesLocacao = table.Column<int>(type: "INT", nullable: false),
                    CpfCliente = table.Column<string>(type: "VARCHAR(14)", maxLength: 14, nullable: false),
                    DataLocacao = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locacao_Cliente_CpfCliente",
                        column: x => x.CpfCliente,
                        principalTable: "Cliente",
                        principalColumn: "Cpf",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilmeGenero",
                columns: table => new
                {
                    IdFilme = table.Column<int>(type: "INT", nullable: false),
                    IdGenero = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmeGenero", x => new { x.IdFilme, x.IdGenero });
                    table.ForeignKey(
                        name: "FK_FilmeGenero_Filme_IdFilme",
                        column: x => x.IdFilme,
                        principalTable: "Filme",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmeGenero_Genero_IdGenero",
                        column: x => x.IdGenero,
                        principalTable: "Genero",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilmeLocacao",
                columns: table => new
                {
                    IdFilme = table.Column<int>(type: "INT", nullable: false),
                    IdLocacao = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmeLocacao", x => new { x.IdFilme, x.IdLocacao });
                    table.ForeignKey(
                        name: "FK_FilmeLocacao_Filme_IdFilme",
                        column: x => x.IdFilme,
                        principalTable: "Filme",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmeLocacao_Locacao_IdLocacao",
                        column: x => x.IdLocacao,
                        principalTable: "Locacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cpf_Cliente",
                table: "Cliente",
                column: "Cpf");

            migrationBuilder.CreateIndex(
                name: "IX_Id_Cliente",
                table: "Cliente",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Id_Filme",
                table: "Filme",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FilmeGenero_IdGenero",
                table: "FilmeGenero",
                column: "IdGenero");

            migrationBuilder.CreateIndex(
                name: "IX_FilmeLocacao_IdLocacao",
                table: "FilmeLocacao",
                column: "IdLocacao");

            migrationBuilder.CreateIndex(
                name: "IX_Id_Genero",
                table: "Genero",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Id_Locacao",
                table: "Locacao",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Locacao_CpfCliente",
                table: "Locacao",
                column: "CpfCliente",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilmeGenero");

            migrationBuilder.DropTable(
                name: "FilmeLocacao");

            migrationBuilder.DropTable(
                name: "Genero");

            migrationBuilder.DropTable(
                name: "Filme");

            migrationBuilder.DropTable(
                name: "Locacao");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
