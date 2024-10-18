using Microsoft.EntityFrameworkCore;
using DetectiveAgencyProject.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Додати служб для підключення до бази даних
builder.Services.AddDbContext<DetectiveAgencyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Налаштування Identity з ролями
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>() // Додати підтримку ролей
    .AddEntityFrameworkStores<DetectiveAgencyDbContext>();

// Додати контролери з виглядом
builder.Services.AddControllersWithViews();

// Додати сторінки Razor
builder.Services.AddRazorPages();

var app = builder.Build();

// Налаштування конвеєра HTTP запитів
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Значення HSTS за замовчуванням - 30 днів
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

// Карта контролерів
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Запустити міграцію та створити базу даних
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<DetectiveAgencyDbContext>();
        context.Database.Migrate(); // Виконати міграцію

        // Сидування ролей
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await SeedRolesAsync(roleManager); // Викликаємо метод сидування ролей
    }
    catch (Exception ex)
    {
        // Логувати помилку (використайте свій механізм логування)
        Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
    }
}

app.Run();

// Метод сидування ролей
async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
{
    string[] roleNames = { "Cool", "NotCool" };

    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}
