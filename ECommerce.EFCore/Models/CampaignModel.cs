using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ECommerce.EFCore.Models
{
    public class CampaignModel
    {
        [Key]
        public int CampaignId { get; set; }
        public string Tittle { get; set; }
        public float Discount { get; set; }
        public int DiscountType { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
    }
}
