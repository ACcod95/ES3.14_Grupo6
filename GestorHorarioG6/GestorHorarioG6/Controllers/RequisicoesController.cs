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
    public class RequisicoesController : Controller
    {
        private readonly GestorHorarioG6Context _context;
        private readonly int PageSize = 5;

        public RequisicoesController(GestorHorarioG6Context context)
        {
            _context = context;
        }

        // GET: Requisicoes
        public async Task<IActionResult> Index(RequisicoesListViewModel model = null, int page = 1)
        {
            DateTime day = DateTime.MinValue;

            if (model != null && model.CurrentDay != DateTime.MinValue)
            {
                day = model.CurrentDay;
            }

            var databaseContext = _context.Requisicao.Include(r => r.Departamento)
                .Where(r => day == DateTime.MinValue || r.Detalhes.OrderBy(d => d.HoraDeInicio).FirstOrDefault().HoraDeInicio.Date.Equals(day.Date));
            var total = await databaseContext.CountAsync();

            if (page > (total / PageSize) + 1)
            {
                page = 1;
            }

            var requisicoes = await databaseContext
                .OrderBy(r => r.Detalhes.OrderBy(d => d.HoraDeInicio).FirstOrDefault().HoraDeInicio)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            return View(new RequisicoesListViewModel
            {
                Requisicoes = requisicoes,
                PagingInfo = new PaginationViewModel
                {
                    CurrentPage = page,
                    ItensPerPage = PageSize,
                    TotalItems = total
                },
                CurrentDay = day
            });
        }

        // POST: Requisicoes/Aprovar/1
        [HttpPost]
        public async Task<IActionResult> Aprovar(int id)
        {
            var detalhe = await _context.RequisicaoDetalhe.FindAsync(id);
            if (detalhe.Aprovado)
                detalhe.Aprovado = false;
            else
                detalhe.Aprovado = true;
            _context.RequisicaoDetalhe.Update(detalhe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Requisicoes/Aprovadas
        public async Task<IActionResult> Aprovadas()
        {
            var databaseContext = _context.RequisicaoDetalhe.Include(r => r.Requisicao).Include(r => r.Servico);
            return View(await databaseContext.Where(r => r.Aprovado == true).ToListAsync());
        }

        // GET: Requisicoes/Create
        public IActionResult Create()
        {
            ViewData["Servico"] = new SelectList(_context.Servico, "ServicoId", "Nome");
            ViewData["Departamento"] = new SelectList(_context.Departamento, "DepartamentoId", "Nome");
            return View();
        }

        // POST: Requisicoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequisicaoId,DepartamentoId,Detalhes")] Requisicao requisicao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(requisicao);
                await _context.SaveChangesAsync();
                return RedirectToAction("Created/" + requisicao.RequisicaoId);
            }
            ViewData["Servico"] = new SelectList(_context.Servico, "ServicoId", "Nome");
            ViewData["Departamento"] = new SelectList(_context.Departamento, "DepartamentoId", "Nome", requisicao.DepartamentoId);
            return View(requisicao);
        }

        // GET: Requisicoes/Created/5
        public async Task<IActionResult> Created(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requisicao = await _context.Requisicao.FindAsync(id);
            if (requisicao == null)
            {
                return NotFound();
            }
            ViewData["Servico"] = new SelectList(_context.Servico, "ServicoId", "Nome");
            ViewData["Departamento"] = new SelectList(_context.Departamento, "DepartamentoId", "Nome", requisicao.DepartamentoId);
            return View(requisicao);
        }

        // POST: Requisicoes/Created/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Created(int id, [Bind("RequisicaoId,DepartamentoId,Detalhes")] Requisicao requisicao)
        {
            if (id != requisicao.RequisicaoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requisicao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequisicaoExists(requisicao.RequisicaoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewData["Servico"] = new SelectList(_context.Servico, "ServicoId", "Nome");
            ViewData["Departamento"] = new SelectList(_context.Departamento, "DepartamentoId", "Nome", requisicao.DepartamentoId);
            return View(requisicao);
        }

        // GET: Requisicoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requisicao = await _context.Requisicao.FindAsync(id);
            if (requisicao == null)
            {
                return NotFound();
            }
            return View(requisicao);
        }

        // POST: Requisicoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RequisicaoId,DepartamentoId,Detalhes")] Requisicao requisicao)
        {
            if (id != requisicao.RequisicaoId)
            {
                return NotFound();
            }

            if (requisicao.Detalhes.OrderBy(d => d.HoraDeInicio).FirstOrDefault().HoraDeInicio.CompareTo(DateTime.Now) < 1)
                ModelState.AddModelError("Detalhes", "A hora de início não pode ser anterior ao presente.");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requisicao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequisicaoExists(requisicao.RequisicaoId))
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
            return View(requisicao);
        }

        // GET: Requisicoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requisicao = await _context.Requisicao
                .FirstOrDefaultAsync(m => m.RequisicaoId == id);
            if (requisicao == null)
            {
                return NotFound();
            }

            return View(requisicao);
        }

        // POST: Requisicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string btn)
        {
            var requisicao = await _context.Requisicao.FindAsync(id);
            _context.Requisicao.Remove(requisicao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequisicaoExists(int id)
        {
            return _context.Requisicao.Any(e => e.RequisicaoId == id);
        }
    }
}
