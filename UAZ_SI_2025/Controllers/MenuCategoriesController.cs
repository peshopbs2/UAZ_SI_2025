using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UAZ_SI_2025.Data;
using UAZ_SI_2025.Models.Domain.Entities;

namespace UAZ_SI_2025.Controllers
{
    public class MenuCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MenuCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MenuCategories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MenuCategories.Include(m => m.Menu);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MenuCategories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuCategory = await _context.MenuCategories
                .Include(m => m.Menu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuCategory == null)
            {
                return NotFound();
            }

            return View(menuCategory);
        }

        // GET: MenuCategories/Create
        public IActionResult Create()
        {
            ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Title");
            return View();
        }

        // POST: MenuCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Icon,MenuId,Id")] MenuCategory menuCategory)
        {
            if (ModelState.IsValid)
            {
                menuCategory.Id = Guid.NewGuid();
                _context.Add(menuCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Title", menuCategory.MenuId);
            return View(menuCategory);
        }

        // GET: MenuCategories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuCategory = await _context.MenuCategories.FindAsync(id);
            if (menuCategory == null)
            {
                return NotFound();
            }
            ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Title", menuCategory.MenuId);
            return View(menuCategory);
        }

        // POST: MenuCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,Description,Icon,MenuId,Id")] MenuCategory menuCategory)
        {
            if (id != menuCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuCategoryExists(menuCategory.Id))
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
            ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Title", menuCategory.MenuId);
            return View(menuCategory);
        }

        // GET: MenuCategories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuCategory = await _context.MenuCategories
                .Include(m => m.Menu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuCategory == null)
            {
                return NotFound();
            }

            return View(menuCategory);
        }

        // POST: MenuCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var menuCategory = await _context.MenuCategories.FindAsync(id);
            if (menuCategory != null)
            {
                _context.MenuCategories.Remove(menuCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuCategoryExists(Guid id)
        {
            return _context.MenuCategories.Any(e => e.Id == id);
        }
    }
}
