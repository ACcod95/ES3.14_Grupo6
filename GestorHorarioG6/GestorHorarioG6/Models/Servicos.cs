using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class Servicos
    {
        public int ServicoID { get; set; }

        [Required]
        public string Nome { get; set; }
    }
}
