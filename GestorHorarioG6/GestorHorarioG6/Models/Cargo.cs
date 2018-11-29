using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class Cargo
    {
        [Key]
        public int CargoId { get; set; }

        [Required]
        public string Nome { get; set; }
    }
}
