using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAZ_SI_2025.Models.Domain.Entities;
using UAZ_SI_2025.Models.ViewModels.Menu;

namespace UAZ_SI_2025.Models.ViewModels.MenuCategory
{
    public class MenuCategoryViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Icon { get; set; }

        [Display(Name = "Menu")]
        public Guid MenuId { get; set; }
        public MenuViewModel Menu { get; set; } = null!;
    }
}
