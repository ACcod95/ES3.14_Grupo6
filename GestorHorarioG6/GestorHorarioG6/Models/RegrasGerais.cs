using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class RegrasGerais
    {
        [Key]
        public int RegraId { get; set; }

        [Required(ErrorMessage = "Por favor insira um nome válido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome tem de ter entre 3 a 50 caracteres")]
        public string Nome { get; set; }

        [StringLength(500, ErrorMessage = "O nome tem de ter menos de 500 caracteres")]
        public string Descricao { get; set; }

        public int Horas { get; set; }
    }
}
