using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts;
using Microsoft.EntityFrameworkCore;
using YarnStash.Interfaces;
using YarnStash.Data;
using YarnStash.Models;

namespace YarnStash.Controllers
{
    public class PatternController : Controller
    {
        private readonly YarnContext _context;
        private readonly ISearchServices _searchServices;

        public PatternController(YarnContext context, ISearchServices searchServices)
        {
            _context = context;
            _searchServices = searchServices;
        }
        // GET: PatternController
        public IActionResult Index()
        {
            var patterns = from p in _context.Pattern
                           select p;

            //default sort is pattern name ascending
            var defaultSort = "";
            patterns = _searchServices.SortPattern(patterns, defaultSort);

            ViewData["SearchResults"] = "_PatternDisplayTable";

            return View( patterns.AsNoTracking().ToList());
        }

        public async Task<IActionResult> Search(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var patterns = from p in _context.Pattern
                           select p;

            //default sort is pattern name ascending
            var defaultSort = "";
            patterns = _searchServices.SortPattern(patterns, defaultSort);

            //search from box input
            if (searchString != null)
            {
                searchString = searchString.Trim();
                patterns = _searchServices.SearchByInput(patterns, searchString);

                if (patterns.Count() == 0)
                {
                    ViewData["SearchResults"] = "_NotFoundView";
                    return View("Index");
                }
            }
            else if (searchString == null)
            {
                ViewData["SearchResults"] = "_PatternDisplayTable";

                return View("Index", await patterns.AsNoTracking().ToListAsync());
            }

            ViewData["SearchResults"] = "_PatternDisplayTable";

            return View("Index", await patterns.AsNoTracking().ToListAsync());
        }

        // GET: PatternController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var patternModel = await _context.Pattern
                .FirstOrDefaultAsync(m => m.id == id);
            if (patternModel == null)
            {
                return NotFound();
            }

            return View(patternModel);
        }

        // GET: PatternController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PatternController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] PatternModel patternModel)
        {
            if (ValidatePatternModel(patternModel))
            {
                _context.Add(patternModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(patternModel);
        }

        private bool ValidatePatternModel(PatternModel patternModel)
        {
            bool isValid = true;

            if (patternModel == null)
            {
                isValid = false;
            }
            else if (string.IsNullOrEmpty(patternModel.Name))
            {
                isValid = false;
            }
            else if (patternModel.Amount < 0)
            {
                isValid = false;
            }

            return isValid;
        }

        // GET: PatternController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var patternModel = await _context.Pattern.FindAsync(id);
            if (patternModel == null)
            {
                return NotFound();
            }

            return View(patternModel);
        }

        // POST: PatternController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] PatternModel patternModel)
        {
            if (id != patternModel.id)
            {
                return NotFound();
            }

            if (ValidatePatternModel(patternModel))
            {
                try
                {
                    _context.Update(patternModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatternModelExists(patternModel.id))
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
            return View(patternModel);
        }

        private bool PatternModelExists(int id)
        {
            return _context.Pattern.Any(e => e.id == id);
        }

        // GET: PatternController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var patternModel = await _context.Pattern
                .FirstOrDefaultAsync(p => p.id == id);
            if (patternModel == null)
            {
                return NotFound();
            }
            return View(patternModel);
        }

        // POST: PatternController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patternModel = await _context.Pattern.FindAsync(id);
            _context.Pattern.Remove(patternModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
