using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.EFCore.Models
{
    public class DeliveryModel
    {
        [Key]
        public int DeliveryId { get; set; }
        public double Cost { get; set; }
        public int CartId { get; set; }
    }
}
