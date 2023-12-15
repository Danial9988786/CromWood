using CromWood.Business.Services.Implementation;
using CromWood.Business.Services.Interface;
using CromWood.Data.Context;
using CromWood.Data.Repository.Implementation;
using CromWood.Data.Repository.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

// Connection to database using connection string.
builder.Services.AddDbContext<CromwoodContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CromWoodDBConnection")));

// Dependency Injection for Repository and Service layers.
builder.Services.AddScoped<ITestRepository, TestRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();

builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRolePermissionService, RolePermissionService>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {   option.LoginPath = "/Auth/Login";
        option.LogoutPath = "/Auth/Logout";
    });

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Test}/{action=Index}/{id?}");

app.Run();
