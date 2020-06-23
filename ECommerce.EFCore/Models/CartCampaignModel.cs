using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.EFCore.Models
{
    public class CartCampaignModel
    {
        [Key]
        public int CartCampaignId { get; set; }
        public double DiscountAmount { get; set; }
        public int CampaignId { get; set; }
        public int CartId { get; set; }
    }
}
