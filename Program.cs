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
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Cấu hình DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IResidentRepository, ResidentRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IApartmentRepository, ApartmentRepository>();
builder.Services.AddScoped<ISettingRepository, SettingRepository>();

// Cấu hình Identity
builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
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

// Seed roles & users mặc định
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // Danh sách role mặc định
    string[] roles = { "Admin", "Manager", "Resident", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole<Guid>(role));
    }

    // Hàm tạo user chung có giới tính
    async Task SeedUserAsync(string email, string password, string fullName, string phoneNumber, string position, string role, bool isResident, int sex)
    {
        var existingUser = await userManager.FindByEmailAsync(email);
        if (existingUser == null)
        {
            var random = new Random();

            var newUser = new User
            {
                UserName = email,
                Email = email,
                FullName = fullName,
                Address = "HCM",
                DocumentNumber = "0" + string.Concat(Enumerable.Range(0, 11).Select(_ => random.Next(0, 10).ToString())),
                Sex = sex,
                Position = position,
                IsResident = isResident,
                PhoneNumber = phoneNumber,
                Status = 1, // 1 = active
                CreatedDate = DateTime.Now,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await userManager.CreateAsync(newUser, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(newUser, role);
            }
            else
            {
                throw new Exception($"Seed user {email} thất bại: " +
                    string.Join("; ", result.Errors.Select(e => e.Description)));
            }
        }
    }

    // --- Tạo Admin
    await SeedUserAsync("admin@example.com", "Admin@123", "Quản trị", "0909000001", "Admin", "Admin", false, 1);

    // --- Tạo Manager
    await SeedUserAsync("manager@example.com", "Manager@123", "Nhân viên quản lý", "0909000002", "Manager", "Manager", false, 1);

    // --- 4 nhân viên
    await SeedUserAsync("guard@example.com", "Employee@123", "Bảo vệ 1", "0901000001", "Bảo vệ", "User", false, 1);
    await SeedUserAsync("reception@example.com", "Employee@123", "Lễ tân 1", "0901000002", "Lễ tân", "User", false, 0);
    await SeedUserAsync("maintenance@example.com", "Employee@123", "Bảo trì 1", "0901000003", "Bảo trì", "User", false, 1);
    await SeedUserAsync("cleaning@example.com", "Employee@123", "Lao công 1", "0901000004", "Lao công", "User", false, 0);

    // --- 5 cư dân
    await SeedUserAsync("resident1@example.com", "Resident@123", "Nguyen Van A", "0911000001", "", "User", true, 1);
    await SeedUserAsync("resident2@example.com", "Resident@123", "Nguyen Van B", "0911000002", "", "User", true, 1);
    await SeedUserAsync("resident3@example.com", "Resident@123", "Tran Thi C", "0911000003", "", "User", true, 0);
    await SeedUserAsync("resident4@example.com", "Resident@123", "Le Van D", "0911000004", "", "User", true, 1);
    await SeedUserAsync("resident5@example.com", "Resident@123", "Pham Thi E", "0911000005", "", "User", true, 0);

    //// --- Seed VehicleTypes
    //if (!dbContext.VehicleTypes.Any())
    //{
    //    dbContext.VehicleTypes.AddRange(
    //        new VehicleType { Name = "Xe máy" },
    //        new VehicleType { Name = "Ô tô" },
    //        new VehicleType { Name = "Xe tải" },
    //        new VehicleType { Name = "Xe đạp" },
    //        new VehicleType { Name = "Xe đạp" }

    //    );
    //    await dbContext.SaveChangesAsync();
    //}

    if (!dbContext.VehicleTypes.Any())
    {
        var adminId = Guid.Parse("00000000-0000-0000-0000-000000000000"); // có thể thay bằng admin thực
        dbContext.VehicleTypes.AddRange(
            new VehicleType { Name = "Xe máy", Fee = 100000, CreatedBy = adminId, CreatedDate = DateTime.Now, Status = 1 },
            new VehicleType { Name = "Ô tô", Fee = 500000, CreatedBy = adminId, CreatedDate = DateTime.Now, Status = 1 },
            new VehicleType { Name = "Xe tải", Fee = 300000, CreatedBy = adminId, CreatedDate = DateTime.Now, Status = 1 },
            new VehicleType { Name = "Xe đạp", Fee = 50000, CreatedBy = adminId, CreatedDate = DateTime.Now, Status = 1 }
        );
        await dbContext.SaveChangesAsync();
    }


}

// Route mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
