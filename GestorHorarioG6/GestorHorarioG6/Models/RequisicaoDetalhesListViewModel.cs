using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class RequisicaoDetalhesListViewModel
    {
        public IEnumerable<RequisicaoDetalhe> RequisicaoDetalhes { get; set; }
        public PaginationViewModel PagingInfo { get; set; }
    }
}
