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
        [Required(ErrorMessage = "Por favor insira um nome válido")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor selecione um cargo válido")]
        public Cargo Cargo { get; set; }
        public int CargoId { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Por favor insira uma data de nascimento válida")]
        public DateTime Nascimento { get; set; }


        [DataType(DataType.Date)]
        public DateTime NascimentoFilho { get; set; }

        [Required(ErrorMessage = "Por favor insira um NIF válido")]
        [RegularExpression(@"[0-9]{8}")]
        public string NIF { get; set; }

        [Required(ErrorMessage = "Por favor insira um número de telefone válido")]
        [RegularExpression(@"(\+[0-9]{3}\s)?[9][0-9]{8}([0-9]{2})?")]
        public string Telefone { get; set; }

        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(\w+(\.\w+)*@[a-zA-Z_]+?\.[a-zA-Z]{2,6})", ErrorMessage = "Por favor insira um email válido")]
        public string Email { get; set; }

        public string Notas { get; set; }

        //[NotMapped] // reformular
       // public ICollection<Trocas> Trocas { get; set; }
    }
}
