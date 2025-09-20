using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAZ_SI_2025.Models.Domain.Entities;
using UAZ_SI_2025.Models.ViewModels.Menu;
using UAZ_SI_2025.Models.ViewModels.MenuCategory;

namespace UAZ_SI_2025.Business.Mappers
{
    public class MenuCategoryProfile : Profile
    {
        public MenuCategoryProfile()
        {
            CreateMap<MenuCategory, MenuCategoryViewModel>();
            CreateMap<MenuCategoryCreateViewModel, MenuCategory>()
                .ForMember(m => m.Id, dest => dest.MapFrom(_ => Guid.NewGuid()));
            CreateMap<MenuCategoryEditViewModel, MenuCategory>();
        }
    }
}
