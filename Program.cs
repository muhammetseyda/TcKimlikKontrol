using Microsoft.Extensions.DependencyInjection;
using TcKimlikKontrol.Models;
using TcKimlikKontrol.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using TcKimlikKontrol.Controllers;
using System.Timers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TcKimlikDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
builder.Services.AddScoped<NviService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Create}/{id?}");

//app.MapRazorPages();



app.Run();


