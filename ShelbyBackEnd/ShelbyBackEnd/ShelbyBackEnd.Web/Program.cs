using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ShelbyBackEnd.Application.Common.Service;
using ShelbyBackEnd.Infrastructure.Models;
using ShelbyBackEnd.Services.Contract;
using ShelbyBackEnd.Services.Service;
using System.Reflection;
using AutoMapper;
using ShelbyBackEnd.Web.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContextPool<ShelbyECommContext>(options =>
    options.UseSqlServer(CryptoLib.Decrypt(SharedLib.GetRegKeyVal(@"SOFTWARE\enterprise", "CstrSec"), CryptoLib.Projects.SHELBYECOMM)));
builder.Services.AddScoped<IShelbyECommContextProcedures, ShelbyECommContextProcedures>();



 

builder.Services.AddAutoMapper(cfg => { }, typeof(Program));
var mapper = builder.Services.BuildServiceProvider().GetRequiredService<IMapper>();
mapper.ConfigurationProvider.AssertConfigurationIsValid();

builder.Services.AddScoped<IBackEndMenuService, BackEndMenuService>();
builder.Services.AddScoped<ICategorieService, CategoriesService>();
builder.Services.AddScoped<IProductService, ProductService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStaticFiles();
app.UseHttpsRedirection();


app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(Directory.GetCurrentDirectory(), "//neptuneii/SHARES/Users/sajik/Development/Media20/")),
    RequestPath = "/Media"
});

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
