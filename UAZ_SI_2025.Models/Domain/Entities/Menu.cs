using System.ComponentModel.DataAnnotations;

namespace UAZ_SI_2025.Models.Domain.Entities
{
    public class Menu : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public List<MenuCategory>? Categories { get; set; } = null!;
    }
}
