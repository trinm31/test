using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtnWeb.Models
{
    public class Toy
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Desciption { get; set; }
        [Required]
        public double Price { get; set; }
        [Required] 
        public int Amount { get; set; }
        [Required] 
        public int ShopId { get; set; }
        [ForeignKey("ShopId")]
        public Shop Shop { get; set; }
    }
}