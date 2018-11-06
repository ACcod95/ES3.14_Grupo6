using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class Requisicao
    {
        [Key]
        public int RequisicaoId { get; set; }

        public Servico Servico { get; set; }
        public int ServicoId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime HoraDeInicio { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime HoraDeFim { get; set; }

        public string RequisicoesAdicionais { get; set; }

        public Boolean Aprovado { get; set; }
    }
}
