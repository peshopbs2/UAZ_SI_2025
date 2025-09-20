using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAZ_SI_2025.Models.ViewModels.MenuCategory;

namespace UAZ_SI_2025.Business.Services.Interfaces
{
    public interface IMenuCategoryService
    {
        Task<IEnumerable<MenuCategoryViewModel>> GetAllAsync();
        Task<MenuCategoryViewModel> GetByIdAsync(Guid id);
        Task<IEnumerable<MenuCategoryViewModel>> GetByMenuIdAsync(Guid menuId);
        Task<MenuCategoryViewModel> CreateAsync(MenuCategoryCreateViewModel model);
        Task<MenuCategoryViewModel> UpdateAsync(MenuCategoryEditViewModel model);
        Task<bool> DeleteAsync(Guid id);
    }
}
