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
        public int TrocasID { set; get; }

        [Required]
        [ForeignKey("Funcionario")]
        public int IDFuncionario1 { get; set; }

        [ForeignKey("Funcionario")]
        public int IDFuncionario2 { get; set; }

        [Required]
        [ForeignKey("Turno")]
        public int Turno1 { get; set; }

        [ForeignKey("Turno")]
        public int Turno2 { get; set; }

        public Boolean  Conhecimento{get;set;}

        public Boolean Aprovado { get; set; }

    }
}
