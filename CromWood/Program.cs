using CromWood.Business.Services.Implementation;
using CromWood.Business.Services.Interface;
using CromWood.Data.Context;
using CromWood.Data.Entities;
using CromWood.Data.Repository.Implementation;
using CromWood.Data.Repository.Interface;
using CromWood.Helper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();
builder.Services.AddMemoryCache();
// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null); ;
builder.Services.AddHttpContextAccessor();

// Connection to database using connection string.
builder.Services.AddDbContext<CromwoodContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CromWoodDBConnection")));
builder.Services.AddScoped<IFileUploader, FileUploader>();


// Dependency Injection for Repository and Service layers.
builder.Services.AddScoped<ITestRepository, TestRepository>();
builder.Services.AddScoped<IRepository<ChangeLog>, Repository<ChangeLog>>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddScoped<IAmenityRepository, AmenityRepository>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<ILicenseCertificateRepository, LicenseCertificateRepository>();
builder.Services.AddScoped<ITenancyRepository, TenancyRepository>();
builder.Services.AddScoped<ITenantRepository, TenantRepository>();
builder.Services.AddScoped<INoticeClaimsRepository, NoticeClaimsRepository>();
builder.Services.AddScoped<IComplaintRepository, ComplaintRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IPropertyAssesmentRepository, PropertyAssesmentRepository>();
builder.Services.AddScoped(typeof(ILookupRepository<>), typeof(LookupRepository<>));

builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IChangeLogService, ChangeLogService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRolePermissionService, RolePermissionService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<IAmenityService, AmenityService>();
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<ILicenseCertificateService, LicenseCertificateService>();
builder.Services.AddScoped<ITenancyService, TenancyService>();
builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<INoticeClaimsService, NoticeClaimsService>();
builder.Services.AddScoped<IComplaintService, ComplaintService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IPropertyAssesmentService, PropertyAssesmentService>();
builder.Services.AddScoped(typeof(ILookupService<>), typeof(LookupService<>));


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.ExpireTimeSpan = TimeSpan.FromMinutes(1000);
        option.SlidingExpiration = true;
        option.LoginPath = "/Auth/Login";
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
