using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class RequisicoesEquipamentosListViewModel
    {
        public IEnumerable<RequisicaoEquipamento> RequisicaoEquipamento { get; set; }
        public PaginationViewModel PagingInfo { get; set; }

        [DisplayName("HoraDeInicio")]
        [DataType(DataType.Date)]
        public DateTime CurrentDay { get; set; }
    }
}
