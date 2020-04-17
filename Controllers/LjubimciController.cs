using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication12.Data;
using WebApplication12.Models;

namespace WebApplication12.Controllers
{
    public class LjubimciController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _environment;
        public LjubimciController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _environment = environment;
            _context = context;
        }

        // GET: Ljubimci
        public async Task<IActionResult> Index()
        {
            var list = new List<Ljubimac>();
            var listpos = new List<Posvajatelj>();
            var listlj = new List<Ljubimac>();
            foreach (var d in _context.Ljubimac)
            {
                list.Add(d);

            }
            foreach (var d in _context.Posvajatelj)
            {
                listpos.Add(d);

            }

            foreach (var b in list)
            {
                if (listpos.Count != 0)
                {
                    foreach (var c in listpos)
                    {
                        if (!listpos.Exists(x => x.LjubimacId == b.Id) && b.SkloništeId != null)
                        {
                            if (!listlj.Contains(b))
                                listlj.Add(b);
                        }



                    }
                }
                else
                {
                    if (b.SkloništeId != null)
                        if (!listlj.Contains(b))
                            listlj.Add(b);
                }
            }

            return View(listlj.ToList());
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Izgubljeni()
        {
            var applicationDbContext = _context.Ljubimac.Include(l => l.Sklonište).Where(l => l.SkloništeId == null);
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: Ljubimci/Details/5


        // GET: Ljubimci/Create

        public IActionResult Create()
        {
            var list = new List<String>();
            list.Add("Mačka");
            list.Add("Pas");
            ViewData["Vrsta"] = new SelectList(list);
            ViewData["SkloništeId"] = new SelectList(_context.Set<Sklonište>(), "Id", "Naziv");
            ViewData["PosvajateljId"] = new SelectList(_context.Set<Posvajatelj>(), "Id", "Prezime");
            return View();

        }

        // POST: Ljubimci/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ime,Opis,Datum,SkloništeId,Mjesto,Vrsta,Godine,Slika")] Ljubimac ljubimac, ICollection<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                var datum = DateTime.Now;

                ljubimac.Datum = datum;
                var uploads = Path.Combine(_environment.WebRootPath, "slike");
                if (ljubimac.Vrsta == "Mačka")
                {
                    uploads = Path.Combine(_environment.WebRootPath, "slike/macke");

                }
                else if (ljubimac.Vrsta == "Pas")
                {
                    uploads = Path.Combine(_environment.WebRootPath, "slike/psi");
                }
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                            if (ljubimac.Vrsta == "Mačka")
                            {
                                ljubimac.Slika = "//" + "macke" + "//" + file.FileName;

                            }
                            else if (ljubimac.Vrsta == "Pas")
                            {
                                ljubimac.Slika = "//" + "psi" + "//" + file.FileName;
                            }
                            else
                            {
                                ljubimac.Slika = file.FileName;
                            }

                        }
                    }
                }
                _context.Add(ljubimac);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var list = new List<String>();
            list.Add("Mačka");
            list.Add("Pas");
            ViewData["Vrsta"] = new SelectList(list);
            ViewData["SkloništeId"] = new SelectList(_context.Set<Sklonište>(), "Id", "Naziv", ljubimac.SkloništeId);

            return View(ljubimac);
        }

        // GET: Ljubimci/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ljubimac = await _context.Ljubimac.FindAsync(id);
            if (ljubimac == null)
            {
                return NotFound();
            }
            ViewData["SkloništeId"] = new SelectList(_context.Sklonište, "Id", "Naziv", ljubimac.SkloništeId);
            return View(ljubimac);
        }

        // POST: Ljubimci/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ime,Opis,Datum,SkloništeId,Mjesto,Vrsta,Godine,PosvajteljId,Slika")] Ljubimac ljubimac, ICollection<IFormFile> files)
        {
            if (id != ljubimac.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ljubimac.Datum = DateTime.Now;
                    var uploads = Path.Combine(_environment.WebRootPath, "slike");
                    if (ljubimac.Vrsta == "Mačka")
                    {
                        uploads = Path.Combine(_environment.WebRootPath, "slike/macke");

                    }
                    else if (ljubimac.Vrsta == "Pas")
                    {
                        uploads = Path.Combine(_environment.WebRootPath, "slike/psi");
                    }
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                if (ljubimac.Vrsta == "Mačka")
                                {
                                    ljubimac.Slika = "//" + "macke" + "//" + file.FileName;

                                }
                                else if (ljubimac.Vrsta == "Pas")
                                {
                                    ljubimac.Slika = "//" + "psi" + "//" + file.FileName;
                                }
                                else
                                {
                                    ljubimac.Slika = file.FileName;
                                }

                            }
                        }
                    }
                    _context.Update(ljubimac);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LjubimacExists(ljubimac.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Ljubimci");
            }
            var list = new List<String>();
            list.Add("Mačka");
            list.Add("Pas");
            ViewData["Vrsta"] = new SelectList(list, ljubimac.Vrsta);
            ViewData["SkloništeId"] = new SelectList(_context.Set<Sklonište>(), "Id", "Naziv", ljubimac.SkloništeId);

            return View(ljubimac);
        }

        // GET: Ljubimci/Delete/5

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ljubimac = await _context.Ljubimac
                .Include(l => l.Sklonište)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ljubimac == null)
            {
                return NotFound();
            }

            return View(ljubimac);
        }

        // POST: Ljubimci/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ljubimac = await _context.Ljubimac.FindAsync(id);
            _context.Ljubimac.Remove(ljubimac);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LjubimacExists(int id)
        {
            return _context.Ljubimac.Any(e => e.Id == id);
        }
    }
}
