using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UAZ_SI_2025.Models.ViewModels.Menu
{
    public class MenuCreateViewModel
    {
        [MinLength(5)]
        [MaxLength(100)]
        [Required(ErrorMessage = "Menu title is required")]
        public string Title { get; set; } = string.Empty;
    }
}
