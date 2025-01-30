using Microsoft.EntityFrameworkCore;

using Inwentarz.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Konfiguracja po³¹czenia z baz¹ danych
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

// Dodanie obs³ugi MVC
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

// W³¹cz sesjê
app.UseSession();

app.UseAuthentication(); // Obs³uga autentykacji (jeœli potrzebujesz ASP.NET Identity w przysz³oœci)
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

        // Dodanie roli jeœli nie istnieje
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
