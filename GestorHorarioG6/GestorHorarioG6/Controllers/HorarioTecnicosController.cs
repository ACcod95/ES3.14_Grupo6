using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestorHorarioG6.Data;
using GestorHorarioG6.Models;

namespace GestorHorarioG6.Controllers
{
    public class HorarioTecnicosController : Controller
    {
        private readonly GestorHorarioG6Context _context;
        private const int PAGE_SIZE = 12;

        public HorarioTecnicosController(GestorHorarioG6Context context)
        {
            _context = context;
        }

        // GET: HorarioTecnicos
        public async Task<IActionResult> Index(HorarioTecnicosViewModel model = null, int page = 1)
        {
            string nome = null;
            DateTime? data = null;

            if (model != null && model.DataInicio != null || model.CurrentNome != null)
            {
                nome = model.CurrentNome;
                data = model.DataInicio;
                page = 1;
            }

            IQueryable<HorarioTecnicos> horario;
            int numHorario;
            IEnumerable<HorarioTecnicos> listaHorario;

            if (data.HasValue && string.IsNullOrEmpty(nome)) 
            {
                int ano = data.Value.Year;
                int mes = data.Value.Month;
                int dia = data.Value.Day;

                horario = _context.HorarioTecnicos
                   .Where(h => h.DataInicioManha.Year.Equals(ano) && h.DataInicioManha.Month.Equals(mes) && h.DataInicioManha.Day.Equals(dia));

                numHorario = await horario.CountAsync();

                listaHorario = await horario
                    .Include(h => h.Funcionario)
                    .Include(h => h.Turno)
                    .OrderBy(h => h.DataInicioManha)
                    .Skip(PAGE_SIZE * (page - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();
            }
            else if (!string.IsNullOrEmpty(nome) && !data.HasValue) 
            {
                horario = _context.HorarioTecnicos
                    .Where(h => h.Funcionario.Nome.Contains(nome.Trim()));

                numHorario = await horario.CountAsync();

                listaHorario = await horario
                    .Include(h => h.Funcionario)
                    .Include(h => h.Turno)
                    .OrderBy(h => h.DataInicioManha)
                    .Skip(PAGE_SIZE * (page - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();
            }         
            else
            {
                horario = _context.HorarioTecnicos;

                numHorario = await horario.CountAsync();

                listaHorario = await horario
                  .Include(h => h.Funcionario)
                  .Include(h => h.Turno)
                  .OrderBy(h => h.DataInicioManha)
                  .Skip(PAGE_SIZE * (page - 1))
                  .Take(PAGE_SIZE)
                  .ToListAsync();
            }

            if (page > (numHorario / PAGE_SIZE) + 1)
            {
                page = 1;
            }

            if (listaHorario.Count() == 0)
            {
                TempData["Insuccess"] = "Não existe dados";
            }


            return View(
                new HorarioTecnicosViewModel
                {
                    HorarioTecnicos = listaHorario,
                    PagingInfo = new PaginationViewModel
                    {
                        CurrentPage = page,
                        ItensPerPage = PAGE_SIZE,
                        TotalItems = numHorario
                    },
                    CurrentNome = nome,
                    DataInicio = data
                }
            );
        }

        // GET: HorarioTecnicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioTecnicos = await _context.HorarioTecnicos
                .Include(h => h.Funcionario)
                .Include(h => h.Turno)
                .FirstOrDefaultAsync(m => m.HorarioTecnicoId == id);
            if (horarioTecnicos == null)
            {
                return NotFound();
            }

            return View(horarioTecnicos);
        }

        // GET: HorarioTecnicos/Create
        public IActionResult Create()
        {
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "FuncionarioId", "NIF");
            ViewData["TurnoId"] = new SelectList(_context.Turno, "TurnoId", "TurnoId");
            return View();
        }

        // POST: HorarioTecnicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HorarioTecnicoId,DataInicioManha,DataFimManha,DataInicioTarde,DataFimTarde,TurnoId,FuncionarioId")] HorarioTecnicos horarioTecnicos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(horarioTecnicos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "FuncionarioId", "NIF", horarioTecnicos.FuncionarioId);
            ViewData["TurnoId"] = new SelectList(_context.Turno, "TurnoId", "TurnoId", horarioTecnicos.TurnoId);
            return View(horarioTecnicos);
        }

        // GET: HorarioTecnicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioTecnicos = await _context.HorarioTecnicos.FindAsync(id);
            if (horarioTecnicos == null)
            {
                return NotFound();
            }
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "FuncionarioId", "NIF", horarioTecnicos.FuncionarioId);
            ViewData["TurnoId"] = new SelectList(_context.Turno, "TurnoId", "TurnoId", horarioTecnicos.TurnoId);
            return View(horarioTecnicos);
        }

        // POST: HorarioTecnicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HorarioTecnicoId,DataInicioManha,DataFimManha,DataInicioTarde,DataFimTarde,TurnoId,FuncionarioId")] HorarioTecnicos horarioTecnicos)
        {
            if (id != horarioTecnicos.HorarioTecnicoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horarioTecnicos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorarioTecnicosExists(horarioTecnicos.HorarioTecnicoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "FuncionarioId", "NIF", horarioTecnicos.FuncionarioId);
            ViewData["TurnoId"] = new SelectList(_context.Turno, "TurnoId", "TurnoId", horarioTecnicos.TurnoId);
            return View(horarioTecnicos);
        }

        // GET: HorarioTecnicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioTecnicos = await _context.HorarioTecnicos
                .Include(h => h.Funcionario)
                .Include(h => h.Turno)
                .FirstOrDefaultAsync(m => m.HorarioTecnicoId == id);
            if (horarioTecnicos == null)
            {
                return NotFound();
            }

            return View(horarioTecnicos);
        }

        // POST: HorarioTecnicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horarioTecnicos = await _context.HorarioTecnicos.FindAsync(id);
            _context.HorarioTecnicos.Remove(horarioTecnicos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorarioTecnicosExists(int id)
        {
            return _context.HorarioTecnicos.Any(e => e.HorarioTecnicoId == id);
        }

        // GET: HorarioTecnicos/GerarHorarioTecnicos
        public IActionResult GerarHorarioTecnicos()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GerarHorarioTecnicos(GerarHorarioTecnicos gerarHorarioTecnicos)
        {
            if (ModelState.IsValid)
            {
                DateTime dataIn = gerarHorarioTecnicos.DataInicioSemana;
                GerarHorarioTecnico(_context, dataIn);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        /**Funções**/
        private void GerarHorarioTecnico(GestorHorarioG6Context db, DateTime dia)
        {
            DateTime segunda;
            DateTime sexta;
            string turno;

            if (dia.DayOfWeek == DayOfWeek.Monday && dia.CompareTo(DateTime.Now) > 0)
            {
                segunda = dia.Date;
                sexta = dia.Date.AddDays(5);
            }
            else {
                TempData["Insuccess2"] = "Não pode gerar nesse dia (Têm de ser segunda e numa data superior)";
                return;
            }
                

            if (db.HorarioTecnicos.Where(d => d.DataFimManha.Date == dia).Any())
            {
                TempData["Insuccess2"] = "Não pode gerar nesse dia (Têm de ser segunda e numa data superior)";
                return;
            }

            TempData["Success"] = "Horário Gerado";

            int[] tecnicos = IdTecnicos();
            int controlo = 1;
            int tec = 0;

            //Lista de Tecnicos
            List<int> listaTecnicos = new List<int>(tecnicos);

            int numeroTecnicos = listaTecnicos.Count();

            for (DateTime i = segunda; i < sexta; i = i.AddDays(1))
            {
                for(int j = 0; j <= numeroTecnicos - 1; j++)
                {
                    if (controlo == 1)
                    {
                        turno = "Primeiro";
                        tec = listaTecnicos[j];
                        Turno IdTurno = _context.Turno.SingleOrDefault(t => t.Nome.Equals(turno));
                        Funcionario IdTecnico = _context.Funcionario.SingleOrDefault(f => f.FuncionarioId == tec);

                        InserirDadosNoHorarioTecnico(db, i.AddHours(8), i.AddHours(12), i.AddHours(13), i.AddHours(15), IdTurno, IdTecnico);
                    }
                    else if (controlo == 2)
                    {
                        turno = "Segundo";
                        tec = listaTecnicos[j];
                        Turno IdTurno = _context.Turno.SingleOrDefault(t => t.Nome.Equals(turno));
                        Funcionario IdTecnico = _context.Funcionario.SingleOrDefault(f => f.FuncionarioId == tec);

                        InserirDadosNoHorarioTecnico(db, i.AddHours(11), i.AddHours(14), i.AddHours(15), i.AddHours(19), IdTurno, IdTecnico);
                    }
                    else if (controlo == 3)
                    {
                        turno = "Terceiro";
                        tec = listaTecnicos[j];
                        Turno IdTurno = _context.Turno.SingleOrDefault(t => t.Nome.Equals(turno));
                        Funcionario IdTecnico = _context.Funcionario.SingleOrDefault(f => f.FuncionarioId == tec);

                        InserirDadosNoHorarioTecnico(db, i.AddHours(14), i.AddHours(19), i.AddHours(20), i.AddHours(22), IdTurno, IdTecnico);
                    }
                    controlo++;
                    if (controlo > 3)
                    {
                        controlo = 1;
                    }
                }
            }
        }


        private int[] IdTecnicos()
        {
            var tecnicos = from t in _context.Funcionario
                           select t.FuncionarioId;

            int[] arrayIdTecnicos = tecnicos.ToArray();

            return arrayIdTecnicos;
        }

        private void InserirDadosNoHorarioTecnico(GestorHorarioG6Context db, DateTime datainiciomanha, DateTime datafimmanha, DateTime datainiciotarde, DateTime datafimtarde, Turno turnoId, Funcionario funcionarioId)
        {
            db.HorarioTecnicos.Add(
                new HorarioTecnicos { DataInicioManha = datainiciomanha, DataFimManha = datafimmanha, DataInicioTarde = datainiciotarde, DataFimTarde = datafimtarde, TurnoId = turnoId.TurnoId, FuncionarioId = funcionarioId.FuncionarioId}
            );

            db.SaveChanges();
        }
    }
}
