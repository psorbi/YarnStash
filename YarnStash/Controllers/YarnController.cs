using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YarnStash.Data;
using YarnStash.Interfaces;
using YarnStash.Models;
using YarnStash.Services;

namespace YarnStash.Controllers
{
    public class YarnController : Controller
    {
        private readonly YarnContext _context;
        private readonly ISearchServices _searchServices;

        public YarnController(YarnContext context, ISearchServices searchServices)
        {
            _context = context;
            _searchServices = searchServices;
        }

        // GET: Yarn
        public IActionResult Index()
        {
            var yarns = from y in _context.Yarn
                        select y;

            //default sort is manufacterer ascending
            var defaultSort = "";
            yarns = _searchServices.SortYarn(yarns, defaultSort);

            ViewData["SearchResults"] = "_YarnDisplayTable";

            return View( yarns.AsNoTracking().ToList());
        }

        public async Task<IActionResult> Search(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            
            var yarns = from y in _context.Yarn
                        select y;

            //default sort is manufacterer ascending
            var defaultSort = "";
            yarns = _searchServices.SortYarn(yarns, defaultSort);

            //search from box input
            if (searchString != null)
            {
                searchString = searchString.Trim();
                yarns = _searchServices.SearchByInput(yarns, searchString);

                if (yarns.Count() == 0)
                {
                    ViewData["SearchResults"] = "_NotFoundView";
                    return View("Index");
                }
            }
            else if (searchString == null)
            {
                ViewData["SearchResults"] = "_YarnDisplayTable";

                return View("Index", await yarns.AsNoTracking().ToListAsync());
            }

            ViewData["SearchResults"] = "_YarnDisplayTable";

            return View("Index", await yarns.AsNoTracking().ToListAsync());
        }

        // GET: Yarn/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
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
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] YarnModel yarnModel)
        {
            if (ValidateYarnModel(yarnModel))
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
                return BadRequest();
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
        public async Task<IActionResult> Edit(int id, [FromForm] YarnModel yarnModel)
        {
            if (id != yarnModel.id)
            {
                return NotFound();
            }

            if (ValidateYarnModel(yarnModel))
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
                return BadRequest();
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

        private bool ValidateYarnModel(YarnModel yarnModel)
        {
            bool isValid = true;

            if (yarnModel == null)
            {
                isValid = false;
            }
            else if (string.IsNullOrEmpty(yarnModel.Name))
            {
                isValid = false;
            }
            else if (yarnModel.Amount < 0)
            {
                isValid = false;
            }
            else if (string.IsNullOrEmpty(yarnModel.Color))
            {
                isValid = false;
            }

            return isValid;
        }

    }
}
