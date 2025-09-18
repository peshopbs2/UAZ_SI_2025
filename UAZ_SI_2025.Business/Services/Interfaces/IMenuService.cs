using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAZ_SI_2025.Models.ViewModels.Menu;

namespace UAZ_SI_2025.Business.Services.Interfaces
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuViewModel>> GetAllAsync();
        Task<MenuViewModel> GetByIdAsync(Guid id);
        Task<MenuViewModel> CreateAsync(MenuCreateViewModel model);
        Task<MenuViewModel> UpdateAsync(MenuEditViewModel model);
        Task<bool> DeleteAsync(Guid id);
    }
}
