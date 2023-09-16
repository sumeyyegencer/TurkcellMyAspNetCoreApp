namespace MyAspNetCoreApp.Web.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Stok { get; set; }

        public string?  Color { get; set; }
        public bool isPublish { get; set; }
        public string Description { get; set; }
        public DateTime? puslishDate { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
