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
                
                if (!db.Local.Any())
                {
                    db.Local.AddRange
                    (new Local { LocalId = 1, Nome = "Centro Obstrético" },
                    new Local { LocalId = 2, Nome = "Centro Cirúrgico" },
                    new Local { LocalId = 3, Nome = "UTI Pediátrica" },
                    new Local { LocalId = 4, Nome = "Gineco-Obstétrica" },
                    new Local { LocalId = 5, Nome = "Oncopediátrica" },
                    new Local { LocalId = 6, Nome = "Lactário" },
                    new Local { LocalId = 7, Nome = "Centro de Materias e Esterilização" },
                    new Local { LocalId = 8, Nome = "UTI Adulto" },
                    new Local { LocalId = 9, Nome = "UTI Neonatal" },
                    new Local { LocalId = 10, Nome = "Fonoaudiologia" },
                    new Local { LocalId = 11, Nome = "Sala de Equipamentos 1"}
                    );
                }

                if (!db.Equipamento.Any())
                {
                    db.Equipamento.AddRange
                    (new Equipamento { EquipamentoId = 1, Nome = "Ultrasom Portátil", Local = "Sala de Equipamentos 1" },
                    new Equipamento { EquipamentoId = 2, Nome = "Torre de vídeo endoscopia alta e baixa", Local = "Oncopediátrica" },
                    new Equipamento { EquipamentoId = 3, Nome = "Aparelho de anestesia com monitorização", Local = "Centro Cirúrgico" },
                    new Equipamento { EquipamentoId = 4, Nome = "Desfribilador", Local = "Sala de Equipamentos 1" },
                    new Equipamento { EquipamentoId = 5, Nome = "Aparelho de Ressonância Magnética", Local = "Centro Obstrético" },
                    new Equipamento { EquipamentoId = 6, Nome = "Aparelho de Raio X", Local = "UTI Adulto" },
                    new Equipamento { EquipamentoId = 7, Nome = "Hemodinâmica", Local = "Gineco-Obstétrica" },
                    new Equipamento { EquipamentoId = 8, Nome = "Aparelho de Hemodiálise", Local = "Sala de Equipamentos 1" }
                    );
                }
                
                db.SaveChanges();
            }
        }
    }
}
