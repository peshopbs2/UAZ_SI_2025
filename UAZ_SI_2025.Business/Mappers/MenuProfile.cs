using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAZ_SI_2025.Models.Domain.Entities;
using UAZ_SI_2025.Models.ViewModels.Menu;

namespace UAZ_SI_2025.Business.Mappers
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<Menu, MenuViewModel>();
            CreateMap<MenuCreateViewModel, Menu>()
                .ForMember(m => m.Id, dest => dest.MapFrom(_ => Guid.NewGuid()));
            CreateMap<MenuEditViewModel, Menu>();
        }
    }
}
