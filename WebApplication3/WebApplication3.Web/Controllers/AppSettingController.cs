using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApplication3.Web.Controllers;

public class AppSettingController:Controller
{
    private readonly IConfiguration _configuration;
    public AppSettingController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult Index(){

        //3 farklı yol
        ViewBag.BaseUrl=_configuration["BaseUrl"];
        ViewBag.FacebookKey=_configuration["ApiKeys:Facebook"];
        ViewBag.GoogleKey=_configuration.GetSection("ApiKeys")["Google"];


     
        return View();
    }


}
