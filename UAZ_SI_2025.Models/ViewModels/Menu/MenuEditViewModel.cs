using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UAZ_SI_2025.Models.ViewModels.Menu
{
    public class MenuEditViewModel : MenuCreateViewModel
    {
        [Required]
        public Guid Id { get; set; }
    }
}
