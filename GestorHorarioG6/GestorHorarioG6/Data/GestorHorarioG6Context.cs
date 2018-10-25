using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GestorHorarioG6.Models;

namespace GestorHorarioG6.Models
{
    public class GestorHorarioG6Context : DbContext
    {
        public GestorHorarioG6Context (DbContextOptions<GestorHorarioG6Context> options)
            : base(options)
        {
        }

        public DbSet<GestorHorarioG6.Models.Funcionario> Funcionario { get; set; }

        public DbSet<GestorHorarioG6.Models.Trocas> Trocas { get; set; }
    }
}
