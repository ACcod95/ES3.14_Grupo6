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
    public class RequisicaoEquipamentosController : Controller
    {
        private readonly GestorHorarioG6Context _context;
        private const int PAGE_SIZE = 5;

        public RequisicaoEquipamentosController(GestorHorarioG6Context context)
        {
            _context = context;
        }

        // GET: RequisicaoEquipamentos
        public async Task<IActionResult> Index(RequisicoesEquipamentosListViewModel model = null, int page = 1)
        {
            DateTime day = DateTime.MinValue;

            if (model != null && model.CurrentDay != DateTime.MinValue)
            {
                day = model.CurrentDay;
                page = 1;
            }

            var requisicaoContext = _context.RequisicaoEquipamento.Include(e => e.Equipamento).Include(b => b.Bloco).
                Where(r => day == DateTime.MinValue || r.HoraDeInicio.Date.Equals(day.Date));
            var total = await requisicaoContext.CountAsync();

            if (page > (total / PAGE_SIZE) + 1)
            {
                page = 1;
            }

            var requisicaoequipamento = await requisicaoContext.
                OrderBy(p => p.RequisicaoEquipamentoId)
                .Skip((page - 1) * PAGE_SIZE)
                .Take(PAGE_SIZE).
                ToListAsync();

            return View(new RequisicoesEquipamentosListViewModel
            {
                RequisicaoEquipamento = requisicaoequipamento,
                PagingInfo = new PaginationViewModel
                {
                    CurrentPage = page,
                    ItensPerPage = PAGE_SIZE,
                    TotalItems = total
                },
                CurrentDay = day
            });
        }

        // GET: RequisicaoEquipamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requisicaoEquipamento = await _context.RequisicaoEquipamento
                .Include(r => r.Bloco)
                .Include(r => r.Equipamento)
                .FirstOrDefaultAsync(m => m.RequisicaoEquipamentoId == id);
            if (requisicaoEquipamento == null)
            {
                return NotFound();
            }

            return View(requisicaoEquipamento);
        }

        // GET: RequisicaoEquipamentos/Create
        public IActionResult Create()
        {
            ViewData["BlocoId"] = new SelectList(_context.Bloco, "BlocoId", "Nome");
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamento, "EquipamentoId", "Nome");
            return View();
        }

        // POST: RequisicaoEquipamentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequisicaoEquipamentoId,EquipamentoId,HoraDeInicio,HoraDeFim,BlocoId")] RequisicaoEquipamento requisicaoEquipamento)
        {
            if (DateTime.Now > requisicaoEquipamento.HoraDeInicio)
                ModelState.AddModelError("HoraDeInicio", "O dia e hora de início não pode ser anterior ao dia e hora actual.");
            if (requisicaoEquipamento.HoraDeFim < requisicaoEquipamento.HoraDeInicio)
                ModelState.AddModelError("HoraDeFim", "A dia e hora de fim não pode ser anterior ao dia e hora de início.");

            if (ModelState.IsValid)
            {
                _context.Add(requisicaoEquipamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlocoId"] = new SelectList(_context.Bloco, "BlocoId", "Nome", requisicaoEquipamento.BlocoId);
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamento, "EquipamentoId", "Nome", requisicaoEquipamento.EquipamentoId);
            return View(requisicaoEquipamento);
        }

        // GET: RequisicaoEquipamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requisicaoEquipamento = await _context.RequisicaoEquipamento.FindAsync(id);
            if (requisicaoEquipamento == null)
            {
                return NotFound();
            }
            ViewData["BlocoId"] = new SelectList(_context.Bloco, "BlocoId", "Nome", requisicaoEquipamento.BlocoId);
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamento, "EquipamentoId", "Nome", requisicaoEquipamento.EquipamentoId);
            return View(requisicaoEquipamento);
        }

        // POST: RequisicaoEquipamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RequisicaoEquipamentoId,EquipamentoId,HoraDeInicio,HoraDeFim,BlocoId")] RequisicaoEquipamento requisicaoEquipamento)
        {
            if (DateTime.Now > requisicaoEquipamento.HoraDeInicio)
                ModelState.AddModelError("HoraDeInicio", "O dia e hora de início não pode ser anterior ao dia e hora actual.");
            if (requisicaoEquipamento.HoraDeFim < requisicaoEquipamento.HoraDeInicio)
                ModelState.AddModelError("HoraDeFim", "A dia e hora de fim não pode ser anterior ao dia e hora de início.");

            if (id != requisicaoEquipamento.RequisicaoEquipamentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requisicaoEquipamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequisicaoEquipamentoExists(requisicaoEquipamento.RequisicaoEquipamentoId))
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
            ViewData["BlocoId"] = new SelectList(_context.Bloco, "BlocoId", "Nome", requisicaoEquipamento.BlocoId);
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamento, "EquipamentoId", "Nome", requisicaoEquipamento.EquipamentoId);
            return View(requisicaoEquipamento);
        }

        // GET: RequisicaoEquipamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requisicaoEquipamento = await _context.RequisicaoEquipamento
                .Include(r => r.Bloco)
                .Include(r => r.Equipamento)
                .FirstOrDefaultAsync(m => m.RequisicaoEquipamentoId == id);
            if (requisicaoEquipamento == null)
            {
                return NotFound();
            }

            return View(requisicaoEquipamento);
        }

        // POST: RequisicaoEquipamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var requisicaoEquipamento = await _context.RequisicaoEquipamento.FindAsync(id);
            _context.RequisicaoEquipamento.Remove(requisicaoEquipamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequisicaoEquipamentoExists(int id)
        {
            return _context.RequisicaoEquipamento.Any(e => e.RequisicaoEquipamentoId == id);
        }
    }
}