using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Web.Helpers;
using WebApplication3.Web.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});

//sadece bir kere bağlanır
builder.Services.AddSingleton<IHelper,Helper>();

//her bir requestte bağlanır ve response'a kadar açık kalır. Birden fazla nesne örneği aynı nesnedir
builder.Services.AddScoped<IHelper,Helper>();

//scope gibi çalışır fakat farklı nesne örnekleri üretir
builder.Services.AddTransient<IHelper,Helper>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

/*

app.MapControllerRoute(
    name: "productPages",
    pattern: "blog/{*article}",
    defaults: new{controller="blog",action="article"});

app.MapControllerRoute(
    name: "productPages",
    pattern: "{controller}/{action}/{page}/{pageSize}");

app.MapControllerRoute(
    name: "getbyid",
    pattern: "{controller}/{action}/{productid}");

*/
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
