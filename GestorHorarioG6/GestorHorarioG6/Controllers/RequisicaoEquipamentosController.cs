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

        public RequisicaoEquipamentosController(GestorHorarioG6Context context)
        {
            _context = context;
        }

        // GET: RequisicaoEquipamentos
        public async Task<IActionResult> Index()
        {
            var gestorHorarioG6Context = _context.RequisicaoEquipamento.Include(r => r.Bloco).Include(r => r.Equipamento);
            return View(await gestorHorarioG6Context.ToListAsync());
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
