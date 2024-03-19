using Courses.Business.Managers;
using Courses.Business.Services;
using Courses.Data.Context;
using Courses.Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString
    ("DefaultConnection");

builder.Services.AddDbContext<CoursesContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICourseService, CourseManager>();
builder.Services.AddScoped<IInstructorService, InstructorManager>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = new PathString("/GirisYap");
    options.LogoutPath = new PathString("/CikisYap");
    options.AccessDeniedPath = new PathString("/Errors/Error403");
});

var app = builder.Build();

app.UseStatusCodePagesWithRedirects("/Errors/Error{0}");

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{Controller=Main}/{Action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "Default",
    pattern: "{Controller=Home}/{Action=Index}/{id?}"
    );

app.Run();
