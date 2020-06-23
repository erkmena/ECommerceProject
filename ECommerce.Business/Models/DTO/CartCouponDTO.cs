namespace ECommerce.Business.Models.DTO
{
    public class CartCouponDTO
    {
        public int CartCouponId { get; set; }
        public int CartId { get; set; }
        public int CouponId { get; set; }
        public double DiscountAmount { get; set; }
    }
}
