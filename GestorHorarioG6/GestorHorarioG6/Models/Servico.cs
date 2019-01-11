using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class Servico
    {
        [Key]
        public int ServicoId { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage = "Por favor insira um nome válido")]
        public string Nome { get; set; }

        [StringLength(200, MinimumLength = 5)]
        public string Descrição { get; set; }
    }
}
