using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestorHorarioG6.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GestorHorarioG6.Controllers
{
    public class FuncionariosController : Controller
    {
        private const int PAGE_SIZE = 5;
        private readonly GestorHorarioG6Context _context;

        public FuncionariosController(GestorHorarioG6Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }



        // GET: Funcionarios
        public async Task<IActionResult> Funcionario(FuncionarioViewModel  model = null, int page = 1)
        {
            string nome = null;

            if (model != null && model.CurrentNome != null)
            {
                nome = model.CurrentNome;
                page = 1;
            }
        
            var func = _context.Funcionario.Include(f => f.Cargo).Where(f => nome == null || f.Nome.Contains(nome));
            var total = await func.CountAsync();

            if (page > (total / PAGE_SIZE) + 1)
            {
                page = 1;
            }
            
            var listFunc = await func
               .OrderBy(p => p.FuncionarioId)   
               .Skip(PAGE_SIZE * (page - 1))
               .Take(PAGE_SIZE)
               .ToListAsync();

            return View(new FuncionarioViewModel
            {
                Funcionario = listFunc,
                PageInfo = new PaginationViewModel
                {
                    CurrentPage = page,
                    ItensPerPage = PAGE_SIZE,
                    TotalItems = total
                },
                CurrentNome = nome
            });
        }

        // GET: Funcionarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Funcionario));
            }

            var funcionario = await _context.Funcionario
                .FirstOrDefaultAsync(m => m.FuncionarioId == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // GET: Funcionarios/Create
        public IActionResult Adicionar_Funcionario()
        {
            ViewData["Cargo"] = new SelectList(_context.Cargo, "CargoId", "Nome");
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adicionar_Funcionario([Bind("FuncionarioId,Nome,CargoId,Nascimento,NascimentoFilho,NIF,Telefone,Email,Notas")] Funcionario funcionario)
        {
            System.Diagnostics.Debug.WriteLine(funcionario.CargoId);
            if (!funcionario.NascimentoFilho.Equals(DateTime.MinValue) && funcionario.NascimentoFilho > DateTime.Now)
            {
                ModelState.AddModelError("NascimentoFilho", "A data de nascimento não pode de ser posterior á atual.");

            }
            if (funcionario.Nascimento > DateTime.Now)
            {
                ModelState.AddModelError("Nascimento", "A data de nascimento não pode ser posterior á atual.");

            }
         
            if (funcionario.Nascimento > DateTime.Now.AddYears(-18))
            {
                ModelState.AddModelError("Nascimento", "Funcionário com menos de 18 anos.");

            }
            if (ModelState.IsValid)
            {
                _context.Add(funcionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Funcionario));
            }
            ViewData["Cargo"] = new SelectList(_context.Cargo, "CargoId", "Nome", funcionario.CargoId);
            return View(funcionario);
        }

        // GET: Funcionarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }
            ViewData["Cargo"] = new SelectList(_context.Cargo, "CargoId", "Nome");
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FuncionarioId,Nome,CargoId,Nascimento,NascimentoFilho,NIF,Telefone,Email,Notas")] Funcionario funcionario)
        {
            if (id != funcionario.FuncionarioId)
            {
                return NotFound();
            }
            if (funcionario.NascimentoFilho > DateTime.Now)
            {
                ModelState.AddModelError("NascimentoFilho", "A data de nascimento não pode de ser posterior á atual.");

            }
            if (funcionario.Nascimento > DateTime.Now)
            {
                ModelState.AddModelError("Nascimento", "A data de nascimento não pode ser posterior á atual.");

            }
            if (funcionario.Nascimento > DateTime.Now.AddYears(-18))
            {
                ModelState.AddModelError("Nascimento", "Funcionário com menos de 18 anos.");

            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionarioExists(funcionario.FuncionarioId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Funcionario));
            }
            ViewData["Cargo"] = new SelectList(_context.Cargo, "CargoId", "Nome", funcionario.CargoId);
            return View(funcionario);
        }

        // GET: Funcionarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario
                .FirstOrDefaultAsync(m => m.FuncionarioId == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }
        
        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionario = await _context.Funcionario.FindAsync(id);
            _context.Funcionario.Remove(funcionario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Funcionario));
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionario.Any(e => e.FuncionarioId == id);
        }
    }
}
