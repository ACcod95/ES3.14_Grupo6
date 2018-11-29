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
        private int _funcionarioId;

        [Key]
        public int TrocasID { set; get; }

        [Required]
        public Funcionario IDFuncionario1 { get; set; }
//public int FuncionarioId { get => _funcionarioId; set => _funcionarioId = value; }
        public Funcionario IDFuncionario2 { get; set; }
        
        [Required]
        public int Turno1 { get; set; }

        public int Turno2 { get; set; }

        public Boolean Conhecimento { get; set; }



    }
}
