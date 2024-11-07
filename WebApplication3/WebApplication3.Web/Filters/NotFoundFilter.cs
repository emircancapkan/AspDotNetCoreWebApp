using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication3.Web.Models;

namespace WebApplication3.Web.Filters;

public class NotFoundFilter:ActionFilterAttribute
{
    private readonly AppDbContext _context;

    public NotFoundFilter(AppDbContext context)
    {
        _context=context;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        //action methodunun içindeki ilk paramtreyi alır.
        var idValue=context.ActionArguments.Values.First();

        var id=(int)idValue;

        var hasProduct=_context.Products.Any(x=>x.Id==id);

        if(hasProduct==null){
            context.Result=new RedirectToActionResult("Error","Home",null);
            
        }
        
    }
}
