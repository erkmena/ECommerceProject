using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerce.EFCore.Models
{
    public class CouponModel
    {
        [Key]
        public int CouponId { get; set; }
        public float Discount { get; set; }
        public float DiscountRate { get; set; }
        public double MinimumCartCost { get; set; }
    }
}
