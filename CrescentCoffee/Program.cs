//using CrescentCoffee.App;
using CrescentCoffee.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("CrescentCoffeeDbContextConnection") ??
    throw new InvalidOperationException("Connection string 'CrescentCoffeeDbContextConnection' not found");

builder.Services.AddDbContext<CrescentCoffeeDbContext>(options => options.UseSqlServer(connectionString));;
builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<CrescentCoffeeDbContext>();;

builder.Services.AddScoped<ICoffeeTypeRepository, CoffeeTypeRepository>();
builder.Services.AddScoped<ICoffeeRepository, CoffeeRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });;

builder.Services.AddRazorPages();
//builder.Services.AddRazorComponents().AddInteractiveServerComponents();
//builder.Services.AddRazorComponents()
//    .AddInteractiveServerComponents();

//builder.Services.AddServerSideBlazor();
//builder.Services.AddControllers();

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();

app.MapRazorPages();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapDefaultControllerRoute();
//app.MapControllers();
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller}/{action=Index}/{id?}");

//app.MapBlazorHub();
//app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

DbInitializer.Seed(app);
app.Run();
