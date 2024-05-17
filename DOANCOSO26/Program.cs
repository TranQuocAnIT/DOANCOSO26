
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;


using DOANCOSO26.Data;
using DOANCOSO26.Models;
using DOANCOSO26.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));





builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{

}).AddDefaultUI().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();


builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IBusRepository, EFBusRepository>();
builder.Services.AddScoped<IBusTripRepository, EFBusTripRepository>();





builder.Services.AddControllersWithViews();


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
//---
app.UseSession();
//
app.UseRouting();

app.UseAuthorization();
app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=ProductManager}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//-----



app.Run();
/////
