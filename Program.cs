using CromWood.Business.Services.Implementation;
using CromWood.Business.Services.Interface;
using CromWood.Data.Context;
using CromWood.Data.Repository.Implementation;
using CromWood.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Connection to database using connection string.
builder.Services.AddDbContext<CromwoodContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CromWoodDBConnection")));

// Dependency Injection for Repository and Service layers.
builder.Services.AddScoped<ITestRepository, TestRepository>();
builder.Services.AddScoped<ITestService, TestService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Test}/{action=Index}/{id?}");

app.Run();
