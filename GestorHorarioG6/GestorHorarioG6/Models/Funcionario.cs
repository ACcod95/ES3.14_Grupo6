using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class Funcionario
    {
        [Key]
        public int FuncionarioId { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage = "Por favor insira o Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor selecione o Cargo")]
        public string Cargo { get; set; }// alterar para tabela 


        [Required(ErrorMessage = "Porfavor insira a Data de Nascimento ")]
        [DataType(DataType.Date)]
        public DateTime Nascimento { get; set; }

        [Required(ErrorMessage = "Por favor insira o NIF")]
        [RegularExpression(@"[0-9]{8}")]
        public string NIF { get; set; }

        [Required(ErrorMessage = "Por favor insira o Número de Telefone")]
        [RegularExpression(@"(\+[0-9]{3}\s)?[9][0-9]{8}([0-9]{2})?")]
        public string Telefone { get; set; }

        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(\w+(\.\w+)*@[a-zA-Z_]+?\.[a-zA-Z]{2,6})", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        public string Notas { get; set; }

        //[NotMapped] // reformular
       // public ICollection<Trocas> Trocas { get; set; }
    }
}
