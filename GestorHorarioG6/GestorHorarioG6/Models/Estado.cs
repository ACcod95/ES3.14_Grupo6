using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class Estado
    {
        [Key]
        public int EstadoTrocaId { get; set; }

        public string Nome { get; set; }

        public ICollection<Trocas> Trocas { get; set; }
    }
}