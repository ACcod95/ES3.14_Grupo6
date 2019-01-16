using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestorHorarioG6.Models
{
    public class GerarHorarioTecnicos
    {
        [Required(ErrorMessage = "Por favor, introduza a data de ínicio da semana")]
        [DataType(DataType.Date, ErrorMessage = "Data de início de semana inválida")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime DataInicioSemana { get; set; }
    }
}
