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
    public class RegrasGeraisController : Controller
    {
        private readonly GestorHorarioG6Context _context;

        public RegrasGeraisController(GestorHorarioG6Context context)
        {
            _context = context;
        }

        // GET: RegrasGerais
        public async Task<IActionResult> Index()
        {
            return View(await _context.RegrasGerais.ToListAsync());
        }

        // GET: RegrasGerais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regrasGerais = await _context.RegrasGerais
                .FirstOrDefaultAsync(m => m.RegraId == id);
            if (regrasGerais == null)
            {
                return NotFound();
            }

            return View(regrasGerais);
        }

        // GET: RegrasGerais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RegrasGerais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegraId,Nome,Descricao,Horas")] RegrasGerais regrasGerais)
        {
            if (ModelState.IsValid)
            {
                _context.Add(regrasGerais);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(regrasGerais);
        }

        // GET: RegrasGerais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regrasGerais = await _context.RegrasGerais.FindAsync(id);
            if (regrasGerais == null)
            {
                return NotFound();
            }
            return View(regrasGerais);
        }

        // POST: RegrasGerais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegraId,Nome,Descricao,Horas")] RegrasGerais regrasGerais)
        {
            if (id != regrasGerais.RegraId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(regrasGerais);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegrasGeraisExists(regrasGerais.RegraId))
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
            return View(regrasGerais);
        }

        // GET: RegrasGerais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regrasGerais = await _context.RegrasGerais
                .FirstOrDefaultAsync(m => m.RegraId == id);
            if (regrasGerais == null)
            {
                return NotFound();
            }

            return View(regrasGerais);
        }

        // POST: RegrasGerais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var regrasGerais = await _context.RegrasGerais.FindAsync(id);
            _context.RegrasGerais.Remove(regrasGerais);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegrasGeraisExists(int id)
        {
            return _context.RegrasGerais.Any(e => e.RegraId == id);
        }
    }
}
