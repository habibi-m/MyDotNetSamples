using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PermissionBasedAuthorization.Data;
using PermissionBasedAuthorization.Permission.PermissionBasedAuthorization.Permission;
using PermissionBasedAuthorization.Permission;
using PermissionBasedAuthorization.Seeds;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddDbContext<ApplicationDbContext>(options => 
        options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();
builder.Services.AddScoped<UserManager<IdentityUser>>();
builder.Services.AddScoped<RoleManager<IdentityRole>>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

await SeedDatabase(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();


async Task SeedDatabase(WebApplication host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("app");

        try
        {
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await DefaultRoles.SeedAsync(userManager, roleManager);
            await DefaultUsers.SeedBasicUserAsync(userManager, roleManager);
            await DefaultUsers.SeedSuperAdminAsync(userManager, roleManager);

            logger.LogInformation("Finished Seeding Default Data");
            logger.LogInformation("Application Starting");
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "An error occurred seeding the DB");
        }
    }
}