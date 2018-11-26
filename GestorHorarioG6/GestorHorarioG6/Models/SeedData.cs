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
                    (new Servico { Nome = "Reparação", Descrição = "Reparação geral de um computador"},
                    new Servico { Nome = "Substituição" }
                    );
                }
                db.SaveChanges();
            }
        }
    }
}
