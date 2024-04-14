using Microsoft.EntityFrameworkCore;
using Planer1.Models;

var builder = WebApplication.CreateBuilder(args);

using (PlaneralContext db = new PlaneralContext())
{
 
 db.SaveChanges();
}

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PlaneralContext>(options => options.UseNpgsql(connection));

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
