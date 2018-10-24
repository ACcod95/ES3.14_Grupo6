using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class RequisicaoAprovada
    {
        public RequisicaoAprovada(Requisicao requisicao)
        {
            this.RequisicaoID = requisicao.RequisicaoID;
            this.ServicoID = requisicao.ServicoID;
            this.HoraDeInicio = requisicao.HoraDeInicio;
            this.HoraDeFim = requisicao.HoraDeFim;
            this.RequisicoesAdicionais = requisicao.RequisicoesAdicionais;
        }

        public int RequisicaoID { get; set; }

        [ForeignKey("Servicos")]
        public int ServicoID { get; set; }

        public Servicos Servico { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime HoraDeInicio { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime HoraDeFim { get; set; }

        public string RequisicoesAdicionais { get; set; }
    }
}
