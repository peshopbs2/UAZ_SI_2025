using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UAZ_SI_2025.Business.Services.Interfaces;
using UAZ_SI_2025.Data;
using UAZ_SI_2025.Models.Domain.Entities;
using UAZ_SI_2025.Models.ViewModels.MenuCategory;

namespace UAZ_SI_2025.Controllers
{
    public class MenuCategoriesController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly IMenuCategoryService _menuCategoryService;

        public MenuCategoriesController(IMenuService menuService, IMenuCategoryService menuCategoryService)
        {
            _menuService = menuService;
            _menuCategoryService = menuCategoryService;
        }

        // GET: MenuCategories
        public async Task<IActionResult> Index()
        {
            var items = await _menuCategoryService.GetAllAsync();
            return View(items);
        }

        [HttpGet("MenuCategories/Menu/{menuId:guid}")]
        public async Task<IActionResult> Menu(Guid menuId)
        {
            var items = await _menuCategoryService.GetByMenuIdAsync(menuId);
            return View(items);
        }

        // GET: MenuCategories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuCategory = await _menuCategoryService.GetByIdAsync(id.Value);
            if (menuCategory == null)
            {
                return NotFound();
            }

            return View(menuCategory);
        }

        // GET: MenuCategories/Create
        public async Task<IActionResult> Create()
        {
            var menus = await _menuService.GetAllAsync();
            ViewData["MenuId"] = new SelectList(menus, "Id", "Title");
            return View();
        }

        // POST: MenuCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuCategoryCreateViewModel menuCategory)
        {
            if (ModelState.IsValid)
            {
                await _menuCategoryService.CreateAsync(menuCategory);
                return RedirectToAction(nameof(Index));
            }

            var menus = await _menuService.GetAllAsync();
            ViewData["MenuId"] = new SelectList(menus, "Id", "Title", menuCategory.MenuId);
            return View(menuCategory);
        }

        // GET: MenuCategories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuCategory = await _menuCategoryService.GetByIdAsync(id.Value);
            if (menuCategory == null)
            {
                return NotFound();
            }
            var menus = await _menuService.GetAllAsync();
            ViewData["MenuId"] = new SelectList(menus, "Id", "Title", menuCategory.MenuId);
            return View(menuCategory);
        }

        // POST: MenuCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, MenuCategoryEditViewModel menuCategory)
        {
            if (id != menuCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _menuCategoryService.UpdateAsync(menuCategory);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await MenuCategoryExistsAsync(menuCategory.Id))
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
            var items = await _menuService.GetAllAsync();
            ViewData["MenuId"] = new SelectList(items, "Id", "Title", menuCategory.MenuId);
            return View(menuCategory);
        }

        // GET: MenuCategories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuCategory = await _menuCategoryService.GetByIdAsync(id.Value);
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
            var menuCategory = await _menuCategoryService.GetByIdAsync(id);
            if (menuCategory != null)
            {
                await _menuCategoryService.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MenuCategoryExistsAsync(Guid id)
        {
            var menuCategory = await _menuCategoryService.GetByIdAsync(id);
            return menuCategory != null;
        }
    }
}
