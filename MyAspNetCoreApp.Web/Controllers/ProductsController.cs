using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyAspNetCoreApp.Web.Models;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private AppDbContext _context;
        private readonly ProductRepository _productRepository;

        public ProductsController(AppDbContext context) //Constructor 
        {
            _context = context;

            //if (!_context.Products.Any())
            //{
            //    _context.Products.Add(new() {Name = "Kalem1", Price = 15, Stok = 200, Color="Red" });
            //    _context.Products.Add(new() {  Name = "Kalem2", Price = 25, Stok = 400, Color = "Red" });
            //    _context.Products.Add(new() {  Name = "Kalem3", Price = 35, Stok = 600, Color = "Red" });
            
            //    _context.SaveChanges(); //EF Core'un Ram'de tututtuğu dataları DB'ye kaydediyoruz.
            //}

            //DI Container
            //Dependency Injection pattern
            _productRepository = new ProductRepository();
            
        }


        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        public IActionResult Remove(int id)

        {
            var product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            TempData["status"] = "Ürün başarıyla silindi.";//Remove actiondan Index actiona veri taşıdığımız için TemData'yı kullanıyoruz.
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Add()
        {

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new() {Data="Mavi",Value="Mavi" },
                new() {Data="Sarı",Value="Sarı" },
                new() {Data="Kırmızı",Value="Kırmızı" },
                new() {Data="Turuncu",Value="Turuncu" },
                new() {Data="Yeşil",Value="Yeşil" }


            },"Value", "Data");

            return View();
        }

        [HttpPost]
        public IActionResult Add(Product newProduct)
        {
            _context.Products.Add(newProduct);
            _context.SaveChanges();
            TempData["status"] = "Ürün başarıyla eklendi.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _context.Products.Find(id);
            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new() {Data="Mavi",Value="Mavi" },
                new() {Data="Sarı",Value="Sarı" },
                new() {Data="Kırmızı",Value="Kırmızı" },
                new() {Data="Turuncu",Value="Turuncu" },
                new() {Data="Yeşil",Value="Yeşil" }


            }, "Value", "Data",product.Color);

            return View(product);
        }

        [HttpPost]

        public IActionResult Update(Product newProduct)
        {
            _context.Products.Update(newProduct);
            _context.SaveChanges();

            TempData["status"] = "Ürün başarıyla güncellendi.";

            return RedirectToAction("Index");
        }





    }
}
