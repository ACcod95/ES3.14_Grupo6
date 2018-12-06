using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class Requisicao
    {
        [Key]
        public int RequisicaoId { get; set; }

        public Departamento Departamento { get; set; }
        public int DepartamentoId { get; set; }

        public IEnumerable<RequisicaoDetalhe> Detalhes { get; set; }
    }
}
