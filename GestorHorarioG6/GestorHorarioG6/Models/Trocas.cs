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
        [ForeignKey("Funcionario")]
        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }

        public int FuncionarioId2 { get; set; }
        public Funcionario Funcionario2 { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public DateTime DiaF1{ get; set; }

        [DataType(DataType.Date)]
        public DateTime DiaF2 { get; set; }

        public Boolean Conhecimento { get; set; }

        public Boolean Aprovado { get; set; }

    }
}
