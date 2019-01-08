using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestorHorarioG6.Models;

namespace GestorHorarioG6.Controllers
{
    public class HorarioTecnicosController : Controller
    {
        private readonly GestorHorarioG6Context _context;

        public HorarioTecnicosController(GestorHorarioG6Context context)
        {
            _context = context;
        }

        // GET: HorarioTecnicos
        public async Task<IActionResult> Index()
        {
            var gestorHorarioG6Context = _context.HorarioTecnicos.Include(h => h.Funcionario).Include(h => h.Turno);
            return View(await gestorHorarioG6Context.ToListAsync());
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

                int ano = dataIn.Year;
                int mes = dataIn.Month;
                int dia = dataIn.Day;

                GerarHorarioTecnico(_context, ano, mes, dia);
                return RedirectToAction(nameof(Index));
            }

            return View(gerarHorarioTecnicos);
        }

       
        /**Funções**/
        private void GerarHorarioTecnico(GestorHorarioG6Context db, int ano, int mes, int dia)
        {
            int segunda = 2;
            int sexta = 6;

            int[] tecnicos = IdTecnicos();

            int tec = 0;

            //Lista de Tecnicos
            List<int> listaTecnicos = new List<int>(tecnicos);

            DateTime data;

            int numeroTecnicos = listaTecnicos.Count();
            int controlo = 1;
            string turno;

            for (int i = 0; i <= numeroTecnicos-1; i++)
            {
               for(int j = segunda; j <= sexta; j++)
                {
                    if (controlo == 1)
                    {
                        turno = "Primeiro";

                        tec = listaTecnicos[i];
                        data = new DateTime(ano, mes, dia-2+j, 0, 0, 0);

                        Turno IdTurno = _context.Turno.SingleOrDefault(t => t.Nome.Equals(turno));
                        Funcionario IdTecnico = _context.Funcionario.SingleOrDefault(f => f.FuncionarioId == tec);

                        InserirDadosNoHorarioTecnico(db, data.AddHours(8), data.AddHours(12), data.AddHours(13), data.AddHours(15), IdTurno, IdTecnico);

                    } else if (controlo == 2)
                    {
                        turno = "Segundo";

                        tec = listaTecnicos[i];
                        data = new DateTime(ano, mes, dia-2+j, 0, 0, 0);

                        Turno IdTurno = _context.Turno.SingleOrDefault(t => t.Nome.Equals(turno));
                        Funcionario IdTecnico = _context.Funcionario.SingleOrDefault(f => f.FuncionarioId == tec);

                        InserirDadosNoHorarioTecnico(db, data.AddHours(11), data.AddHours(14), data.AddHours(15), data.AddHours(19), IdTurno, IdTecnico);
                    } else if (controlo == 3)
                    {
                        turno = "Terceiro";

                        tec = listaTecnicos[i];
                        data = new DateTime(ano, mes, dia-2+j, 0, 0, 0);

                        Turno IdTurno = _context.Turno.SingleOrDefault(t => t.Nome.Equals(turno));
                        Funcionario IdTecnico = _context.Funcionario.SingleOrDefault(f => f.FuncionarioId == tec);

                        InserirDadosNoHorarioTecnico(db, data.AddHours(14), data.AddHours(19), data.AddHours(20), data.AddHours(22), IdTurno, IdTecnico);
                    }

                }

               if(controlo < 3)
                {
                    controlo++;
                }
                else
                {
                    controlo = 1;
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
