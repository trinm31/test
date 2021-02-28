using System.ComponentModel.DataAnnotations;

namespace AtnWeb.Models
{
    public class Shop
    {
        [Key] 
        public int Id { get; set; }
        [Required] 
        public string Location { get; set; }
    }
}