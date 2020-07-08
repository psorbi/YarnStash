using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YarnStash.Data;
using YarnStash.Models;

namespace YarnStash.Controllers
{
    public class YarnController : Controller
    {
        private readonly YarnContext _context;

        public YarnController(YarnContext context)
        {
            _context = context;
        }

        // GET: Yarn
        public async Task<IActionResult> Index()
        {
            return View(await _context.Yarn.ToListAsync());
        }

        // GET: Yarn/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yarnModel = await _context.Yarn
                .FirstOrDefaultAsync(m => m.id == id);
            if (yarnModel == null)
            {
                return NotFound();
            }

            return View(yarnModel);
        }

        // GET: Yarn/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Yarn/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,yarnManufacturer,yarnName,yarnAmount,yarnColor,yarnSize")] YarnModel yarnModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(yarnModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(yarnModel);
        }

        // GET: Yarn/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yarnModel = await _context.Yarn.FindAsync(id);
            if (yarnModel == null)
            {
                return NotFound();
            }
            return View(yarnModel);
        }

        // POST: Yarn/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,yarnManufacturer,yarnName,yarnAmount,yarnColor,yarnSize")] YarnModel yarnModel)
        {
            if (id != yarnModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yarnModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YarnModelExists(yarnModel.id))
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
            return View(yarnModel);
        }

        // GET: Yarn/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yarnModel = await _context.Yarn
                .FirstOrDefaultAsync(m => m.id == id);
            if (yarnModel == null)
            {
                return NotFound();
            }

            return View(yarnModel);
        }

        // POST: Yarn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var yarnModel = await _context.Yarn.FindAsync(id);
            _context.Yarn.Remove(yarnModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YarnModelExists(int id)
        {
            return _context.Yarn.Any(e => e.id == id);
        }
    }
}