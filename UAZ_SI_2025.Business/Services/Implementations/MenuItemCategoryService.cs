using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAZ_SI_2025.Business.Repository;
using UAZ_SI_2025.Business.Services.Interfaces;
using UAZ_SI_2025.Models.Domain.Entities;
using UAZ_SI_2025.Models.ViewModels.MenuCategory;
using UAZ_SI_2025.Models.ViewModels.MenuItem;

namespace UAZ_SI_2025.Business.Services.Implementations
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IRepository<MenuItem> _menuItemRepository;
        private readonly IMapper _mapper;
        public MenuItemService(IRepository<MenuItem> MenuItemRepository, IMapper mapper)
        {
            _menuItemRepository = MenuItemRepository;
            _mapper = mapper;
        }

        public async Task<MenuItemViewModel> CreateAsync(MenuItemCreateViewModel model)
        {
            var entity = _mapper.Map<MenuItem>(model);
            await _menuItemRepository.AddAsync(entity);
            await _menuItemRepository.CommitAsync();
            return _mapper.Map<MenuItemViewModel>(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _menuItemRepository.GetByIdAsync(id);
            if (entity == null)
            {
                //TODO: throw an exception
                return false;
            }

            _menuItemRepository.Remove(entity);
            await _menuItemRepository.CommitAsync();
            return true;
        }

        public async Task<IEnumerable<MenuItemViewModel>> GetAllAsync()
        {
            var items = await _menuItemRepository.GetAllAsync(mi => mi.MenuCategory);
            return _mapper.Map<IEnumerable<MenuItemViewModel>>(items);
        }

        public async Task<MenuItemViewModel> GetByIdAsync(Guid id)
        {
            var item = await _menuItemRepository.GetByIdAsync(id, mi => mi.MenuCategory);
            return _mapper.Map<MenuItemViewModel>(item);
        }

        public async Task<IEnumerable<MenuItemViewModel>> GetByMenuCategoryIdAsync(Guid menuCategoryId)
        {
            var items = await _menuItemRepository.FindAsync(mc => mc.MenuCategoryId == menuCategoryId, mi => mi.MenuCategory);
            return _mapper.Map<IEnumerable<MenuItemViewModel>>(items);
        }

        public async Task<MenuItemViewModel> UpdateAsync(MenuItemEditViewModel model)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(model.Id) ?? throw new InvalidOperationException($"Menu item {model.Id} does not exist.");
            _mapper.Map(model, menuItem);
            _menuItemRepository.Update(menuItem);
            await _menuItemRepository.CommitAsync();
            return _mapper.Map<MenuItemViewModel>(menuItem);
        }
    }
}
