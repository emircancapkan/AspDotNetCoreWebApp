using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Web.ViewModels;

public class VisitorsViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is mandatory")]
    public string Name { get; set; }

    [Range(1,300,ErrorMessage = "You can write maximum 300 characters!")]
    public string Comment { get; set; }

    public DateTime Published { get; set; }
}
