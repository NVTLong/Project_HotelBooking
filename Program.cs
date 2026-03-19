using Microsoft.AspNetCore.Identity;
using Project_HotelBooking.Data;
using Project_HotelBooking.Identity;

var builder = WebApplication.CreateBuilder(args);

#region ================== SERVICES ==================

builder.Services.AddControllersWithViews();

// Infrastructure (DbContext + Identity)
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// C?u h?nh Cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";          // Customer login m?c ð?nh
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.LogoutPath = "/Account/Logout";

    options.Cookie.Name = "HotelBookingAuth";
});

#endregion

var app = builder.Build();

#region ================== SEED DATA ==================

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();

    await ApplicationDbContext.SeedAsync(userManager, roleManager);
}

#endregion

#region ================== MIDDLEWARE ==================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// QUAN TR?NG: Authentication ph?i trý?c Authorization
app.UseAuthentication();
app.UseAuthorization();

#endregion

#region ================== ROUTING ==================

// Route cho Area (Admin)
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

// Route m?c ð?nh (Customer)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

#endregion

app.Run();
