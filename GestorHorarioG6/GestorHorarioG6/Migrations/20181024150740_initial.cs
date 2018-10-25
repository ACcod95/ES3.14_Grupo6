using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestorHorarioG6.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trocas",
                columns: table => new
                {
                    TrocasID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IDFuncionario1 = table.Column<int>(nullable: false),
                    IDFuncionario2 = table.Column<int>(nullable: false),
                    Turno1 = table.Column<int>(nullable: false),
                    Turno2 = table.Column<int>(nullable: false),
                    Conhecimento = table.Column<bool>(nullable: false),
                    Aprovado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trocas", x => x.TrocasID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trocas");
        }
    }
}
