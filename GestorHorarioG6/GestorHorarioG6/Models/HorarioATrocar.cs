using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class HorarioATrocar
    {
        public int HorarioATrocarId { get; set; }

        public HorarioTecnicos HorarioTecnicos { get; set; }
        public int HorarioTecnicoId { get; set; }

        public ICollection<Trocas> Trocas { get; set; }
    }
}