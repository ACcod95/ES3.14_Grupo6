using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class Turno
    {
        [Key]
        public int TurnoId { get; set; }
        public string Nome { get; set; }
        [Required(ErrorMessage = "Este campo não pode estar por preencher")]
        public int HoraInicioManha { get; set; }
        [Required(ErrorMessage = "Este campo não pode estar por preencher")]
        public int HoraFimManha { get; set; }
        [Required(ErrorMessage = "Este campo não pode estar por preencher")]
        public int HoraInicioTarde { get; set; }
        [Required(ErrorMessage = "Este campo não pode estar por preencher")]
        public int HoraFimTarde { get; set; }

        public ICollection<HorarioTecnicos> HorarioTecnicos { get; set; }
    }
}
