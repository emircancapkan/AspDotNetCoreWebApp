using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication2.Models;
using WebApplication3.Web.Helpers;
using WebApplication3.Web.Models;

using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApplication3.Web.Controllers
{
    public class ProductsController : Controller
    {
        //private readonly ProductRepo _productRepo;

        private AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            //_productRepo = new ProductRepo();
            _context= context;


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
        public IActionResult Add(Product newProduct) {
            
            //1.yöntem
            /*var name =  HttpContext.Request.Form["Name"].ToString();
            var price = decimal.Parse(HttpContext.Request.Form["Price"].ToString());
            var stock = int.Parse(HttpContext.Request.Form["Stock"].ToString());
            var color = HttpContext.Request.Form["Color"].ToString();*/

            //2. yöntem
            //Product newProduct = new(){Name=Name,Price=Price,Stock=Stock,Color=Color};
            _context.Products.Add(newProduct);
            _context.SaveChanges();

            return RedirectToAction("Index");
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

            return View(product);
        }

        [HttpPost]
        public IActionResult Update(Product updateProduct, int productId){
            

            updateProduct.Id = productId;
            _context.Products.Update(updateProduct);
            _context.SaveChanges();
            TempData["status"]="The Product is updated succesfully!";
            return RedirectToAction("Index");

        }
    }
}
