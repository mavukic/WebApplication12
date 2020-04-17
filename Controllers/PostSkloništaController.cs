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
    public class PostSkloništaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostSkloništaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PostSkloništa
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PostSkloništa.Include(p => p.Sklonište);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PostSkloništa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postSkloništa = await _context.PostSkloništa
                .Include(p => p.Sklonište)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postSkloništa == null)
            {
                return NotFound();
            }

            return View(postSkloništa);
        }

        // GET: PostSkloništa/Create
        public IActionResult Create()
        {
            ViewData["SkloništeId"] = new SelectList(_context.Set<Sklonište>(), "Id", "Id");
            return View();
        }

        // POST: PostSkloništa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,Content,Datum,SkloništeId")] PostSkloništa postSkloništa)
        {
            if (ModelState.IsValid)
            {
                
                postSkloništa.Datum = DateTime.Now;
                _context.Add(postSkloništa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SkloništeId"] = new SelectList(_context.Set<Sklonište>(), "Id", "Id", postSkloništa.SkloništeId);
            return View(postSkloništa);
        }

        // GET: PostSkloništa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postSkloništa = await _context.PostSkloništa.FindAsync(id);
            if (postSkloništa == null)
            {
                return NotFound();
            }
            ViewData["SkloništeId"] = new SelectList(_context.Set<Sklonište>(), "Id", "Id", postSkloništa.SkloništeId);
            return View(postSkloništa);
        }

        // POST: PostSkloništa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,Content,Datum,SkloništeId")] PostSkloništa postSkloništa)
        {
            if (id != postSkloništa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postSkloništa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostSkloništaExists(postSkloništa.Id))
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
            ViewData["SkloništeId"] = new SelectList(_context.Set<Sklonište>(), "Id", "Id", postSkloništa.SkloništeId);
            return View(postSkloništa);
        }

        // GET: PostSkloništa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postSkloništa = await _context.PostSkloništa
                .Include(p => p.Sklonište)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postSkloništa == null)
            {
                return NotFound();
            }

            return View(postSkloništa);
        }

        // POST: PostSkloništa/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postSkloništa = await _context.PostSkloništa.FindAsync(id);
            _context.PostSkloništa.Remove(postSkloništa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostSkloništaExists(int id)
        {
            return _context.PostSkloništa.Any(e => e.Id == id);
        }
    }
}
