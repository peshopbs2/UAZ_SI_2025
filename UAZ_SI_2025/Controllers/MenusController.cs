using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UAZ_SI_2025.Business.Services.Interfaces;
using UAZ_SI_2025.Data;
using UAZ_SI_2025.Models.Domain.Entities;
using UAZ_SI_2025.Models.ViewModels.Menu;

namespace UAZ_SI_2025.Controllers
{
    public class MenusController : Controller
    {
        private readonly IMenuService _menuService;

        public MenusController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        // GET: Menus
        public async Task<IActionResult> Index()
        {
            return View(await _menuService.GetAllAsync());
        }

        // GET: Menus/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _menuService.GetByIdAsync(id.Value);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        [Authorize(Roles="Admin")]
        // GET: Menus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuCreateViewModel menu)
        {
            if (ModelState.IsValid)
            {
                await _menuService.CreateAsync(menu);
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }

        // GET: Menus/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _menuService.GetByIdAsync(id.Value);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, MenuEditViewModel menu)
        {
            if (id != menu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _menuService.UpdateAsync(menu);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await MenuExistsAsync(menu.Id))
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
            return View(menu);
        }

        // GET: Menus/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _menuService.GetByIdAsync(id.Value);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: Menus/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var menu = await _menuService.GetByIdAsync(id);
            if (menu != null)
            {
                await _menuService.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MenuExistsAsync(Guid id)
        {
            var item = await _menuService.GetByIdAsync(id);
            return item != null;
        }
    }
}
