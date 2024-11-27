using System;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication2.Models;

namespace WebApplication3.Web.Models;

public class Categories
{
    public int Id { get; set; }

    public string Name { get; set; }

    public List<Product>? Products {get; set;}

}
