using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.EFCore.Models
{
    public class CartDetailModel
    {
        [Key]
        public int CartDetailId { get; set; }
        public int ProductQuantity { get; set; }
        public int ProductId { get; set; }
        public int CartId { get; set; }
    }
}
