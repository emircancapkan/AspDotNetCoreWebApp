using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Web.Models;
using WebApplication3.Web.ViewModels;

namespace WebApplication3.Web.Views.Shared.ViewComponents;

public class VisitorsViewComponent:ViewComponent
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public VisitorsViewComponent(AppDbContext context, IMapper mapper)
    {
        _context=context;
        _mapper=mapper;
    }
    public async Task<IViewComponentResult> InvokeAsync(){
        var visitors = _context.Visitors.ToList();
        var visitorViewModel=_mapper.Map<List<VisitorsViewModel>>(visitors);
        ViewBag.visitorsViewModel=visitorViewModel;
        return View();
    }

}
