using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AtnWeb.Models
{
    public class ManageIndexViewModel
    {
        public IEnumerable<SelectListItem> Shops { get; set; }
    }
}