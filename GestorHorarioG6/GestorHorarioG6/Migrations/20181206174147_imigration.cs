using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestorHorarioG6.Migrations
{
    public partial class imigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bloco",
                columns: table => new
                {
                    BlocoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bloco", x => x.BlocoId);
                });

            migrationBuilder.CreateTable(
                name: "Cargo",
                columns: table => new
                {
                    CargoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargo", x => x.CargoId);
                });

            migrationBuilder.CreateTable(
                name: "Departamento",
                columns: table => new
                {
                    DepartamentoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamento", x => x.DepartamentoId);
                });

            migrationBuilder.CreateTable(
                name: "Servico",
                columns: table => new
                {
                    ServicoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 60, nullable: false),
                    Descrição = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servico", x => x.ServicoId);
                });

            migrationBuilder.CreateTable(
                name: "Equipamento",
                columns: table => new
                {
                    EquipamentoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    BlocoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipamento", x => x.EquipamentoId);
                    table.ForeignKey(
                        name: "FK_Equipamento_Bloco_BlocoId",
                        column: x => x.BlocoId,
                        principalTable: "Bloco",
                        principalColumn: "BlocoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Funcionario",
                columns: table => new
                {
                    FuncionarioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 60, nullable: false),
                    CargoId = table.Column<int>(nullable: false),
                    Nascimento = table.Column<DateTime>(nullable: false),
                    NascimentoFilho = table.Column<DateTime>(nullable: false),
                    NIF = table.Column<string>(nullable: false),
                    Telefone = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Notas = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionario", x => x.FuncionarioId);
                    table.ForeignKey(
                        name: "FK_Funcionario_Cargo_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargo",
                        principalColumn: "CargoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requisicao",
                columns: table => new
                {
                    RequisicaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DepartamentoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requisicao", x => x.RequisicaoId);
                    table.ForeignKey(
                        name: "FK_Requisicao_Departamento_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamento",
                        principalColumn: "DepartamentoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequisicaoEquipamento",
                columns: table => new
                {
                    RequisicaoEquipamentoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EquipamentoId = table.Column<int>(nullable: false),
                    HoraDeInicio = table.Column<DateTime>(nullable: false),
                    HoraDeFim = table.Column<DateTime>(nullable: false),
                    BlocoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequisicaoEquipamento", x => x.RequisicaoEquipamentoId);
                    table.ForeignKey(
                        name: "FK_RequisicaoEquipamento_Bloco_BlocoId",
                        column: x => x.BlocoId,
                        principalTable: "Bloco",
                        principalColumn: "BlocoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequisicaoEquipamento_Equipamento_EquipamentoId",
                        column: x => x.EquipamentoId,
                        principalTable: "Equipamento",
                        principalColumn: "EquipamentoId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "RequisicaoDetalhe",
                columns: table => new
                {
                    RequisicaoDetalheId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RequisicaoId = table.Column<int>(nullable: false),
                    ServicoId = table.Column<int>(nullable: false),
                    HoraDeInicio = table.Column<DateTime>(nullable: false),
                    DuraçãoEstimada = table.Column<DateTime>(nullable: false),
                    Concluido = table.Column<DateTime>(nullable: false),
                    Notas = table.Column<string>(nullable: true),
                    Aprovado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequisicaoDetalhe", x => x.RequisicaoDetalheId);
                    table.ForeignKey(
                        name: "FK_RequisicaoDetalhe_Requisicao_RequisicaoId",
                        column: x => x.RequisicaoId,
                        principalTable: "Requisicao",
                        principalColumn: "RequisicaoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequisicaoDetalhe_Servico_ServicoId",
                        column: x => x.ServicoId,
                        principalTable: "Servico",
                        principalColumn: "ServicoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipamento_BlocoId",
                table: "Equipamento",
                column: "BlocoId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_CargoId",
                table: "Funcionario",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_Requisicao_DepartamentoId",
                table: "Requisicao",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisicaoDetalhe_RequisicaoId",
                table: "RequisicaoDetalhe",
                column: "RequisicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisicaoDetalhe_ServicoId",
                table: "RequisicaoDetalhe",
                column: "ServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisicaoEquipamento_BlocoId",
                table: "RequisicaoEquipamento",
                column: "BlocoId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisicaoEquipamento_EquipamentoId",
                table: "RequisicaoEquipamento",
                column: "EquipamentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Funcionario");

            migrationBuilder.DropTable(
                name: "RequisicaoDetalhe");

            migrationBuilder.DropTable(
                name: "RequisicaoEquipamento");

            migrationBuilder.DropTable(
                name: "Cargo");

            migrationBuilder.DropTable(
                name: "Requisicao");

            migrationBuilder.DropTable(
                name: "Servico");

            migrationBuilder.DropTable(
                name: "Equipamento");

            migrationBuilder.DropTable(
                name: "Departamento");

            migrationBuilder.DropTable(
                name: "Bloco");
        }
    }
}
