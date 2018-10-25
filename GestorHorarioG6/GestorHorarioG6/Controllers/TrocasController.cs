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
    public class TrocasController : Controller
    {
        private readonly GestorHorarioG6Context _context;

        public TrocasController(GestorHorarioG6Context context)
        {
            _context = context;
        }

        // GET: Trocas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Trocas.ToListAsync());
        }

        // GET: Trocas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trocas = await _context.Trocas
                .FirstOrDefaultAsync(m => m.TrocasID == id);
            if (trocas == null)
            {
                return NotFound();
            }

            return View(trocas);
        }

        // GET: Trocas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trocas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrocasID,IDFuncionario1,IDFuncionario2,Turno1,Turno2,Conhecimento,Aprovado")] Trocas trocas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trocas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trocas);
        }

        // GET: Trocas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trocas = await _context.Trocas.FindAsync(id);
            if (trocas == null)
            {
                return NotFound();
            }
            return View(trocas);
        }

        // POST: Trocas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrocasID,IDFuncionario1,IDFuncionario2,Turno1,Turno2,Conhecimento,Aprovado")] Trocas trocas)
        {
            if (id != trocas.TrocasID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trocas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrocasExists(trocas.TrocasID))
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
            return View(trocas);
        }

        // GET: Trocas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trocas = await _context.Trocas
                .FirstOrDefaultAsync(m => m.TrocasID == id);
            if (trocas == null)
            {
                return NotFound();
            }

            return View(trocas);
        }

        // POST: Trocas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trocas = await _context.Trocas.FindAsync(id);
            _context.Trocas.Remove(trocas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrocasExists(int id)
        {
            return _context.Trocas.Any(e => e.TrocasID == id);
        }
    }
}
