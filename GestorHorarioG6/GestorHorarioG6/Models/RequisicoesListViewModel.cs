using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class RequisicoesListViewModel
    {
        public IEnumerable<Requisicao> Requisicoes { get; set; }
        public PaginationViewModel PagingInfo { get; set; }

        [DisplayName("Dia")]
        [DataType(DataType.Date)]
        public DateTime CurrentDay { get; set; }
    }
}
