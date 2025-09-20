using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAZ_SI_2025.Models.ViewModels.MenuCategory;
using UAZ_SI_2025.Models.ViewModels.MenuItem;

namespace UAZ_SI_2025.Business.Services.Interfaces
{
    public interface IMenuItemService
    {
        Task<IEnumerable<MenuItemViewModel>> GetAllAsync();
        Task<MenuItemViewModel> GetByIdAsync(Guid id);
        Task<IEnumerable<MenuItemViewModel>> GetByMenuCategoryIdAsync(Guid menuCategoryId);
        Task<MenuItemViewModel> CreateAsync(MenuItemCreateViewModel model);
        Task<MenuItemViewModel> UpdateAsync(MenuItemEditViewModel model);
        Task<bool> DeleteAsync(Guid id);
    }
}
