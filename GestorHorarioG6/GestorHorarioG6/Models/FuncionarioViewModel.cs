using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class FuncionarioViewModel
    {
        public IEnumerable<Funcionario> Funcionario { get; set; }
        public PaginationViewModel PageInfo { get; set; }

        [DisplayName("Nome")]
        public string CurrentNome { get; set; }
    }
}
