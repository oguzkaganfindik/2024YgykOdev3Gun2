using Courses.Business.Managers;
using Courses.Business.Services;
using Courses.Data.Context;
using Courses.Data.Repositories;
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


var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "Default",
    pattern: "{Controller=Courses}/{Action=Index}/{id?}"
    );

app.Run();
