using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class Equipamento
    {
        [Key]
        public int EquipamentoId{ get; set; }

        [Required]
        public string Nome { get; set; }
        
        public Bloco Bloco { get; set; }
        public int BlocoId { get; set; }
    }
}
