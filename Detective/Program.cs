using Microsoft.EntityFrameworkCore;
using DetectiveAgencyProject.Models;

var builder = WebApplication.CreateBuilder(args);

// Додати служб для підключення до бази даних
builder.Services.AddDbContext<DetectiveAgencyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Додати контролери з виглядом
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

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
    }
    catch (Exception ex)
    {
        // Логувати помилку (використайте свій механізм логування)
        Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
    }
}

app.Run();