using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication3.Web.Models;
using WebApplication3.Web.ViewModels;

namespace WebApplication3.Web.Views.Shared.ViewComponents;


public class ProductListViewComponent:ViewComponent
{
    private readonly AppDbContext _context;

    public ProductListViewComponent(AppDbContext context){
        _context=context;
    }
    public async Task<IViewComponentResult> InvokeAsync(int type=1){
        var view_models=_context.Products.Select(x=> new ProductListViewComponentModel(){
            Id=x.Id,
            Name=x.Name,
            Description=x.Description
        }).ToList();

    if(type==1)
        return View("Default",view_models);

    else
        return View("Type2",view_models);

    }

}
