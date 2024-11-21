using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebApplication3.Web.Filters;
using WebApplication3.Web.Helpers;
using WebApplication3.Web.Middlewares;
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

builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));

//her bir requestte bağlanır ve response'a kadar açık kalır. Birden fazla nesne örneği aynı nesnedir
builder.Services.AddScoped<IHelper,Helper>();

//scope gibi çalışır fakat farklı nesne örnekleri üretir
builder.Services.AddTransient<IHelper,Helper>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<NotFoundFilter>();

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


#region Use ve Run kullanımı
/*
app.Use(async(context, next)=>{
    await context.Response.WriteAsync("Before 1. middleware\n");

    await next();

    await context.Response.WriteAsync("After 1. middleware\n");
});

app.Use(async(context, next)=>{
    await context.Response.WriteAsync("Before 2. middleware\n");

    await next();

    await context.Response.WriteAsync("After 2. middleware\n");
});



app.Run(async(context)=>{
    await context.Response.WriteAsync("Terminal 3. middleware\n");
});*/


//map ile sadece example url'inde çalışan middleware yapıyoruz.

#endregion

#region Map kullanımı ve use ve run
/*
app.Map("/example", app=>{


    app.Use(async(context, next)=>{
        await context.Response.WriteAsync("Before 1. middleware\n");

        await next();

        await context.Response.WriteAsync("After 1. middleware\n");
    });

    app.Use(async(context, next)=>{
        await context.Response.WriteAsync("Before 2. middleware\n");

        await next();

        await context.Response.WriteAsync("After 2. middleware\n");
    });


    app.Run(async(context)=>{
        await context.Response.WriteAsync("Terminal 3. middleware\n");
    });

});
*/
#endregion

#region MapWhen kullanımı
/*
//eğer urlde query olarak name varsa bu middleware çalışır. Örneğin:
//https://localhost:7143/?name=ahmet
app.MapWhen(context=>context.Request.Query.ContainsKey("name"),app=>{
    app.Use(async(context,next)=>{
        await context.Response.WriteAsync("Before 1. middleware\n");
        await next();
        await context.Response.WriteAsync("After 1. middleware\n");

    });

    app.Run(async (context)=>{
        await context.Response.WriteAsync("Terminal 3. middleware\n");
    });
});
*/
#endregion

app.Run();
