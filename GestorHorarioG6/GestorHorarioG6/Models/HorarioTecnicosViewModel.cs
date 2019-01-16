using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class HorarioTecnicosViewModel
    {
        public IEnumerable<HorarioTecnicos> HorarioTecnicos { get; set; }
        public PaginationViewModel PagingInfo { get; set; }

        public string CurrentNome { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Data inválida")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? DataInicio { get; set; }
    }
}
