using System;
using AutoMapper;
using WebApplication2.Models;
using WebApplication3.Web.Models;
using WebApplication3.Web.ViewModels;

namespace WebApplication3.Web.Mapping;

public class ViewModelMapping:Profile
{
    public ViewModelMapping()
    {   
        CreateMap<Product,ProductViewModel>().ReverseMap();
        CreateMap<Visitors,VisitorsViewModel>().ReverseMap();
    }

}
