using Microsoft.EntityFrameworkCore.Migrations;

namespace GestorHorarioG6.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDFuncionario1",
                table: "Trocas");

            migrationBuilder.RenameColumn(
                name: "IDFuncionario2",
                table: "Trocas",
                newName: "IDFuncionario1FuncionarioID");

            migrationBuilder.AddColumn<int>(
                name: "IDFuncionario2FuncionarioID",
                table: "Trocas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trocas_IDFuncionario1FuncionarioID",
                table: "Trocas",
                column: "IDFuncionario1FuncionarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Trocas_IDFuncionario2FuncionarioID",
                table: "Trocas",
                column: "IDFuncionario2FuncionarioID");

            migrationBuilder.AddForeignKey(
                name: "FK_Trocas_Funcionario_IDFuncionario1FuncionarioID",
                table: "Trocas",
                column: "IDFuncionario1FuncionarioID",
                principalTable: "Funcionario",
                principalColumn: "FuncionarioID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trocas_Funcionario_IDFuncionario2FuncionarioID",
                table: "Trocas",
                column: "IDFuncionario2FuncionarioID",
                principalTable: "Funcionario",
                principalColumn: "FuncionarioID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trocas_Funcionario_IDFuncionario1FuncionarioID",
                table: "Trocas");

            migrationBuilder.DropForeignKey(
                name: "FK_Trocas_Funcionario_IDFuncionario2FuncionarioID",
                table: "Trocas");

            migrationBuilder.DropIndex(
                name: "IX_Trocas_IDFuncionario1FuncionarioID",
                table: "Trocas");

            migrationBuilder.DropIndex(
                name: "IX_Trocas_IDFuncionario2FuncionarioID",
                table: "Trocas");

            migrationBuilder.DropColumn(
                name: "IDFuncionario2FuncionarioID",
                table: "Trocas");

            migrationBuilder.RenameColumn(
                name: "IDFuncionario1FuncionarioID",
                table: "Trocas",
                newName: "IDFuncionario2");

            migrationBuilder.AddColumn<int>(
                name: "IDFuncionario1",
                table: "Trocas",
                nullable: false,
                defaultValue: 0);
        }
    }
}
