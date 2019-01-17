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
    public class HorarioParaTrocasController : Controller
    {
        private readonly GestorHorarioG6Context _context;

        public HorarioParaTrocasController(GestorHorarioG6Context context)
        {
            _context = context;
        }

        // GET: HorarioParaTrocas
        public async Task<IActionResult> Index()
        { 
            return View(await _context.HorarioATrocar.Include(h => h.HorarioTecnicos).ToListAsync());
        }

        // GET: HorarioParaTrocas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioParaTroca = await _context.HorarioParaTroca
                .Include(h => h.HorarioTecnicos)
                .FirstOrDefaultAsync(m => m.HorarioParaTrocaId == id);
            if (horarioParaTroca == null)
            {
                return NotFound();
            }

            return View(horarioParaTroca);
        }

        // GET: HorarioParaTrocas/Create
        public IActionResult Create()
        {
            ViewData["HorarioTecnicoId"] = new SelectList(_context.HorarioTecnicos, "HorarioTecnicoId", "HorarioTecnicoId");
            return View();
        }

        // POST: HorarioParaTrocas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HorarioParaTrocaId,HorarioTecnicoId")] HorarioParaTroca horarioParaTroca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(horarioParaTroca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HorarioTecnicoId"] = new SelectList(_context.HorarioTecnicos, "HorarioTecnicoId", "HorarioTecnicoId", horarioParaTroca.HorarioTecnicoId);
            return View(horarioParaTroca);
        }

        // GET: HorarioParaTrocas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioParaTroca = await _context.HorarioParaTroca.FindAsync(id);
            if (horarioParaTroca == null)
            {
                return NotFound();
            }
            ViewData["HorarioTecnicoId"] = new SelectList(_context.HorarioTecnicos, "HorarioTecnicoId", "HorarioTecnicoId", horarioParaTroca.HorarioTecnicoId);
            return View(horarioParaTroca);
        }

        // POST: HorarioParaTrocas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HorarioParaTrocaId,HorarioTecnicoId")] HorarioParaTroca horarioParaTroca)
        {
            if (id != horarioParaTroca.HorarioParaTrocaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horarioParaTroca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorarioParaTrocaExists(horarioParaTroca.HorarioParaTrocaId))
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
            ViewData["HorarioTecnicoId"] = new SelectList(_context.HorarioTecnicos, "HorarioTecnicoId", "HorarioTecnicoId", horarioParaTroca.HorarioTecnicoId);
            return View(horarioParaTroca);
        }

        // GET: HorarioParaTrocas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioParaTroca = await _context.HorarioParaTroca
                .Include(h => h.HorarioTecnicos)
                .FirstOrDefaultAsync(m => m.HorarioParaTrocaId == id);
            if (horarioParaTroca == null)
            {
                return NotFound();
            }

            return View(horarioParaTroca);
        }

        // POST: HorarioParaTrocas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horarioParaTroca = await _context.HorarioParaTroca.FindAsync(id);
            _context.HorarioParaTroca.Remove(horarioParaTroca);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorarioParaTrocaExists(int id)
        {
            return _context.HorarioParaTroca.Any(e => e.HorarioParaTrocaId == id);
        }
    }
}
