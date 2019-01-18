using GestorHorarioG6.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Data
{
    public static class SeedData
    {
        public static void Populate(IServiceProvider applicationServices)
        {
            using
            (var serviceScope = applicationServices.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<GestorHorarioG6Context>();
                if (!db.Departamento.Any())
                {
                    db.Departamento.AddRange
                    (new Departamento { Nome = "Obstetrícia" },
                    new Departamento { Nome = "Ortopedia" },
                    new Departamento { Nome = "Pediatria" },
                    new Departamento { Nome = "Cardiologia" },
                    new Departamento { Nome = "Neurologia" }
                    );
                }
                db.SaveChanges();

                if (!db.Requisicao.Any())
                {
                    DateTime inicio = DateTime.Today.AddDays(5), fim = DateTime.Today.AddDays(5).AddHours(2);
                    db.Requisicao.AddRange
                    (new Requisicao { DepartamentoId = 1, Dia = inicio.AddDays(8) },
                    new Requisicao { DepartamentoId = 2, Dia = inicio.AddDays(7) },
                    new Requisicao { DepartamentoId = 3, Dia = inicio.AddDays(2) },
                    new Requisicao { DepartamentoId = 4, Dia = inicio },
                    new Requisicao { DepartamentoId = 5, Dia = inicio.AddDays(1) },
                    new Requisicao { DepartamentoId = 4, Dia = inicio.AddDays(5) },
                    new Requisicao { DepartamentoId = 3, Dia = inicio.AddDays(1) },
                    new Requisicao { DepartamentoId = 2, Dia = inicio.AddDays(10) },
                    new Requisicao { DepartamentoId = 1, Dia = inicio.AddDays(3) }
                    );
                }
                db.SaveChanges();

                if (!db.Cargo.Any())
                {
                    db.Cargo.AddRange
                    (new Cargo { Nome = "Engenheiro Informático" },
                     new Cargo { Nome = "Técnico de Equipamentos Eletrónicos" }
                    );
                }
                db.SaveChanges();

                if (!db.Funcionario.Any())
                {
                    db.Funcionario.AddRange
                    (new Funcionario { Nome = "João ", CargoId = 1, Nascimento = DateTime.Today, NascimentoFilho = DateTime.Today, NIF = "256248756", Telefone = "968745632", Email = "joao@gmail.com", Notas = "" },
                    new Funcionario { Nome = "Emanuel ", CargoId = 2, Nascimento = DateTime.Today, NascimentoFilho = DateTime.Today, NIF = "226789478", Telefone = "925874136", Email = "emanu@hotmail.com", Notas = "" },
                    new Funcionario { Nome = "Ana ", CargoId = 2, Nascimento = DateTime.Today, NascimentoFilho = DateTime.Today, NIF = "226897456", Telefone = "965871369", Email = "ana@hotmail.com", Notas = "" },
                    new Funcionario { Nome = "Maria ", CargoId = 1, Nascimento = DateTime.Today, NascimentoFilho = DateTime.Today, NIF = "224117819", Telefone = "912789658", Email = "mari4@sapo.pt", Notas = "" },
                    new Funcionario { Nome = "António ", CargoId = 2, Nascimento = DateTime.Today, NascimentoFilho = DateTime.Today, NIF = "235587975", Telefone = "918751032", Email = "toni@gmail.com", Notas = "" },
                    new Funcionario { Nome = "Bruna ", CargoId = 2, Nascimento = DateTime.Today, NascimentoFilho = DateTime.Today, NIF = "221362789", Telefone = "917854745", Email = "bruna@outlook.com", Notas = "" },
                    new Funcionario { Nome = "Celso ", CargoId = 1, Nascimento = DateTime.Today, NascimentoFilho = DateTime.Today, NIF = "221362800", Telefone = "917854777", Email = "celso@gmail.com", Notas = "" },
                    new Funcionario { Nome = "André ", CargoId = 2, Nascimento = DateTime.Today, NascimentoFilho = DateTime.Today, NIF = "221362801", Telefone = "917854778", Email = "andre@gmail.com", Notas = "" },
                    new Funcionario { Nome = "Tiago ", CargoId = 1, Nascimento = DateTime.Today, NascimentoFilho = DateTime.Today, NIF = "221362802", Telefone = "917854779", Email = "tiago@gmail.com", Notas = "" }
                    );
                }
                db.SaveChanges();
                
                if (!db.Turno.Any())
                {
                    db.Turno.AddRange
                    (new Turno { Nome = "Primeiro", HoraInicioManha = 8, HoraFimManha = 13, HoraInicioTarde = 14, HoraFimTarde = 16 },
                    new Turno { Nome = "Segundo", HoraInicioManha = 11, HoraFimManha = 14, HoraInicioTarde = 15, HoraFimTarde = 19 },
                    new Turno { Nome = "Terceiro", HoraInicioManha = 14, HoraFimManha = 19, HoraInicioTarde = 20, HoraFimTarde = 22 }
                    );
                }
                db.SaveChanges();
              
                if (!db.Servico.Any())
                {
                    db.Servico.AddRange
                    (new Servico { Nome = "Reparação", Descrição = "Reparação geral de um computador", DuracaoMedia = 2},
                    new Servico { Nome = "Substituição", DuracaoMedia = 5 },
                    new Servico { Nome = "Remoção", Descrição = "Remoção de um equipamento eletrónico", DuracaoMedia = 1},
                    new Servico { Nome = "Diagnóstico", Descrição = "Diagnóstico de um problema informático", DuracaoMedia = 3}
                    );
                }
                db.SaveChanges();

                if (!db.RequisicaoDetalhe.Any())
                {
                    var duracao1 = db.Servico.Where(s => s.ServicoId == 1).FirstOrDefault().DuracaoMedia;
                    var duracao2 = db.Servico.Where(s => s.ServicoId == 2).FirstOrDefault().DuracaoMedia;
                    var duracao3 = db.Servico.Where(s => s.ServicoId == 3).FirstOrDefault().DuracaoMedia;
                    var duracao4 = db.Servico.Where(s => s.ServicoId == 4).FirstOrDefault().DuracaoMedia;
                    db.RequisicaoDetalhe.AddRange
                    (new RequisicaoDetalhe { RequisicaoId = 1, ServicoId = 3, DuraçãoEstimada = duracao3, Aprovado = false, HoraCriticaDe = DateTime.Now.AddDays(2)},
                    new RequisicaoDetalhe { RequisicaoId = 5, ServicoId = 1, DuraçãoEstimada = duracao1, Aprovado = false, HoraCriticaDe = DateTime.Now.AddDays(1).AddHours(3)},
                    new RequisicaoDetalhe { RequisicaoId = 3, ServicoId = 2, DuraçãoEstimada = duracao2, Aprovado = false, HoraCriticaDe = DateTime.Now.AddDays(5) },
                    new RequisicaoDetalhe { RequisicaoId = 4, ServicoId = 4, DuraçãoEstimada = duracao4, Aprovado = true, HoraCriticaDe = DateTime.Now.AddDays(4) },
                    new RequisicaoDetalhe { RequisicaoId = 7, ServicoId = 1, DuraçãoEstimada = duracao1, Aprovado = false, HoraCriticaDe = DateTime.Now.AddDays(9) },
                    new RequisicaoDetalhe { RequisicaoId = 8, ServicoId = 3, DuraçãoEstimada = duracao3, Aprovado = true, HoraCriticaDe = DateTime.Now.AddDays(10) },
                    new RequisicaoDetalhe { RequisicaoId = 5, ServicoId = 2, DuraçãoEstimada = duracao2, Aprovado = false, HoraCriticaDe = DateTime.Now.AddDays(1) },
                    new RequisicaoDetalhe { RequisicaoId = 2, ServicoId = 1, DuraçãoEstimada = duracao1, Aprovado = false, HoraCriticaDe = DateTime.Now.AddDays(6) },
                    new RequisicaoDetalhe { RequisicaoId = 3, ServicoId = 4, DuraçãoEstimada = duracao4, Aprovado = true, HoraCriticaDe = DateTime.Now.AddDays(2) },
                    new RequisicaoDetalhe { RequisicaoId = 1, ServicoId = 3, DuraçãoEstimada = duracao3, Aprovado = false, HoraCriticaDe = DateTime.Now.AddDays(4) },
                    new RequisicaoDetalhe { RequisicaoId = 4, ServicoId = 2, DuraçãoEstimada = duracao2, Aprovado = false, HoraCriticaDe = DateTime.Now.AddDays(8) },
                    new RequisicaoDetalhe { RequisicaoId = 6, ServicoId = 4, DuraçãoEstimada = duracao4, Aprovado = false, HoraCriticaDe = DateTime.Now.AddDays(9) },
                    new RequisicaoDetalhe { RequisicaoId = 8, ServicoId = 1, DuraçãoEstimada = duracao1, Aprovado = false, HoraCriticaDe = DateTime.Now.AddDays(2) },
                    new RequisicaoDetalhe { RequisicaoId = 8, ServicoId = 1, DuraçãoEstimada = duracao1, Aprovado = false, HoraCriticaDe = DateTime.Now.AddHours(2) },
                    new RequisicaoDetalhe { RequisicaoId = 7, ServicoId = 3, DuraçãoEstimada = duracao3, Aprovado = true, HoraCriticaDe = DateTime.Now.AddHours(3) }
                    );
                }
                db.SaveChanges();
              
                if (!db.Estado.Any())
                {
                    db.Estado.AddRange
                    (new Estado { Nome = "Aprovado" },
                    new Estado { Nome = "Não Aprovado" },
                    new Estado { Nome = "Pendente" }
                    );
                }
                db.SaveChanges();
            }
        }
    }
}
