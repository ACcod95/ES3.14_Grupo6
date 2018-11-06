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
        public int FuncionarioID { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage = "Please enter Name")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Please enter the job")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string Cargo { get; set; }

        [Required(ErrorMessage = "Please enter BirthDate")]
        [DataType(DataType.Date)]
        public DateTime Nascimento { get; set; }

        [Required(ErrorMessage = "Please enter NIF")]
        [RegularExpression(@"[0-9]{8}")]
        public int NIF { get; set; }

        [Required(ErrorMessage = "Please enter Number")]
        [RegularExpression(@"(\+[0-9]{3}\s)?[9][0-9]{8}([0-9]{2})?")]
        public int Telefone { get; set; }

        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(\w+(\.\w+)*@[a-zA-Z_]+?\.[a-zA-Z]{2,6})", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        
        public string Notas { get; set; }

        [NotMapped]
        public ICollection<Trocas> Trocas { get; set; }
    }
}
