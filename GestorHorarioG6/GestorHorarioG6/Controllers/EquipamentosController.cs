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
    public class EquipamentosController : Controller
    {
        private const int PAGE_SIZE = 5;
        private readonly GestorHorarioG6Context _context;

        public EquipamentosController(GestorHorarioG6Context context)
        {
            _context = context;
        }

        // GET: Equipamentos
        public async Task<IActionResult> Index(string searchString, string searchString2, EquipamentosViewModel model = null, int page=1)
        {
            string nome = null;

            if(model != null && model.CurrentNome != null)
            {
                nome = model.CurrentNome;
                page = 1;
            }
            var equipamentoContext = from e in _context.Equipamento select e;

            if (!String.IsNullOrEmpty(searchString))
            {
                equipamentoContext = _context.Equipamento.Where(s => s.Nome.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(searchString2))
            {
                equipamentoContext = _context.Equipamento.Where(s => s.Bloco.Nome.Contains(searchString2));
            }

            equipamentoContext = equipamentoContext.Include(e => e.Bloco);
            var total = await equipamentoContext.CountAsync();

            if (page > (total / PAGE_SIZE) +1)
            {
                page = 1;
            }

            var listEqui = await equipamentoContext
                .OrderBy(p => p.EquipamentoId)
                .Skip(PAGE_SIZE * (page - 1))
                .Take(PAGE_SIZE)
                .ToListAsync();

            return View(new EquipamentosViewModel
            {
                Equipamento = listEqui,
                PageInfo = new PaginationViewModel
                {
                    CurrentPage = page,
                    ItensPerPage = PAGE_SIZE,
                    TotalItems = total
                },
                CurrentNome = nome
            });
        }

        // GET: Equipamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipamento = await _context.Equipamento
                .Include(e => e.Bloco)
                .FirstOrDefaultAsync(m => m.EquipamentoId == id);
            if (equipamento == null)
            {
                return NotFound();
            }

            return View(equipamento);
        }

        // GET: Equipamentos/Create
        public IActionResult Create()
        {
            ViewData["BlocoId"] = new SelectList(_context.Bloco, "BlocoId", "Nome");
            return View();
        }

        // POST: Equipamentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EquipamentoId,Nome,BlocoId")] Equipamento equipamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlocoId"] = new SelectList(_context.Bloco, "BlocoId", "Nome");
            return View(equipamento);
        }

        // GET: Equipamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipamento = await _context.Equipamento.FindAsync(id);
            if (equipamento == null)
            {
                return NotFound();
            }
            ViewData["BlocoId"] = new SelectList(_context.Bloco, "BlocoId", "Nome");
            return View(equipamento);
        }

        // POST: Equipamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EquipamentoId,Nome,BlocoId")] Equipamento equipamento)
        {
            if (id != equipamento.EquipamentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipamentoExists(equipamento.EquipamentoId))
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
            ViewData["BlocoId"] = new SelectList(_context.Bloco, "BlocoId", "Nome");
            return View(equipamento);
        }

        // GET: Equipamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipamento = await _context.Equipamento
                .Include(e => e.Bloco)
                .FirstOrDefaultAsync(m => m.EquipamentoId == id);
            if (equipamento == null)
            {
                return NotFound();
            }

            return View(equipamento);
        }

        // POST: Equipamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipamento = await _context.Equipamento.FindAsync(id);
            _context.Equipamento.Remove(equipamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipamentoExists(int id)
        {
            return _context.Equipamento.Any(e => e.EquipamentoId == id);
        }
    }
}
