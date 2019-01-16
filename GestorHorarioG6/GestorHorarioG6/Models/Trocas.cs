using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class Trocas
    {
        [Key]
        public int TrocasID { set; get; }

        [Required]
        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }

       // public int IdFuncionarioParaTroca { get; set; }

        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

                                           // public DateTime DiaDeTroca { get; set; }
                                           // [DataType(DataType.Date)]
                                           //  public DateTime DiaParaTroca { get; set; }

        public HorarioATrocar HorarioATrocar { get; set; }
        public int HorarioATrocarId { get; set; }

        public HorarioParaTroca HorarioParaTroca { get; set; }
        public int HorarioParaTrocaId { get; set; }

        public Estado Estado { get; set; }
        public int EstadoTrocaId { get; set; }
    }
}
