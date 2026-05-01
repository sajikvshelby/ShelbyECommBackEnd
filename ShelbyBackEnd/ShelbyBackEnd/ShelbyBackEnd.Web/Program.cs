using Microsoft.EntityFrameworkCore;
using ShelbyBackEnd.Application.Common.Service;
using ShelbyBackEnd.Infrastructure.Models;
using ShelbyBackEnd.Services.Contract;
using ShelbyBackEnd.Services.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContextPool<ShelbyECommContext>(options =>
    options.UseSqlServer(CryptoLib.Decrypt(SharedLib.GetRegKeyVal(@"SOFTWARE\enterprise", "CstrSec"), CryptoLib.Projects.SHELBYECOMM)));
builder.Services.AddScoped<IShelbyECommContextProcedures, ShelbyECommContextProcedures>();

builder.Services.AddScoped<IBackEndMenuService, BackEndMenuService>();
builder.Services.AddScoped<ICategorieService, CategoriesService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
