using System;
using WebApplication2.Models;

namespace WebApplication3.Web.Models;

public class Category
{
    public int Id { get; set; }

    public string Name { get; set; }

    public List<Product> Products {get; set;}

}
