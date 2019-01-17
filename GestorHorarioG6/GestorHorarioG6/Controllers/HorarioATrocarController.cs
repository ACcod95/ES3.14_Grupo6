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
    public class HorarioATrocarController : Controller
    {
        private readonly GestorHorarioG6Context _context;

        public HorarioATrocarController(GestorHorarioG6Context context)
        {
            _context = context;
        }

        // GET: HorarioATrocar
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.HorarioATrocar.Include(h => h.HorarioTecnicos).ToListAsync());
        }

        // GET: HorarioATrocar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioATrocar = await _context.HorarioATrocar
                .Include(h => h.HorarioTecnicos)
                .FirstOrDefaultAsync(m => m.HorarioATrocarId == id);
            if (horarioATrocar == null)
            {
                return NotFound();
            }

            return View(horarioATrocar);
        }

        // GET: HorarioATrocar/Create
        public IActionResult Create()
        {
            ViewData["HorarioTecnicoId"] = new SelectList(_context.HorarioTecnicos, "HorarioTecnicoId", "HorarioTecnicoId");
            return View();
        }

        // POST: HorarioATrocar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HorarioATrocarId,HorarioTecnicoId")] HorarioATrocar horarioATrocar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(horarioATrocar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HorarioTecnicoId"] = new SelectList(_context.HorarioTecnicos, "HorarioTecnicoId", "HorarioTecnicoId", horarioATrocar.HorarioTecnicoId);
            return View(horarioATrocar);
        }

        // GET: HorarioATrocar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioATrocar = await _context.HorarioATrocar.FindAsync(id);
            if (horarioATrocar == null)
            {
                return NotFound();
            }
            ViewData["HorarioTecnicoId"] = new SelectList(_context.HorarioTecnicos, "HorarioTecnicoId", "HorarioTecnicoId", horarioATrocar.HorarioTecnicoId);
            return View(horarioATrocar);
        }

        // POST: HorarioATrocar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HorarioATrocarId,HorarioTecnicoId")] HorarioATrocar horarioATrocar)
        {
            if (id != horarioATrocar.HorarioATrocarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horarioATrocar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorarioATrocarExists(horarioATrocar.HorarioATrocarId))
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
            ViewData["HorarioTecnicoId"] = new SelectList(_context.HorarioTecnicos, "HorarioTecnicoId", "HorarioTecnicoId", horarioATrocar.HorarioTecnicoId);
            return View(horarioATrocar);
        }

        // GET: HorarioATrocar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioATrocar = await _context.HorarioATrocar
                .Include(h => h.HorarioTecnicos)
                .FirstOrDefaultAsync(m => m.HorarioATrocarId == id);
            if (horarioATrocar == null)
            {
                return NotFound();
            }

            return View(horarioATrocar);
        }

        // POST: HorarioATrocar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horarioATrocar = await _context.HorarioATrocar.FindAsync(id);
            _context.HorarioATrocar.Remove(horarioATrocar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorarioATrocarExists(int id)
        {
            return _context.HorarioATrocar.Any(e => e.HorarioATrocarId == id);
        }
    }
}
