using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAZ_SI_2025.Models.Domain.Entities;

namespace UAZ_SI_2025.Models.ViewModels.Menu
{
    public class MenuViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}
