using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Web.Models;
using WebApplication3.Web.ViewModels;

namespace WebApplication3.Web.Controllers;

[Route("[controller]/[action]")]

public class VisitorAjaxController:Controller
{

    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public VisitorAjaxController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IActionResult Index(){
        return View();
    }

    [HttpPost]
    public IActionResult SaveChangesVisitor(VisitorsViewModel visitorsViewModel)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { IsSuccess = false, Message = "Invalid data" });
        }

        try
        {
            var visitor = _mapper.Map<Visitors>(visitorsViewModel);
            visitor.Published = DateTime.Now;
            _context.Visitors.Add(visitor);
            _context.SaveChanges();

            return Json(new { IsSuccess = true });
        }
        catch (Exception)
        {
            return Json(new { IsSuccess = false });
        }
    }


    [HttpGet]
    public IActionResult Visitor(){
        var visitors=_context.Visitors.ToList();

        var visitorViewModel=_mapper.Map<List<VisitorsViewModel>>(visitors);

        return Json(visitorViewModel);
    }

}
