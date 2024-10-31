using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication3.Web.Models;
using WebApplication3.Web.ViewModels;

namespace WebApplication3.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper=mapper;
        }

        public IActionResult Index()
        {
            // Veritabanından ürünleri getir
            var products = _context.Products.OrderByDescending(x => x.Id).Select(x => new ProductPartialViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Stock = x.Stock
            }).ToList();


            // ViewBag'e dönüştürülmüş listeyi koy
            ViewBag.ProductListPartialViewModel = new ProductListPartialViewModel()
            {
                Products = products
            };

            return View();
        }


        public IActionResult Privacy()
        {
                       // Veritabanından ürünleri getir
            var products = _context.Products.OrderByDescending(x => x.Id).Select(x => new ProductPartialViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Stock = x.Stock
            }).ToList();


            // ViewBag'e dönüştürülmüş listeyi koy
            ViewBag.ProductListPartialViewModel = new ProductListPartialViewModel()
            {
                Products = products
            };

            return View();
        }

        public IActionResult Visitor(){
            return View();
        }

        [HttpPost]
        public IActionResult SaveChangesVisitor(VisitorsViewModel visitorsViewModel){

            try{
                var visitor=_mapper.Map<Visitors>(visitorsViewModel);
                visitor.Published=DateTime.Now;
                _context.Visitors.Add(visitor);
                _context.SaveChanges();
                TempData["result"]="Comment has been saved";
                return RedirectToAction(nameof(HomeController.Visitor));


            }
            catch(Exception){
                TempData["result"]="Comment couldn't has been saved";
                return RedirectToAction(nameof(HomeController.Visitor));
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
