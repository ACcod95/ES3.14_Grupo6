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
        [Required(ErrorMessage = "Este campo não pode estar por preencher")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Este campo não pode estar por preencher")]
        public int HoraInicio { get; set; }
        [Required(ErrorMessage = "Este campo não pode estar por preencher")]
        public int HoraFim { get; set; }
        [Required(ErrorMessage = "Este campo não pode estar por preencher")]
        public int Pausa { get; set; }
        [Required(ErrorMessage = "Este campo não pode estar por preencher")]
        public int IRefeicao { get; set; }
        [Required(ErrorMessage = "Este campo não pode estar por preencher")]
        public int FRefeicao { get; set; }


    }
}
