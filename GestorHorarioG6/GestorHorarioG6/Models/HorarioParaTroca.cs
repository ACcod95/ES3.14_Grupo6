using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class HorarioParaTroca
    {
        public int HorarioParaTrocaId { get; set; }

        public HorarioTecnicos HorarioFucnionario { get; set; }
        public int HorarioFucnionarioId { get; set; }

        public ICollection<Trocas> Trocas { get; set; }
    }
}
