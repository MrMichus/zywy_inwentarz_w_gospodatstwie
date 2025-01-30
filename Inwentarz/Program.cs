using Microsoft.EntityFrameworkCore;

using Inwentarz.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Konfiguracja po��czenia z baz� danych
builder.Services.AddDbContext<InwentarzDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dodaj Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<InwentarzDbContext>()
    .AddDefaultTokenProviders();

// Dodanie sesji
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Czas trwania sesji
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Dodanie obs�ugi MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// W��cz sesj�
app.UseSession();

app.UseAuthentication(); // Obs�uga autentykacji (je�li potrzebujesz ASP.NET Identity w przysz�o�ci)
app.UseAuthorization();

//role
var scope = app.Services.CreateScope();
await SeedData.InitializeAsync(scope.ServiceProvider);


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Dodanie roli je�li nie istnieje
        if (!await roleManager.RoleExistsAsync("Pracownik"))
        {
            await roleManager.CreateAsync(new IdentityRole("Pracownik"));
        }
        if (!await roleManager.RoleExistsAsync("Uzytkownik"))
        {
            await roleManager.CreateAsync(new IdentityRole("Uzytkownik"));
        }
    }
}
