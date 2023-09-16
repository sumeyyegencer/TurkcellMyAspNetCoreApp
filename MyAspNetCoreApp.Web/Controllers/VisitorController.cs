using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Models;
using MyAspNetCoreApp.Web.ViewModels;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class VisitorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public VisitorController(IMapper mapper,AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SaveVisitorComment(VisitorViewModel visitorViewModel)
        {
            var visitor = _mapper.Map<Visitor>(visitorViewModel);
            visitor.Created = DateTime.Now;
            _context.Visitors.Add(visitor);
            return Json(new { isSuccess = "true" });
        }

        public IActionResult VisitorCommentList()
        {
            var visitors = _context.Visitors.ToList();
            var visitorViewModel = _mapper.Map<List<VisitorViewModel>>(visitors);

            return Json(visitorViewModel);
            
        }







    }
}
