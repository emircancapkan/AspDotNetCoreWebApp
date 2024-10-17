using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using WebApplication2.Models;
using WebApplication3.Web.Models;

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

            /*if(!_context.Products.Any()){
                _context.Products.Add(new Product() {Name="VW", Price=30000, Stock=200, Color="Red"}); 
                _context.Products.Add(new Product() {Name="Audi", Price=20000, Stock=100, Color="Black"});  
                _context.Products.Add(new Product() {Name="BMW", Price=40000, Stock=300, Color="Blue"});  
                _context.Products.Add(new Product() {Name="Mercedes", Price=50000, Stock=500, Color="White"});  

                _context.SaveChanges();
            }*/
           

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
