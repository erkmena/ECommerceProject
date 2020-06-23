namespace ECommerce.Business.Models
{
    public class PrintModel
    {
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public double TotalDiscount { get; set; }
    }
}
