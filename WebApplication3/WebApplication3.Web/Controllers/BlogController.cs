using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Web.Controllers
{
    public class BlogController : Controller
    {
        // GET: BlogController
        public IActionResult Article(){
            var routes=Request.RouteValues["articles"];

            return View();
        }

    }
}
