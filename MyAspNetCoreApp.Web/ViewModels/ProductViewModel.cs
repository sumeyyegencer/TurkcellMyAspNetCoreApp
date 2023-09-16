using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace MyAspNetCoreApp.Web.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Remote(action:"HasProductName", controller:"Products")]
        [StringLength(50, ErrorMessage ="İsim alanına en  fazla 50 karakter girilebilir.")]
        [Required(ErrorMessage ="İsim alanı boş olamaz.")]
        public string? Name { get; set; }

        [RegularExpression(@"[0-9]+(\.[0-9]{1,2})", ErrorMessage ="Fiyat alanında noktadan sonra en fazla 2 basamak olmalıdır. ")]
        [Required(ErrorMessage = "Fiyat alanı boş olamaz.")]
        [Range(1,1000, ErrorMessage = "1 ile 1000 arasında bir değer olmalıdır.")]
        public decimal? Price { get; set; }


        [Required(ErrorMessage = "Stok alanı boş olamaz.")]
        [Range(1,200,ErrorMessage = "1 ile 200 arasında bir değer olmalıdır.")]
        public int? Stok { get; set; }


        [Required(ErrorMessage = "Renk alanı boş olamaz.")]
        public string? Color { get; set; }


     
        public bool isPublish { get; set; }

        [StringLength(300, MinimumLength =50,ErrorMessage ="Açıklama alanı 50 ile 300 karakter arasında değer alabilir.")]
        [Required(ErrorMessage = "Açıklama alanı boş olamaz.")]
        public string Description { get; set; }


        public DateTime? puslishDate { get; set; }
        public IFormFile Image { get; set; }


        [Required(ErrorMessage = "Kategori alanı boş olamaz.")]
        public int CategoryId { get; set; }
    }
}
