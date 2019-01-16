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
                name: "Estado",
                columns: table => new
                {
                    EstadoTrocaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.EstadoTrocaId);
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
                name: "Turno",
                columns: table => new
                {
                    TurnoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    HoraInicioManha = table.Column<int>(nullable: false),
                    HoraFimManha = table.Column<int>(nullable: false),
                    HoraInicioTarde = table.Column<int>(nullable: false),
                    HoraFimTarde = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turno", x => x.TurnoId);
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
                    NascimentoFilho = table.Column<DateTime>(nullable: true),
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
                    DepartamentoId = table.Column<int>(nullable: false),
                    HoraDeInicio = table.Column<DateTime>(nullable: false),
                    HoraDeFim = table.Column<DateTime>(nullable: false),
                    RequisicoesAdicionais = table.Column<string>(nullable: true),
                    Aprovado = table.Column<bool>(nullable: false)
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HorarioTecnicos",
                columns: table => new
                {
                    HorarioTecnicoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataInicioManha = table.Column<DateTime>(nullable: false),
                    DataFimManha = table.Column<DateTime>(nullable: false),
                    DataInicioTarde = table.Column<DateTime>(nullable: false),
                    DataFimTarde = table.Column<DateTime>(nullable: false),
                    TurnoId = table.Column<int>(nullable: false),
                    FuncionarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioTecnicos", x => x.HorarioTecnicoId);
                    table.ForeignKey(
                        name: "FK_HorarioTecnicos_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "FuncionarioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HorarioTecnicos_Turno_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turno",
                        principalColumn: "TurnoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HorarioATrocar",
                columns: table => new
                {
                    HorarioATrocarId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HorarioFuncionarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioATrocar", x => x.HorarioATrocarId);
                    table.ForeignKey(
                        name: "FK_HorarioATrocar_HorarioTecnicos_HorarioFuncionarioId",
                        column: x => x.HorarioFuncionarioId,
                        principalTable: "HorarioTecnicos",
                        principalColumn: "HorarioTecnicoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HorarioParaTroca",
                columns: table => new
                {
                    HorarioParaTrocaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HorarioFucnionarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioParaTroca", x => x.HorarioParaTrocaId);
                    table.ForeignKey(
                        name: "FK_HorarioParaTroca_HorarioTecnicos_HorarioFucnionarioId",
                        column: x => x.HorarioFucnionarioId,
                        principalTable: "HorarioTecnicos",
                        principalColumn: "HorarioTecnicoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trocas",
                columns: table => new
                {
                    TrocasID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FuncionarioId = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    HorarioATrocarId = table.Column<int>(nullable: false),
                    HorarioParaTrocaId = table.Column<int>(nullable: false),
                    EstadoTrocaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trocas", x => x.TrocasID);
                    table.ForeignKey(
                        name: "FK_Trocas_Estado_EstadoTrocaId",
                        column: x => x.EstadoTrocaId,
                        principalTable: "Estado",
                        principalColumn: "EstadoTrocaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trocas_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "FuncionarioId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trocas_HorarioATrocar_HorarioATrocarId",
                        column: x => x.HorarioATrocarId,
                        principalTable: "HorarioATrocar",
                        principalColumn: "HorarioATrocarId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trocas_HorarioParaTroca_HorarioParaTrocaId",
                        column: x => x.HorarioParaTrocaId,
                        principalTable: "HorarioParaTroca",
                        principalColumn: "HorarioParaTrocaId",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_HorarioATrocar_HorarioFuncionarioId",
                table: "HorarioATrocar",
                column: "HorarioFuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_HorarioParaTroca_HorarioFucnionarioId",
                table: "HorarioParaTroca",
                column: "HorarioFucnionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_HorarioTecnicos_FuncionarioId",
                table: "HorarioTecnicos",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_HorarioTecnicos_TurnoId",
                table: "HorarioTecnicos",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Requisicao_DepartamentoId",
                table: "Requisicao",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisicaoEquipamento_BlocoId",
                table: "RequisicaoEquipamento",
                column: "BlocoId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisicaoEquipamento_EquipamentoId",
                table: "RequisicaoEquipamento",
                column: "EquipamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Trocas_EstadoTrocaId",
                table: "Trocas",
                column: "EstadoTrocaId");

            migrationBuilder.CreateIndex(
                name: "IX_Trocas_FuncionarioId",
                table: "Trocas",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Trocas_HorarioATrocarId",
                table: "Trocas",
                column: "HorarioATrocarId");

            migrationBuilder.CreateIndex(
                name: "IX_Trocas_HorarioParaTrocaId",
                table: "Trocas",
                column: "HorarioParaTrocaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requisicao");

            migrationBuilder.DropTable(
                name: "RequisicaoEquipamento");

            migrationBuilder.DropTable(
                name: "Servico");

            migrationBuilder.DropTable(
                name: "Trocas");

            migrationBuilder.DropTable(
                name: "Departamento");

            migrationBuilder.DropTable(
                name: "Equipamento");

            migrationBuilder.DropTable(
                name: "Estado");

            migrationBuilder.DropTable(
                name: "HorarioATrocar");

            migrationBuilder.DropTable(
                name: "HorarioParaTroca");

            migrationBuilder.DropTable(
                name: "Bloco");

            migrationBuilder.DropTable(
                name: "HorarioTecnicos");

            migrationBuilder.DropTable(
                name: "Funcionario");

            migrationBuilder.DropTable(
                name: "Turno");

            migrationBuilder.DropTable(
                name: "Cargo");
        }
    }
}
