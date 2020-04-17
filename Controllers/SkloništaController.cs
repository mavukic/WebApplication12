using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication12.Data;
using WebApplication12.Models;

namespace WebApplication12.Controllers
{
    public class SkloništaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SkloništaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Skloništa
        public async Task<IActionResult> Index(int? id, int? postID)
        {
            var viewModel = new SkloništeView();
            viewModel.Skloništa = await _context.Sklonište
                  .Include(i => i.PostsSkloništa)

                  .AsNoTracking()
                  .OrderBy(i => i.Naziv)
                  .ToListAsync();

            if (id != null)
            {
                ViewData["SkloništeID"] = id.Value;
                Sklonište sklonište = viewModel.Skloništa.Where(
                    i => i.Id == id.Value).Single();
                viewModel.PostSkloništa = sklonište.PostsSkloništa;
            }

            if (postID != null)
            {
                ViewData["PostID"] = postID.Value;
                viewModel.PostSkloništa = viewModel.PostSkloništa.Where(
                    x => x.Id == postID);
            }


            return View(viewModel);
        }

        // GET: Skloništa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sklonište = await _context.Sklonište
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sklonište == null)
            {
                return NotFound();
            }

            return View(sklonište);
        }

        // GET: Skloništa/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Skloništa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,Adresa,Grad,Tel,Mail,Web")] Sklonište sklonište)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sklonište);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sklonište);
        }

        // GET: Skloništa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sklonište = await _context.Sklonište.FindAsync(id);
            if (sklonište == null)
            {
                return NotFound();
            }
            return View(sklonište);
        }

        // POST: Skloništa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,Adresa,Grad,Tel,Mail,Web")] Sklonište sklonište)
        {
            if (id != sklonište.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sklonište);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkloništeExists(sklonište.Id))
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
            return View(sklonište);
        }

        // GET: Skloništa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sklonište = await _context.Sklonište
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sklonište == null)
            {
                return NotFound();
            }

            return View(sklonište);
        }

        // POST: Skloništa/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sklonište = await _context.Sklonište.FindAsync(id);
            _context.Sklonište.Remove(sklonište);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkloništeExists(int id)
        {
            return _context.Sklonište.Any(e => e.Id == id);
        }
    }
}
