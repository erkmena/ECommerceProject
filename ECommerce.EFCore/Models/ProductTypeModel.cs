using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerce.EFCore.Models
{
    public class ProductTypeModel
    {
        [Key]
        public int ProductTypeId { get; set; }
        public string Tittle { get; set; }
    }
}
