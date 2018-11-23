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
                
                if (!db.Bloco.Any())
                {
                    db.Bloco.AddRange
                    (new Bloco { Nome = "Centro Obstrético" },
                    new Bloco { Nome = "Centro Cirúrgico" },
                    new Bloco { Nome = "UTI Pediátrica" },
                    new Bloco { Nome = "Gineco-Obstétrica" },
                    new Bloco { Nome = "Oncopediátrica" },
                    new Bloco { Nome = "Lactário" },
                    new Bloco { Nome = "Centro de Materias e Esterilização" },
                    new Bloco { Nome = "UTI Adulto" },
                    new Bloco { Nome = "UTI Neonatal" },
                    new Bloco { Nome = "Fonoaudiologia" },
                    new Bloco { Nome = "Sala de Equipamentos 1"}
                    );
                }

                if (!db.Equipamento.Any())
                {
                    db.Equipamento.AddRange
                    (new Equipamento { Nome = "Ultrasom Portátil", BlocoId = 11 },
                    new Equipamento { Nome = "Torre de vídeo endoscopia alta e baixa", BlocoId = 5 },
                    new Equipamento { Nome = "Aparelho de anestesia com monitorização", BlocoId = 2 },
                    new Equipamento { Nome = "Desfribilador", BlocoId = 11 },
                    new Equipamento { Nome = "Aparelho de Ressonância Magnética", BlocoId = 1 },
                    new Equipamento { Nome = "Aparelho de Raio X", BlocoId = 8 },
                    new Equipamento { Nome = "Hemodinâmica", BlocoId = 4 },
                    new Equipamento { Nome = "Aparelho de Hemodiálise", BlocoId = 11 }
                    );
                }
                db.SaveChanges();
            }
        }
    }
}
