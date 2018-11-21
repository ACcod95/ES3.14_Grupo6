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
                    (new Local { Nome = "Centro Obstrético" },
                    new Local { Nome = "Centro Cirúrgico" },
                    new Local { Nome = "UTI Pediátrica" },
                    new Local { Nome = "Gineco-Obstétrica" },
                    new Local { Nome = "Oncopediátrica" },
                    new Local { Nome = "Lactário" },
                    new Local { Nome = "Centro de Materias e Esterilização" },
                    new Local { Nome = "UTI Adulto" },
                    new Local { Nome = "UTI Neonatal" },
                    new Local {  Nome = "Fonoaudiologia" },
                    new Local {  Nome = "Sala de Equipamentos 1"}
                    );
                }

                if (!db.Equipamento.Any())
                {
                    db.Equipamento.AddRange
                    (new Equipamento {  Nome = "Ultrasom Portátil", Local = "Sala de Equipamentos 1" },
                    new Equipamento {  Nome = "Torre de vídeo endoscopia alta e baixa", Local = "Oncopediátrica" },
                    new Equipamento {  Nome = "Aparelho de anestesia com monitorização", Local = "Centro Cirúrgico" },
                    new Equipamento {  Nome = "Desfribilador", Local = "Sala de Equipamentos 1" },
                    new Equipamento {  Nome = "Aparelho de Ressonância Magnética", Local = "Centro Obstrético" },
                    new Equipamento {  Nome = "Aparelho de Raio X", Local = "UTI Adulto" },
                    new Equipamento {  Nome = "Hemodinâmica", Local = "Gineco-Obstétrica" },
                    new Equipamento {  Nome = "Aparelho de Hemodiálise", Local = "Sala de Equipamentos 1" }
                    );
                }
                
                db.SaveChanges();
            }
        }
    }
}
