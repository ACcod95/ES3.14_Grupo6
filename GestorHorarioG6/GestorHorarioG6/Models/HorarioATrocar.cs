using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class HorarioATrocar
    {
        public int HorarioATrocarId { get; set; }

        public HorarioTecnicos HorarioFuncionario { get; set; }
        public int HorarioFuncionarioId { get; set; }

        public ICollection<Trocas> Trocas { get; set; }
    }
}