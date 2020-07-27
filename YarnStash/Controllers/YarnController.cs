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
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["ManufacturerSortParm"] = String.IsNullOrEmpty(sortOrder) ? "manufacturer_desc" : "";
            ViewData["NameSortParm"] = sortOrder == "Name" ? "name_desc" : "Name";
            ViewData["AmountSortParm"] = sortOrder == "Amount" ? "amount_desc" : "Amount";
            ViewData["SizeSortParm"] = sortOrder == "Size" ? "size_desc" : "Size";
            ViewData["CurrentFilter"] = searchString;

            var yarns = from y in _context.Yarn
                        select y;

            //search from box input
            if (!String.IsNullOrEmpty(searchString))
            {
                yarns = yarns.Where(y => y.Manufacturer.ToLower().Contains(searchString.ToLower())
                    || y.Name.ToLower().Contains(searchString.ToLower()));
            }

            //sort table by column, default is manufacterer ascending
            switch (sortOrder)
            {
                case "manufacturer_desc":
                    yarns = yarns.OrderByDescending(y => y.Manufacturer.ToLower());
                    break;
                case "Name":
                    yarns = yarns.OrderBy(y => y.Name.ToLower());
                    break;
                case "name_desc":
                    yarns = yarns.OrderByDescending(y => y.Name.ToLower());
                    break;
                case "Amount":
                    yarns = yarns.OrderBy(y => y.Amount);
                    break;
                case "amount_desc":
                    yarns = yarns.OrderByDescending(y => y.Amount);
                    break;
                case "Size":
                    yarns = yarns.OrderBy(y => y.Size);
                    break;
                case "size_desc":
                    yarns = yarns.OrderByDescending(y => y.Size);
                    break;
                default:
                    yarns = yarns.OrderBy(y => y.Manufacturer.ToLower());
                    break;

            }

            return View(await yarns.AsNoTracking().ToListAsync());
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
        public async Task<IActionResult> Create([Bind("id,Manufacturer,Name,Amount,Color,Size")] YarnModel yarnModel)
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
        public async Task<IActionResult> Edit(int id, [Bind("id,Manufacturer,Name,Amount,Color,Size")] YarnModel yarnModel)
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
