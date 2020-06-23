using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ECommerce.EFCore.Models
{
    public class CampaignProductTypeModel
    {
        [Key]
        public int CampaignProductTypeId { get; set; }
        public int ProductTypeId { get; set; }
        public int CampaignId{ get; set; }

    }
}
