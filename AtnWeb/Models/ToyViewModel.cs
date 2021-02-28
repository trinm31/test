using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AtnWeb.Models
{
    public class ToyViewModel
    {
        public IEnumerable<SelectListItem> Shops { get; set; }
        public Toy Toy { get; set; }
    }
}