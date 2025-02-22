using CustomersOrderOtomation.Data.DBOperations;
using CustomersOrderOtomation.Data.Models;
using CustomersOrderOtomation.Extensions;
using CustomersOrderOtomation.Service.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddServicesDI();

builder.Services.AddDbContext<DatabaseContext>(
       options => options.UseSqlServer("name=ConnectionStrings:SqlServerConnectionString"));

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddSignalR();
builder.Services.AddScoped<StorageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Dashboard/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NotifyHub>("/notify");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
