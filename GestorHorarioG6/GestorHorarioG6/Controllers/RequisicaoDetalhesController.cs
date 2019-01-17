using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestorHorarioG6.Models;
using GestorHorarioG6.Data;

namespace GestorHorarioG6.Controllers
{
    public class RequisicaoDetalhesController : Controller
    {
        private readonly GestorHorarioG6Context _context;
        private readonly int PageSize = 5;

        public RequisicaoDetalhesController(GestorHorarioG6Context context)
        {
            _context = context;
        }

        // GET: RequisicaoDetalhes
        public async Task<IActionResult> Index()
        {
            var gestorHorarioG6Context = _context.RequisicaoDetalhe.Include(r => r.Requisicao).Include(r => r.Servico);
            return View(await gestorHorarioG6Context.ToListAsync());
        }

        // GET: Requisicoes/Aprovadas
        public async Task<IActionResult> Aprovadas(int page = 1)
        {
            var databaseContext = _context.RequisicaoDetalhe.Include(r => r.Requisicao).Include(r => r.Servico);
            var total = await databaseContext.CountAsync();
            return View(new RequisicaoDetalhesListViewModel
            {
                RequisicaoDetalhes = await databaseContext
                        .Where(r => r.Aprovado == true)
                        .OrderByDescending(r => r.Requisicao.Dia)
                        .ToListAsync(),
                PagingInfo = new PaginationViewModel
                {
                    CurrentPage = page,
                    ItensPerPage = PageSize,
                    TotalItems = total
                }
            });
        }

        // GET: RequisicaoDetalhes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requisicaoDetalhe = await _context.RequisicaoDetalhe
                .Include(r => r.Requisicao)
                .Include(r => r.Servico)
                .FirstOrDefaultAsync(m => m.RequisicaoDetalheId == id);
            if (requisicaoDetalhe == null)
            {
                return NotFound();
            }

            return View(requisicaoDetalhe);
        }

        // POST: Requisicoes/Concluir/1
        [HttpPost]
        public async Task<IActionResult> Concluir(int id)
        {
            var detalhe = await _context.RequisicaoDetalhe.FindAsync(id);
            if (!detalhe.HoraConcluido.HasValue)
                detalhe.HoraConcluido = DateTime.Now;
            else
                detalhe.HoraConcluido = null;
            _context.RequisicaoDetalhe.Update(detalhe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: RequisicaoDetalhes/Create/1
        public IActionResult Create(int? id)
        {
            if (id == null)
                return NotFound();

            ViewData["RequisicaoId"] = id;
            ViewData["ServicoId"] = new SelectList(_context.Servico, "ServicoId", "Nome");
            return View();
        }

        // POST: RequisicaoDetalhes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequisicaoDetalheId,RequisicaoId,ServicoId,HoraCriticaDe,HoraCriticaAte,DuraçãoEstimada,Notas,Aprovado,HoraDeInicio,HoraDeFim,HoraConcluido")] RequisicaoDetalhe requisicaoDetalhe)
        {
            if (ModelState.IsValid)
            {
                requisicaoDetalhe.DuraçãoEstimada = _context.Servico.Where(s => s.ServicoId == requisicaoDetalhe.ServicoId).FirstOrDefault().DuracaoMedia;
                _context.Add(requisicaoDetalhe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RequisicaoId"] = new SelectList(_context.Requisicao, "RequisicaoId", "RequisicaoId", requisicaoDetalhe.RequisicaoId);
            ViewData["ServicoId"] = new SelectList(_context.Servico, "ServicoId", "Nome", requisicaoDetalhe.ServicoId);
            return View(requisicaoDetalhe);
        }

        // GET: RequisicaoDetalhes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requisicaoDetalhe = await _context.RequisicaoDetalhe.FindAsync(id);
            if (requisicaoDetalhe == null)
            {
                return NotFound();
            }
            ViewData["RequisicaoId"] = new SelectList(_context.Requisicao, "RequisicaoId", "RequisicaoId", requisicaoDetalhe.RequisicaoId);
            ViewData["ServicoId"] = new SelectList(_context.Servico, "ServicoId", "Nome", requisicaoDetalhe.ServicoId);
            return View(requisicaoDetalhe);
        }

        // POST: RequisicaoDetalhes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RequisicaoDetalheId,RequisicaoId,ServicoId,HoraCriticaDe,HoraCriticaAte,DuraçãoEstimada,Notas,Aprovado,HoraDeInicio,HoraDeFim,HoraConcluido")] RequisicaoDetalhe requisicaoDetalhe)
        {
            if (id != requisicaoDetalhe.RequisicaoDetalheId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requisicaoDetalhe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequisicaoDetalheExists(requisicaoDetalhe.RequisicaoDetalheId))
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
            ViewData["RequisicaoId"] = new SelectList(_context.Requisicao, "RequisicaoId", "RequisicaoId", requisicaoDetalhe.RequisicaoId);
            ViewData["ServicoId"] = new SelectList(_context.Servico, "ServicoId", "Nome", requisicaoDetalhe.ServicoId);
            return View(requisicaoDetalhe);
        }

        // GET: RequisicaoDetalhes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requisicaoDetalhe = await _context.RequisicaoDetalhe
                .Include(r => r.Requisicao)
                .Include(r => r.Servico)
                .FirstOrDefaultAsync(m => m.RequisicaoDetalheId == id);
            if (requisicaoDetalhe == null)
            {
                return NotFound();
            }

            return View(requisicaoDetalhe);
        }

        // POST: RequisicaoDetalhes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var requisicaoDetalhe = await _context.RequisicaoDetalhe.FindAsync(id);
            _context.RequisicaoDetalhe.Remove(requisicaoDetalhe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequisicaoDetalheExists(int id)
        {
            return _context.RequisicaoDetalhe.Any(e => e.RequisicaoDetalheId == id);
        }
    }
}
