namespace ECommerce.Business.Models.DTO
{
    public class CampaignDTO
    {
        public int CampaignId { get; set; }
        public string Tittle { get; set; }
        public float Discount { get; set; }
        public int CategoryId { get; set; }
        public int Quantity { get; set; }
        public DiscountType DiscountType { get; set; }

    }
}
