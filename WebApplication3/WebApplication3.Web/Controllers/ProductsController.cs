using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication2.Models;
using WebApplication3.Web.Helpers;
using WebApplication3.Web.Models;

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AutoMapper;
using WebApplication3.Web.ViewModels;
using WebApplication3.Web.Filters;
using Microsoft.Extensions.FileProviders;

namespace WebApplication3.Web.Controllers
{

    public class ProductsController : Controller
    {
        //private readonly ProductRepo _productRepo;

        private readonly IFileProvider _fileprovider;
        private AppDbContext _context;

        private readonly IMapper _mapper;
        public ProductsController(AppDbContext context, IMapper mapper, IFileProvider fileprovider)
        {
            //_productRepo = new ProductRepo();
            _context = context;
            _mapper = mapper;
            _fileprovider = fileprovider;
        }



        public IActionResult Index()
        {
            var products = _context.Products.ToList();

            return View(_mapper.Map<List<ProductViewModel>>(products));
            
        }
        
        [ServiceFilter(typeof(NotFoundFilter))]
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

            IActionResult result=null;

            if(ModelState.IsValid){
                try
                {

                    var product = _mapper.Map<Product>(newProduct);

                    if(newProduct.Image!=null && newProduct.Image.Length>0){
                        var root = _fileprovider.GetDirectoryContents("wwwroot");
                        var images = root.First(x => x.Name == "images");
                        
                        //eğer 2 tane aynı isimli görsel varsa bir tanesine rastgele isim atar
                        var RandomImageNames=Guid.NewGuid() + Path.GetExtension(newProduct.Image.FileName);

                        var path = Path.Combine(images.PhysicalPath, RandomImageNames);

                        using var stream = new FileStream(path, FileMode.Create);
                        newProduct.Image.CopyTo(stream);
                        product.ImagePath = newProduct.Image.FileName;
                    }


                    _context.Products.Add(product);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(Exception)
                {
                    
                    ModelState.AddModelError("Image", "Image is required.");
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

                    return View(newProduct);
                }


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
        [ServiceFilter(typeof(NotFoundFilter))]
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

            return View(_mapper.Map<ProductUpdateViewModel>(product));
        }

        [HttpPost]
        public IActionResult Update(ProductViewModel updateProduct)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ExpireValue = updateProduct.Expire;
                ViewBag.ExpireDate = new Dictionary<string, int>(){
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
                }, "Value", "Key", updateProduct.Color);

                return View();
            }

            if (updateProduct.Image != null && updateProduct.Image.Length > 0)
            {
                var root = _fileprovider.GetDirectoryContents("wwwroot");
                var images = root.First(x => x.Name == "images");

                // Rastgele isim oluşturuluyor
                var RandomImageNames = Guid.NewGuid() + Path.GetExtension(updateProduct.Image.FileName);

                var path = Path.Combine(images.PhysicalPath, RandomImageNames);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    updateProduct.Image.CopyTo(stream);
                }

                // Yolu veritabanında saklamak için ImagePath'e atıyoruz
                updateProduct.ImagePath = RandomImageNames;
            }

            var product = _mapper.Map<Product>(updateProduct);
            _context.Products.Update(product);
            _context.SaveChanges();
            TempData["status"] = "The Product is updated successfully!";
            return RedirectToAction("Index");
        }



        //not found filter içinde bir parametre (context) olduğu için ServiceFilter kullanırız
        [ServiceFilter(typeof(NotFoundFilter))]
        [Route("[controller]/[action]/{productid}", Name = "getbyid")]
        public IActionResult GetById(int productid){
            var product=_context.Products.Find(productid);

            return View(_mapper.Map<ProductViewModel>(product));
        }


        [Route("[controller]/[action]/{page}/{pageSize}")]
        public IActionResult Pages(int page, int pageSize){
            
            //page 1 pagesize=3
            //page 2 pagesize=3
            var products=_context.Products.Skip((page-1)*pageSize).Take(pageSize).ToList();

            ViewBag.Page=page;
            ViewBag.pageSize=pageSize;
            
            return View(_mapper.Map<List<ProductViewModel>>(products));
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
