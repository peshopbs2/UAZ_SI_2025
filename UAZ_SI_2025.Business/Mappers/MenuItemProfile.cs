using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAZ_SI_2025.Models.Domain.Entities;
using UAZ_SI_2025.Models.ViewModels.MenuCategory;
using UAZ_SI_2025.Models.ViewModels.MenuItem;

namespace UAZ_SI_2025.Business.Mappers
{
    public class MenuItemProfile : Profile
    {
        public MenuItemProfile()
        {
            CreateMap<MenuItem, MenuItemViewModel>();
            CreateMap<MenuItemCreateViewModel, MenuItem>()
                .ForMember(m => m.Id, dest => dest.MapFrom(_ => Guid.NewGuid()));
            CreateMap<MenuItemEditViewModel, MenuItem>();
        }
    }
}
