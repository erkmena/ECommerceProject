using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.EFCore.Models
{
    public class ProductModel
    {
        [Key]
        public int ProductId { get; set; }
        public string Tittle { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
    }
}
