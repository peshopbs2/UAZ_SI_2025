using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAZ_SI_2025.Business.Repository;
using UAZ_SI_2025.Business.Services.Interfaces;
using UAZ_SI_2025.Models.Domain.Entities;
using UAZ_SI_2025.Models.ViewModels.Menu;
using UAZ_SI_2025.Models.ViewModels.MenuCategory;

namespace UAZ_SI_2025.Business.Services.Implementations
{
    public class MenuCategoryService : IMenuCategoryService
    {
        private readonly IRepository<MenuCategory> _menuCategoryRepository;
        private readonly IMapper _mapper;
        public MenuCategoryService(IRepository<MenuCategory> menuCategoryRepository, IMapper mapper)
        {
            _menuCategoryRepository = menuCategoryRepository;
            _mapper = mapper;
        }
        
        public async Task<MenuCategoryViewModel> CreateAsync(MenuCategoryCreateViewModel model)
        {
            var entity = _mapper.Map<MenuCategory>(model);
            await _menuCategoryRepository.AddAsync(entity);
            await _menuCategoryRepository.CommitAsync();
            return _mapper.Map<MenuCategoryViewModel>(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _menuCategoryRepository.GetByIdAsync(id);
            if (entity == null)
            {
                //TODO: throw an exception
                return false;
            }

            _menuCategoryRepository.Remove(entity);
            await _menuCategoryRepository.CommitAsync();
            return true;
        }

        public async Task<IEnumerable<MenuCategoryViewModel>> GetAllAsync()
        {
            var categories = await _menuCategoryRepository.GetAllAsync(mc => mc.Menu);
            return _mapper.Map<IEnumerable<MenuCategoryViewModel>>(categories);
        }

        public async Task<MenuCategoryViewModel> GetByIdAsync(Guid id)
        {
            var category = await _menuCategoryRepository.GetByIdAsync(id, mc => mc.Menu);
            return _mapper.Map<MenuCategoryViewModel>(category);
        }

        public async Task<IEnumerable<MenuCategoryViewModel>> GetByMenuIdAsync(Guid menuId)
        {
            var categories = await _menuCategoryRepository.FindAsync(mc => mc.MenuId == menuId, mc => mc.Menu);
            return _mapper.Map<IEnumerable<MenuCategoryViewModel>>(categories);
        }

        public async Task<MenuCategoryViewModel> UpdateAsync(MenuCategoryEditViewModel model)
        {
            var menuCategory = await _menuCategoryRepository.GetByIdAsync(model.Id) ?? throw new InvalidOperationException($"Menu category {model.Id} does not exist.");
            _mapper.Map(model, menuCategory);
            _menuCategoryRepository.Update(menuCategory);
            await _menuCategoryRepository.CommitAsync();
            return _mapper.Map<MenuCategoryViewModel>(menuCategory);
        }
    }
}
