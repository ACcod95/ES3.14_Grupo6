using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
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

                if (!db.Requisicao.Any())
                {
                    db.Requisicao.AddRange
                    (new Requisicao { DepartamentoId = 1, HoraDeInicio = DateTime.Today, HoraDeFim = DateTime.Today, Aprovado = false },
                    new Requisicao { DepartamentoId = 2, HoraDeInicio = DateTime.Today, HoraDeFim = DateTime.Today, Aprovado = false },
                    new Requisicao { DepartamentoId = 3, HoraDeInicio = DateTime.Today, HoraDeFim = DateTime.Today, Aprovado = false },
                    new Requisicao { DepartamentoId = 4, HoraDeInicio = DateTime.Today, HoraDeFim = DateTime.Today, Aprovado = false },
                    new Requisicao { DepartamentoId = 5, HoraDeInicio = DateTime.Today, HoraDeFim = DateTime.Today, Aprovado = false }
                    );
                }

                if (!db.Servico.Any())
                {
                    db.Servico.AddRange
                    (new Servico { Nome = "Reparação", Descrição = "Reparação geral de um computador" },
                    new Servico { Nome = "Substituição" }
                    );
                }
              
                if (!db.Cargo.Any())
                {
                    db.Cargo.AddRange
                    (new Cargo { Nome = "Engenheiro Informático" },
                     new Cargo { Nome = "Engenheiro Técnico Informático" }
                    
                    );
                }


                if (!db.Funcionario.Any())
                {
                    db.Funcionario.AddRange
                    (new Funcionario { Nome="João ",CargoId = 1, Nascimento = DateTime.Today, NascimentoFilho = DateTime.Today,NIF="256248756", Telefone="968745632", Email = "joao@gmail.com",Notas="" },
                    new Funcionario { Nome = "Emanuel ", CargoId = 2, Nascimento = DateTime.Today, NascimentoFilho = DateTime.Today, NIF = "226789478", Telefone = "925874136", Email = "emanu@hotmail.com", Notas = "" },
                    new Funcionario { Nome = "Ana ", CargoId = 2, Nascimento = DateTime.Today, NascimentoFilho = DateTime.Today, NIF = "226897456", Telefone = "965871369", Email = "ana@hotmail.com", Notas = "" },
                    new Funcionario { Nome = "Maria ", CargoId = 1, Nascimento = DateTime.Today, NascimentoFilho = DateTime.Today, NIF = "224117819", Telefone = "912789658", Email = "mari4@sapo.pt", Notas = "" },
                    new Funcionario { Nome = "António ", CargoId = 2, Nascimento = DateTime.Today, NascimentoFilho = DateTime.Today, NIF = "235587975", Telefone = "918751032", Email = "toni@gmail.com", Notas = "" },
                    new Funcionario { Nome = "Bruna ", CargoId = 2, Nascimento = DateTime.Today, NascimentoFilho = DateTime.Today, NIF = "221362789", Telefone = "917854745", Email = "bruna@outlook.com", Notas = "" }
                    );
                }
                db.SaveChanges();
            }
        }
    }
}
