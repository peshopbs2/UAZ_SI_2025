using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAZ_SI_2025.Models.ViewModels.MenuCategory;

namespace UAZ_SI_2025.Models.ViewModels.MenuItem
{
    public class MenuItemViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public Guid MenuCategoryId { get; set; }
        public MenuCategoryViewModel MenuCategory { get; set; } = null!;
    }
}
