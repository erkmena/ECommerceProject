namespace ECommerce.Business.Models.DTO
{
    public class CartCampaignDTO
    {
        public int CartCampaignId { get; set; }
        public int CampaignId { get; set; }
        public int CartId { get; set; }
        public double DiscountAmount { get; set; }

    }
}
