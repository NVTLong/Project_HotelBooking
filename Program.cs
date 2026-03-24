using Microsoft.AspNetCore.Identity;
using Project_HotelBooking.Data;
using Project_HotelBooking.Identity;

var builder = WebApplication.CreateBuilder(args);

#region ================== SERVICES ==================

builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Infrastructure (DbContext + Identity)
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// Cấu hình Cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";          // Customer login mặc định
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
app.UseSession();

// QUAN TR?NG: Authentication phải trước Authorization
app.UseAuthentication();
app.UseAuthorization();

#endregion

#region ================== ROUTING ==================

// Route cho Area (Admin)
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

// Route mặc định (Customer)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

#endregion

app.Run();
