using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class ServicosListViewModel
    {
        public IEnumerable<Servico> Servicos { get; set; }
        public PaginationViewModel PagingInfo { get; set; }
    }
}
