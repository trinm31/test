using System.Collections.Generic;

namespace AtnWeb.Models
{
    public class ManageViewModel
    {
        public IEnumerable<Shop> Shops { get; set; }
        public IEnumerable<Toy> Toys { get; set; }
    }
}