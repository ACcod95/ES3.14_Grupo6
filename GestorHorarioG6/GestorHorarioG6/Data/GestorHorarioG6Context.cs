using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GestorHorarioG6.Models
{
    public class GestorHorarioG6Context : DbContext
    {
        public GestorHorarioG6Context (DbContextOptions<GestorHorarioG6Context> options)
            : base(options)
        {
        }

        public DbSet<GestorHorarioG6.Models.Funcionario> Funcionario { get; set; }
    }
}
