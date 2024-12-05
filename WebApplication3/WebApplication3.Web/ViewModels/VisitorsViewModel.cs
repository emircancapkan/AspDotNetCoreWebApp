using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Web.ViewModels;

public class VisitorsViewModel
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Comment { get; set; }

    public DateTime Published { get; set; }
}
