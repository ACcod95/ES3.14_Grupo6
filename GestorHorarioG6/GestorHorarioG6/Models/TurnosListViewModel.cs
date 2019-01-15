using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class TurnosListViewModel
    {
        public IEnumerable<Turno> Turno { get; set; }
        public PaginationViewModel PagingInfo { get; set; }

        [DisplayName("Nome")]
        public string CurrentNome { get; set; }
    }
}
