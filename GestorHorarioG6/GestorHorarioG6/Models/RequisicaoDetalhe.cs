using System;
using System.ComponentModel.DataAnnotations;

namespace GestorHorarioG6.Models
{
    public class RequisicaoDetalhe
    {
        public int RequisicaoDetalheId { get; set; }

        [Required]
        public int RequisicaoId { get; set; }
        public Requisicao Requisicao { get; set; }

        [Required]
        public int ServicoId { get; set; }
        public Servico Servico { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime HoraCriticaDe { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime HoraCriticaAte { get; set; }

        [DataType(DataType.Time)]
        public DateTime DuraçãoEstimada { get; set; }

        public string Notas { get; set; }

        public Boolean Aprovado { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime HoraDeInicio { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime HoraDeFim { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime HoraConcluido { get; set; }
    }
}