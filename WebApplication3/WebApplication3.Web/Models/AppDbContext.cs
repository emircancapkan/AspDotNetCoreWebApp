﻿using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication3.Web.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}

        public DbSet<Product> Products  { get; set; }

        public DbSet<Visitors> Visitors {get; set;}

        public DbSet<Categories> Categories { get; set; }
    }
}
