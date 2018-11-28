using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class RequisicaoEquipamento
    {
        [Key]
        public int RequisicaoEquipamentoId { get; set; }

        public Equipamento Equipamento { get; set; }
        public int EquipamentoId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime HoraDeInicio { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime HoraDeFim { get; set; }

        [Required]
        public Bloco Bloco { get; set; }
        public int BlocoId { get; set; }
    }
}
