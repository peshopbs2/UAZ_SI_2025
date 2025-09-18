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
    public class MenuItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MenuItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MenuItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MenuItems.Include(m => m.MenuCategory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MenuItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems
                .Include(m => m.MenuCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuItem == null)
            {
                return NotFound();
            }

            return View(menuItem);
        }

        // GET: MenuItems/Create
        public IActionResult Create()
        {
            ViewData["MenuCategoryId"] = new SelectList(_context.MenuCategories, "Id", "Title");
            return View();
        }

        // POST: MenuItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Price,ImageUrl,MenuCategoryId,Id")] MenuItem menuItem)
        {
            if (ModelState.IsValid)
            {
                menuItem.Id = Guid.NewGuid();
                _context.Add(menuItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuCategoryId"] = new SelectList(_context.MenuCategories, "Id", "Title", menuItem.MenuCategoryId);
            return View(menuItem);
        }

        // GET: MenuItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            ViewData["MenuCategoryId"] = new SelectList(_context.MenuCategories, "Id", "Title", menuItem.MenuCategoryId);
            return View(menuItem);
        }

        // POST: MenuItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,Description,Price,ImageUrl,MenuCategoryId,Id")] MenuItem menuItem)
        {
            if (id != menuItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuItemExists(menuItem.Id))
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
            ViewData["MenuCategoryId"] = new SelectList(_context.MenuCategories, "Id", "Title", menuItem.MenuCategoryId);
            return View(menuItem);
        }

        // GET: MenuItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems
                .Include(m => m.MenuCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuItem == null)
            {
                return NotFound();
            }

            return View(menuItem);
        }

        // POST: MenuItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem != null)
            {
                _context.MenuItems.Remove(menuItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuItemExists(Guid id)
        {
            return _context.MenuItems.Any(e => e.Id == id);
        }
    }
}
