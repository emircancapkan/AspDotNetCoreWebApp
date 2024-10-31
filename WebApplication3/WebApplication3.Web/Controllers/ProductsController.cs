using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication2.Models;
using WebApplication3.Web.Helpers;
using WebApplication3.Web.Models;

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AutoMapper;
using WebApplication3.Web.ViewModels;

namespace WebApplication3.Web.Controllers
{
    public class ProductsController : Controller
    {
        //private readonly ProductRepo _productRepo;

        private AppDbContext _context;

        private readonly IMapper _mapper;
        public ProductsController(AppDbContext context, IMapper mapper)
        {
            //_productRepo = new ProductRepo();
            _context= context;

            _mapper=mapper;


        }
        public IActionResult Index()
        {
            var products = _context.Products.ToList();

            return View(_mapper.Map<List<ProductViewModel>>(products));
            
        }

        public IActionResult Remove(int id)
        {
            var product = _context.Products.Find(id);

            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Add(){

            ViewBag.ExpireDate = new Dictionary<string,int>(){
                {"1 Month",1},
                {"3 Month",3},
                {"6 Month",6},
                {"9 Month",9},
                {"12 Month",12},
            };

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>(){
                new (){ Key= "Black" , Value="Black"},
                new (){ Key = "Blue", Value="Blue"},
                new (){ Key = "Red", Value="Red"},
                new (){ Key = "White", Value="White"}
            },"Value","Key");

            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel newProduct) {
            
            //1.yöntem
            /*var name =  HttpContext.Request.Form["Name"].ToString();
            var price = decimal.Parse(HttpContext.Request.Form["Price"].ToString());
            var stock = int.Parse(HttpContext.Request.Form["Stock"].ToString());
            var color = HttpContext.Request.Form["Color"].ToString();*/

            //2. yöntem
            //Product newProduct = new(){Name=Name,Price=Price,Stock=Stock,Color=Color};

            if(ModelState.IsValid){
                _context.Products.Add(_mapper.Map<Product>(newProduct));
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            else{
            
                ViewBag.ExpireDate = new Dictionary<string,int>(){
                    {"1 Month",1},
                    {"3 Month",3},
                    {"6 Month",6},
                    {"9 Month",9},
                    {"12 Month",12},
                };

                ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>(){
                    new (){ Key= "Black" , Value="Black"},
                    new (){ Key = "Blue", Value="Blue"},
                    new (){ Key = "Red", Value="Red"},
                    new (){ Key = "White", Value="White"}
                },"Value","Key");

                return View();

            }

        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            
            var product=_context.Products.Find(id);

            ViewBag.ExpireValue=product.Expire;
            ViewBag.ExpireDate = new Dictionary<string,int>(){
                {"1 Month",1},
                {"3 Month",3},
                {"6 Month",6},
                {"9 Month",9},
                {"12 Month",12},
            };

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>(){
                new (){ Key= "Black" , Value="Black"},
                new (){ Key = "Blue", Value="Blue"},
                new (){ Key = "Red", Value="Red"},
                new (){ Key = "White", Value="White"}
            },"Value","Key",product.Color);

            return View(_mapper.Map<ProductViewModel>(product));
        }

        [HttpPost]
        public IActionResult Update(Product updateProduct){

            if(!ModelState.IsValid){

                ViewBag.ExpireValue=updateProduct.Expire;
                ViewBag.ExpireDate = new Dictionary<string,int>(){
                    {"1 Month",1},
                    {"3 Month",3},
                    {"6 Month",6},
                    {"9 Month",9},
                    {"12 Month",12},
                };

                ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>(){
                    new (){ Key= "Black" , Value="Black"},
                    new (){ Key = "Blue", Value="Blue"},
                    new (){ Key = "Red", Value="Red"},
                    new (){ Key = "White", Value="White"}
                },"Value","Key",updateProduct.Color);

                    return View();
            }

            _context.Products.Update(updateProduct);
            _context.SaveChanges();
            TempData["status"]="The Product is updated succesfully!";
            return RedirectToAction("Index");

        }

        [HttpGet]
        [HttpPost]
        public IActionResult HasProductName(string Name){
            var anyproduct=_context.Products.Any(x=>x.Name.ToLower()==Name.ToLower());

            if(anyproduct){
                return Json("The product name is available in the database");
            }

            else{
                return Json(true);
            }
        }
    }
}
