using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ECommerce.EFCore.Models
{
    public class CartCouponModel
    {
        [Key]
        public int CartCouponId { get; set; }
        public double DiscountAmount { get; set; }
        public int CartId { get; set; }
        public int CouponId { get; set; }
    }
}
