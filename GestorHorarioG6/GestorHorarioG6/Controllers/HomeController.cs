﻿using System;
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
    public class HomeController : Controller
    {
        private const int PAGE_SIZE = 5;
        private readonly GestorHorarioG6Context _context;

        public HomeController(GestorHorarioG6Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }



        // GET: Funcionarios
        public async Task<IActionResult> Escalas(FuncionarioViewModel  model= null, int page = 1)
        {
              string nome = null;

            if (model != null)
            {
                nome = model.CurrentNome;
                page = 1;
            }

         /*   
                 funcionario = _context.Funcionario.Where(n => nome == null || n.Nome.Contains(nome));

                int numFuncionario = await funcionario.CountAsync();

            if (page > (numFuncionario / PAGE_SIZE) + 1)
            {
                page = 1;
            }*/


            var funcionario = _context.Funcionario.Include(f => f.Cargo);
            var search = _context.Funcionario.Where(n => nome == null || n.Nome.Contains(nome));
            var total = await funcionario.CountAsync();
            var listFunc = await funcionario.
               OrderBy(p => p.FuncionarioId)
               .Skip(PAGE_SIZE *(page - 1) )
               .Take(PAGE_SIZE)
               .ToListAsync();

            return View(new FuncionarioViewModel
            {
                Funcionario = listFunc,
                PageInfo = new PaginationViewModel
                {
                    CurrentPage = page,
                    ItemsPerPage = PAGE_SIZE,
                    TotalItems = total
                },
                CurrentNome = nome
            }

           );
        }

        // GET: Funcionarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Escalas));
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
        public async Task<IActionResult> Adicionar_Funcionario([Bind("FuncionarioID,Nome,CargoId,Nascimento,NascimentoFilho,NIF,Telefone,Email,Notas")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Escalas));
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
        public async Task<IActionResult> Edit(int id, [Bind("FuncionarioID,Nome,CargoId,Nascimento,NascimentoFilho,NIF,Telefone,Email,Notas")] Funcionario funcionario)
        {
            if (id != funcionario.FuncionarioId)
            {
                return NotFound();
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
                return RedirectToAction(nameof(Escalas));
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
            return RedirectToAction(nameof(Escalas));
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionario.Any(e => e.FuncionarioId == id);
        }
    }
}
