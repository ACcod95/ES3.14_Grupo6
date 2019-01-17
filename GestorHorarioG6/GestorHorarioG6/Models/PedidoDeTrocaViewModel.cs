using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class PedidoDeTrocaViewModel
    {
        public IEnumerable<Trocas> Trocas { get; set; }
        public PaginationViewModel Pagination { get; set; }

        public string CurrentNome { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Data inválida")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? DataInicio { get; set; }
    }
}
