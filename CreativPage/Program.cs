using CreativPage.Data;
using CreativPage.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<User>().AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();



builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/dashboard/Auth/login";
    options.AccessDeniedPath = "/dashboard/Auth/login";
});

var app = builder.Build();
app.UseRouting();   
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(

       name: "areas",
       pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
