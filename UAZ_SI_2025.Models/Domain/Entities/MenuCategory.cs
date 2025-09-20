using System.ComponentModel.DataAnnotations;

namespace UAZ_SI_2025.Models.Domain.Entities
{
    public class MenuCategory : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Icon { get; set; }

        [Display(Name="Menu")]
        public Guid MenuId { get; set; }
        public Menu Menu { get; set; } = null!;
        public List<MenuItem>? MenuItems { get; set; } = null!;
    }
}
