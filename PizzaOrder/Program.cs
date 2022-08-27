using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PizzaOrder.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//Data base connection
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

//Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
        });

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Routing
app.MapAreaControllerRoute(
    name: "adminRoute",
    areaName: "Admin",
    pattern: "Admin/{controller=Main}/{action=Index}/{id?}");
app.MapAreaControllerRoute(
    name: "managerRoute",
    areaName: "Manager",
    pattern: "Manager/{controller=Main}/{action=Index}/{id?}");
app.MapAreaControllerRoute(
    name: "userRoute",
    areaName: "User",
    pattern: "User/{controller=Main}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Main}/{action=Index}/{id?}");

app.Run();
