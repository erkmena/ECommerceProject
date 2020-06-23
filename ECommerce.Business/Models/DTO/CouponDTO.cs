namespace ECommerce.Business.Models.DTO
{
    public class CouponDTO
    {
        public int CouponId { get; set; }
        public float Discount { get; set; }
        public double MinimumCartCost { get; set; }
        public DiscountType DiscountType { get; set; }
    }
}
