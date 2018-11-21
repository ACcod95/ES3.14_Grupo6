using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class PaginationViewModel
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / ItemsPerPage);
    }
}
