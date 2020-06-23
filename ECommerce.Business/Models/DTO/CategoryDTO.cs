namespace ECommerce.Business.Models.DTO
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string Tittle { get; set; }
        public int ParentCategoryId { get; set; }
    }
}
