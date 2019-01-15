using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class RequisicaoDetalheListView
    {
        public int RequisicaoDetalheId { get; set; }
        public Requisicao RequisicaoDetalhe { get; set; }

        public PaginationViewModel PagingInfo { get; set; }
    }
}
