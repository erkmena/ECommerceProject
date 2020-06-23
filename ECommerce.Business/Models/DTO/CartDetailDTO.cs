namespace ECommerce.Business.Models.DTO
{
    public class CartDetailDTO
    {
        public int CartDetailId { get; set; }
        public int ProductQuantity { get; set; }
        public int ProductId { get; set; }
        public int CartId { get; set; }
    }
}
