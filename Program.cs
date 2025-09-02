//using _2051010166_NguyenTranThanhLiem;
//using _2051010166_NguyenTranThanhLiem.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllersWithViews();

//// Cấu hình để thực hiện Code-First
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Login}/{id?}");

//app.Run();


using _2051010166_NguyenTranThanhLiem;
using _2051010166_NguyenTranThanhLiem.Interfaces;
using _2051010166_NguyenTranThanhLiem.Models;
using _2051010166_NguyenTranThanhLiem.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Cấu hình DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IResidentRepository, ResidentRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

// Cấu hình Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
{
    // Password policy (cho dev, production nên tăng cường)
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

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
app.UseAuthentication();
app.UseAuthorization();

// Seed roles & admin user
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    // Tạo roles nếu chưa có
    string[] roles = { "Admin", "Manager", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole<Guid>(role));
    }

    // Tạo Admin user nếu chưa có
    string adminEmail = "admin@example.com";
    string adminPassword = "Admin@123"; // Password đủ mạnh
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            SecurityStamp = Guid.NewGuid().ToString() // Bắt buộc
        };

        var result = await userManager.CreateAsync(adminUser, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }

    // Tạo Manager
    string managerEmail = "manager@example.com";
    string managerPassword = "Manager@123";
    var managerUser = await userManager.FindByEmailAsync(managerEmail);
    if (managerUser == null)
    {
        managerUser = new ApplicationUser
        {
            UserName = managerEmail,
            Email = managerEmail,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        var result = await userManager.CreateAsync(managerUser, managerPassword);
        if (result.Succeeded)
            await userManager.AddToRoleAsync(managerUser, "Manager");
    }

    // Tạo User bình thường
    string userEmail = "user@example.com";
    string userPassword = "User@123";
    var normalUser = await userManager.FindByEmailAsync(userEmail);
    if (normalUser == null)
    {
        normalUser = new ApplicationUser
        {
            UserName = userEmail,
            Email = userEmail,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        var result = await userManager.CreateAsync(normalUser, userPassword);
        if (result.Succeeded)
            await userManager.AddToRoleAsync(normalUser, "User");
    }
}

// Route mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
