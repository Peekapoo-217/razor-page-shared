using Microsoft.EntityFrameworkCore;
using System;
using TH.RazorPages.Data;
using TH.RazorPages.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbContect>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AccessData")));
builder.Services.AddScoped<ProductServices>();


// Add services to the container.
builder.Services.AddRazorPages();

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

app.MapRazorPages();

app.Run();
