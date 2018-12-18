using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class Turno
    {
        public int TurnoId { get; set; }

        public string Numero { get; set; }

        public int HoraInicio { get; set; }

        public int HoraFim { get; set; }
        
        public int IRefeicao { get; set; }

        public int FRefeicao { get; set; }


    }
}
