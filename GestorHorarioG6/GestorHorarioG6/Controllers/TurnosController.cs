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
    public class TurnosController : Controller
    {
        private readonly GestorHorarioG6Context _context;
        private const int PageSize = 5;

        public TurnosController(GestorHorarioG6Context context)
        {
            _context = context;
        }

        // GET: Turnos
        public async Task<IActionResult> Index(TurnoListViewModel model = null, int page = 1)
        {
            string nome = null;

            if (model != null && model.CurrentNome != null)
            {
                nome = model.CurrentNome;
                page = 1;
            }

            var turnos = _context.Turno.Where(t => nome == null || t.Nome.Contains(nome));
            var total = await turnos.CountAsync();

            if (page > (total / PageSize) + 1)
            {
                page = 1;
            }

            var listTurno = await turnos
                .OrderBy(t => t.TurnoId)
                .Skip(PageSize * (page - 1))
                .Take(PageSize)
                .ToListAsync();

            return View(new TurnoListViewModel
            {
                Turno = listTurno,
                PagingInfo = new PaginationViewModel
                {
                    CurrentPage = page,
                    ItensPerPage = PageSize,
                    TotalItems = total
                },
                CurrentNome = nome
            });

        }

        // GET: Turnos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turno
                .FirstOrDefaultAsync(m => m.TurnoId == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // GET: Turnos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Turnos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TurnoId,Nome,HoraInicioManha,HoraFimManha,HoraInicioTarde,HoraFimTarde")] Turno turno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(turno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(turno);
        }

        // GET: Turnos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turno.FindAsync(id);
            if (turno == null)
            {
                return NotFound();
            }
            return View(turno);
        }

        // POST: Turnos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TurnoId,Nome,HoraInicioManha,HoraFimManha,HoraInicioTarde,HoraFimTarde")] Turno turno)
        {
            if (id != turno.TurnoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurnoExists(turno.TurnoId))
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
            return View(turno);
        }

        // GET: Turnos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turno
                .FirstOrDefaultAsync(m => m.TurnoId == id);
            if (turno == null)
            {
                return NotFound();
            }

            return View(turno);
        }

        // POST: Turnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turno = await _context.Turno.FindAsync(id);
            _context.Turno.Remove(turno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TurnoExists(int id)
        {
            return _context.Turno.Any(e => e.TurnoId == id);
        }
    }
}
