using AspNetCore;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using MyAspNetCoreApp.Web.Models;
using MyAspNetCoreApp.Web.ViewModels;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ProductRepository _productRepository;
        private readonly IFileProvider _fileProvider;

        public ProductsController(AppDbContext context,IMapper mapper) //Constructor 
        {
            _context = context;
            _mapper = mapper;

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
            return View(_mapper.Map<List<ProductViewModel>>(products));
        }


        public IActionResult GetById(int productid)
        {
            var product = _context.Products.Find(productid);

            return View(_mapper.Map<ProductViewModel>(product));
        }

        public IActionResult Pages(int page,int pageSize)
        {
            //page=1 pageSize=3 
            //page=2 pageSize=3
            //page=3 pageSize=3

            var products = _context.Products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return View(_mapper.Map<List<ProductViewModel>>(products));
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

            var categories = _context.Category.ToList();
            ViewBag.categorySelect = new SelectList(categories, "Id", "Name");


            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel newProduct)
        {
            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new() {Data="Mavi",Value="Mavi" },
                new() {Data="Sarı",Value="Sarı" },
                new() {Data="Kırmızı",Value="Kırmızı" },
                new() {Data="Turuncu",Value="Turuncu" },
                new() {Data="Yeşil",Value="Yeşil" }


            }, "Value", "Data");

            var categories =_context.Category.ToList();
            ViewBag.categorySelect = new SelectList(categories, "Id", "Name");


            if (ModelState.IsValid)

                try
                {
                    //throw new Exception("db hatası");
                    _context.Products.Add(_mapper.Map<Product>(newProduct));
                    _context.SaveChanges();
                    TempData["status"] = "Ürün başarıyla eklendi.";
                    return RedirectToAction("Index");

                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty,"Ürün kaydedilirken bir hata meydana geldi.Lütfen daha sonra tekrar deneyiniz.");
                    return View();
                }
          


            else
            {
                return View();
            }
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

            return View(_mapper.Map<ProductViewModel>(product));
        }

        [HttpPost]

        public IActionResult Update(ProductViewModel updateProduct)
        {
            if (!ModelState.IsValid)
            {
                var product = _context.Products.Find(updateProduct.Id);
                ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new() {Data="Mavi",Value="Mavi" },
                new() {Data="Sarı",Value="Sarı" },
                new() {Data="Kırmızı",Value="Kırmızı" },
                new() {Data="Turuncu",Value="Turuncu" },
                new() {Data="Yeşil",Value="Yeşil" }


            }, "Value", "Data", product.Color);
                return View();
            }
                _context.Products.Update(_mapper.Map<Product>(updateProduct));
                _context.SaveChanges();

                TempData["status"] = "Ürün başarıyla güncellendi.";

                return RedirectToAction("Index");
          
        }
        public IActionResult HasProductName(string Name)
        {
            var anyProduct = _context.Products.Any(x => x.Name.ToLower() == Name.ToLower());

            if(anyProduct)
            {
                return Json("Kaydetmeye çalıştığınız ürün ismi vertabanında bulunmaktadır.");
            }
            else
            {
                return Json(true);

            }
     }





    }
}
