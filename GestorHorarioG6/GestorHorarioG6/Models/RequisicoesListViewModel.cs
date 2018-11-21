using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class RequisicoesListViewModel
    {
        public IEnumerable<Requisicao> Requisicoes { get; set; }
        public PaginationViewModel PagingInfo { get; set; }
    }
}
