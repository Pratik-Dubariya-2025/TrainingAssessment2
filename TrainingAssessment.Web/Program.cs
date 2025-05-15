using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TrainingAssessment.Models.Data;
using TrainingAssessment.Repository.Repository;
using TrainingAssessment.Repository.Repository.IRepository;
using TrainingAssessment.Service.Service;
using TrainingAssessment.Service.Service.IService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TrainingAssessmentDbContext>
    (options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection"), s => s.MigrationsAssembly("TrainingAssessment.Web")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IHomeService, HomeService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
    options.LoginPath = "/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});
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
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
