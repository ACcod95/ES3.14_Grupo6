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
    public class TrocasController : Controller
    {
        private const int PAGE_SIZE = 5;
        private readonly GestorHorarioG6Context _context;

        public TrocasController(GestorHorarioG6Context context)
        {
            _context = context;
        }

        // GET: Trocas
        public async Task<IActionResult> Index(PedidoDeTrocaViewModel model = null, int page = 1)
        {
            string nome = null;
            DateTime? data = null;
            if (model != null && model.DataInicio != null || model.CurrentNome != null)
            {
                nome = model.CurrentNome;
                data = model.DataInicio;
                page = 1;
            }
            IQueryable<Trocas> Trocas;
            int numHorario;
            IEnumerable<Trocas> PedidoDeTrocaViewModel;

            if (data.HasValue && string.IsNullOrEmpty(nome)) //data
            {
                int ano = data.Value.Year;
                int mes = data.Value.Month;
                int dia = data.Value.Day;

                Trocas = _context.Trocas
                   .Where(h => h.Data.Year.Equals(ano) && h.Data.Month.Equals(mes) && h.Data.Day.Equals(dia));

                numHorario = await Trocas.CountAsync();

                PedidoDeTrocaViewModel = await Trocas
                    .Include(h => h.Funcionario)
                    .Include(h => h.Estado)
                    .Include(h => h.HorarioATrocar)
                    .Include(h => h.HorarioParaTroca)
                    .OrderByDescending(h => h.Data)
                    .Skip(PAGE_SIZE * (page - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();
            }
            else if (!string.IsNullOrEmpty(nome) && !data.HasValue) // nome
            {
                Trocas = _context.Trocas
                    .Where(h => h.Funcionario.Nome.Contains(nome.Trim()));

                numHorario = await Trocas.CountAsync();

                PedidoDeTrocaViewModel = await Trocas
                    .Include(h => h.Funcionario)
                    .Include(h => h.Estado)
                    .Include(h => h.HorarioATrocar)
                    .Include(h => h.HorarioParaTroca)
                    .OrderByDescending(h => h.Data)
                    .Skip(PAGE_SIZE * (page - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();
            }
            else
            {
                Trocas = _context.Trocas;

                numHorario = await Trocas.CountAsync();

                PedidoDeTrocaViewModel = await Trocas
                  .Include(h => h.Funcionario)
                    .Include(h => h.Estado)
                    .Include(h => h.HorarioATrocar)
                    .Include(h => h.HorarioParaTroca)
                    .OrderByDescending(h => h.Data)
                  .Skip(PAGE_SIZE * (page - 1))
                  .Take(PAGE_SIZE)
                  .ToListAsync();
            }
            if (page > (numHorario / PAGE_SIZE) + 1)
            {
                page = 1;
            }

            if (PedidoDeTrocaViewModel.Count() == 0)
            {
                TempData["NoItemsFound"] = "Não foram encontrados resultados para a sua pesquisa";
            }

            return View(new PedidoDeTrocaViewModel
            {
                Trocas = PedidoDeTrocaViewModel,
                Pagination = new PaginationViewModel
                {
                    CurrentPage = page,
                    ItensPerPage = PAGE_SIZE,
                    TotalItems = numHorario
                },
                CurrentNome = nome,
                DataInicio = data
            });
        }

        // GET: Trocas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trocas = await _context.Trocas
                .Include(t => t.Estado)
                .Include(t => t.Funcionario)
                .Include(t => t.HorarioATrocar)
                .Include(t => t.HorarioParaTroca)
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
            ViewData["EstadoTrocaId"] = new SelectList(_context.Set<Estado>(), "EstadoTrocaId", "EstadoTrocaId");
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "FuncionarioId", "Nome");
            ViewData["HorarioATrocarId"] = new SelectList(_context.Set<HorarioATrocar>(), "HorarioATrocarId", "HorarioATrocarId");
            ViewData["HorarioParaTrocaId"] = new SelectList(_context.Set<HorarioParaTroca>(), "HorarioParaTrocaId", "HorarioParaTrocaId");
            return View();
        }

        // POST: Trocas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrocasID,FuncionarioId,Data,HorarioATrocarId,HorarioParaTrocaId,EstadoTrocaId")] Trocas trocas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trocas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstadoTrocaId"] = new SelectList(_context.Set<Estado>(), "EstadoTrocaId", "EstadoTrocaId", trocas.EstadoTrocaId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "FuncionarioId", "Nome", trocas.FuncionarioId);
            ViewData["HorarioATrocarId"] = new SelectList(_context.Set<HorarioATrocar>(), "HorarioATrocarId", "HorarioATrocarId", trocas.HorarioATrocarId);
            ViewData["HorarioParaTrocaId"] = new SelectList(_context.Set<HorarioParaTroca>(), "HorarioParaTrocaId", "HorarioParaTrocaId", trocas.HorarioParaTrocaId);
            return View(trocas);
        }

        // GET: Trocas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trocas = await _context.Trocas
                .Include(t => t.Estado)
                .Include(t => t.Funcionario)
                .Include(t => t.HorarioATrocar)
                .Include(t => t.HorarioParaTroca)
                .FirstOrDefaultAsync(m => m.TrocasID == id);
            if (trocas == null)
            {
                return NotFound();
            }
            ViewData["EstadoTrocaId"] = new SelectList(_context.Set<Estado>(), "EstadoTrocaId", "EstadoTrocaId", trocas.EstadoTrocaId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "FuncionarioId", "Nome", trocas.FuncionarioId);
            ViewData["HorarioATrocarId"] = new SelectList(_context.Set<HorarioATrocar>(), "HorarioATrocarId", "HorarioATrocarId", trocas.HorarioATrocarId);
            ViewData["HorarioParaTrocaId"] = new SelectList(_context.Set<HorarioParaTroca>(), "HorarioParaTrocaId", "HorarioParaTrocaId", trocas.HorarioParaTrocaId);
            return View(trocas);
        }

        // POST: Trocas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrocasID,FuncionarioId,Data,HorarioATrocarId,HorarioParaTrocaId,EstadoTrocaId")] Trocas trocas)
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
            ViewData["EstadoTrocaId"] = new SelectList(_context.Set<Estado>(), "EstadoTrocaId", "EstadoTrocaId", trocas.EstadoTrocaId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "FuncionarioId", "Nome", trocas.FuncionarioId);
            ViewData["HorarioATrocarId"] = new SelectList(_context.Set<HorarioATrocar>(), "HorarioATrocarId", "HorarioATrocarId", trocas.HorarioATrocarId);
            ViewData["HorarioParaTrocaId"] = new SelectList(_context.Set<HorarioParaTroca>(), "HorarioParaTrocaId", "HorarioParaTrocaId", trocas.HorarioParaTrocaId);
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
                .Include(t => t.Estado)
                .Include(t => t.Funcionario)
                .Include(t => t.HorarioATrocar)
                .Include(t => t.HorarioParaTroca)
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

        public async Task<IActionResult> Aprovar(int id)
        {
            var trocas= await _context.Trocas.FindAsync(id);

            if (id != trocas.TrocasID)
            {
                return NotFound();
            }

            Estado idEstadoAprovado = _context.Estado.SingleOrDefault(e => e.Nome == "Aprovado");

            trocas.EstadoTrocaId = idEstadoAprovado.EstadoTrocaId; 
            
            int id1 = (from p in _context.Trocas
                       where p.TrocasID == id
                       select p.HorarioATrocar.HorarioTecnicoId).Single();

            HorarioTecnicos idHora1 = _context.HorarioTecnicos.SingleOrDefault(h => h.HorarioTecnicoId == id1);

            var horarioATrocar = await _context.HorarioTecnicos.FindAsync(idHora1.HorarioTecnicoId);

            int nH1 = (from p in _context.Trocas
                            where p.TrocasID == id
                            select p.HorarioATrocar.HorarioTecnicos.Funcionario.FuncionarioId).Single();

            horarioATrocar.FuncionarioId = nH1;

            int id2 = (from p in _context.Trocas
                       where p.TrocasID == id
                       select p.HorarioParaTroca.HorarioTecnicoId).Single();

            HorarioTecnicos idHora2 = _context.HorarioTecnicos.SingleOrDefault(h => h.HorarioTecnicoId == id2);

            var horarioParaTroca = await _context.HorarioTecnicos.FindAsync(idHora2.HorarioTecnicoId);

            int nH2 = (from p in _context.Trocas
                            where p.TrocasID == id
                            select p.HorarioParaTroca.HorarioTecnicos.Funcionario.FuncionarioId).Single();

            horarioParaTroca.FuncionarioId = nH2;

            try
            {
                UpdateHorario(_context, horarioATrocar);
                UpdateHorario(_context, horarioParaTroca);
                TempData["UpdatedSuccess"] = "Pedido aprovado com sucesso";
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

    
    private void UpdateHorario(GestorHorarioG6Context db, HorarioTecnicos horario)
    {
        db.HorarioTecnicos.Update(horario);
        db.SaveChanges();
    }

    // GET: PedidoTrocaTurnos/NaoAprovar/5
    public async Task<IActionResult> NaoAprovar(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var Trocas = await _context.Trocas
            .Include(p => p.Funcionario)
            .Include(p => p.Estado)
            .Include(p => p.HorarioATrocar.HorarioTecnicos.Funcionario)
            .Include(p => p.HorarioParaTroca.HorarioTecnicos.Funcionario)
            .Include(p => p.HorarioATrocar.HorarioTecnicos.Turno)
            .Include(p => p.HorarioParaTroca.HorarioTecnicos.Turno)
            .FirstOrDefaultAsync(m => m.TrocasID == id);

        if (Trocas == null)
        {
            return NotFound();
        }

        return View(Trocas);
    }

    // POST: PedidoTrocaTurnos/NaoAprovar/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> NaoAprovar(int id)
    {
        var pedidoTrocaTurnos = await _context.Trocas.FindAsync(id);

        if (id != pedidoTrocaTurnos.TrocasID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {

            Estado idEstadoAprovado = _context.Estado.SingleOrDefault(e => e.Nome == "Não Aprovado");

            pedidoTrocaTurnos.EstadoTrocaId = idEstadoAprovado.EstadoTrocaId; 

            try
            {
                _context.Update(pedidoTrocaTurnos);
                await _context.SaveChangesAsync();
                TempData["NotAproved"] = "O pedido foi arquivado!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoTrocaTurnosExists(pedidoTrocaTurnos.TrocasID))
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

        return View(pedidoTrocaTurnos);
    }

        private bool PedidoTrocaTurnosExists(int id)
        {
            return _context.Trocas.Any(e => e.TrocasID == id);
        }
    }

}