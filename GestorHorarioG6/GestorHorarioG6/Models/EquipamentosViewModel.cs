using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class EquipamentosViewModel
    {
        public IEnumerable<Equipamento> Equipamento { get; set; }
        public PaginationViewModel PageInfo { get; set; }

        [DisplayName("Nome")]
        public string CurrentNome { get; set; }
    }
}
