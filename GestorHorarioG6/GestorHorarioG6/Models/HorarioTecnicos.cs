using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class HorarioTecnicos
    {
        [Key]
        public int HorarioTecnicoId { get; set; }

        public DateTime DataInicioManha { get; set; }
        public DateTime DataFimManha { get; set; }
        public DateTime DataInicioTarde { get; set; }
        public DateTime DataFimTarde { get; set; }

        public int TurnoId { get; set; }
        public Turno Turno { get; set; }

        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }
    }
}
