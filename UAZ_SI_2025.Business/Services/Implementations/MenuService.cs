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

namespace UAZ_SI_2025.Business.Services.Implementations
{
    public class MenuService : IMenuService
    {
        private readonly IRepository<Menu> _menuRepository;
        private readonly IMapper _mapper;
        public MenuService(IRepository<Menu> menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<MenuViewModel> CreateAsync(MenuCreateViewModel model)
        {
            var entity = _mapper.Map<Menu>(model);
            await _menuRepository.AddAsync(entity);
            await _menuRepository.CommitAsync();
            return _mapper.Map<MenuViewModel>(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _menuRepository.GetByIdAsync(id);
            if(entity == null)
            {
                //TODO: throw an exception
                return false;
            }

            _menuRepository.Remove(entity);
            await _menuRepository.CommitAsync();
            return true;
        }

        public async Task<IEnumerable<MenuViewModel>> GetAllAsync()
        {
            var items = await _menuRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MenuViewModel>>(items);
        }

        public async Task<MenuViewModel> GetByIdAsync(Guid id)
        {
            var item = await _menuRepository.GetByIdAsync(id);
            return _mapper.Map<MenuViewModel>(item);
        }

        public async Task<MenuViewModel> UpdateAsync(MenuEditViewModel model)
        {
            var menu = await _menuRepository.GetByIdAsync(model.Id) ?? throw new InvalidOperationException($"Menu {model.Id} does not exist.");
            _mapper.Map(model, menu);
            _menuRepository.Update(menu);
            await _menuRepository.CommitAsync();
            return _mapper.Map<MenuViewModel>(menu);
        }
    }
}
