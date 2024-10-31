using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Web.ViewModels;

public class ProductViewModel
{

        public int Id { get; set; }


        [Remote(action:"HasProductName", controller:"Products")]
        [Required(ErrorMessage = "This section cannot be blank")]
        public string? Name { get; set; }

        public decimal Price { get; set; }

        [Range(1,300, ErrorMessage = "The Stock must be between 1 and 300")]
        public int Stock { get; set; }

        public string? Color { get; set; }

        public bool isPublished {get; set;}

        public int? Expire{ get; set; }

        [StringLength(300,ErrorMessage = "Maximum 300 characters!")]
        public string? Description{ get; set; }

        [Required(ErrorMessage = "This section cannot be blank")]
        public string? Email { get; set; }
        

}
