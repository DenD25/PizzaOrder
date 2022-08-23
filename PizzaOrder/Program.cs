using Microsoft.EntityFrameworkCore;
using PizzaOrder.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//Data base connection
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Routing
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller}/{action}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Main}/{action=Index}/{id?}");

app.Run();
