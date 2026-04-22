using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PalmyraHospital.Infrastructure.Data;
using PalmyraHospital.Infrastructure.Identity;
using System;
using PalmyraHospital.Application.Interfaces.Seed;
using PalmyraHospital.Infrastructure.Services.Seed;
using PalmyraHospital.Infrastructure.Services;
using PalmyraHospital.Application.Interfaces.Auth;
using PalmyraHospital.Application.Implementations;
using PalmyraHospital.Application.Interfaces.Redirect;
using PalmyraHospital.Infrastructure.Services.Admin;
using PalmyraHospital.Application.Interfaces.Admin;
using PalmyraHospital.Infrastructure.Repositories;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => {
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders(); 

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    options.SlidingExpiration = false;
});

builder.Services.AddScoped<IDatabaseMigrator, DatabaseMigrator>();
builder.Services.AddScoped<IRoleSeeder, RoleSeeder>();
builder.Services.AddScoped<IUserSeeder, UserSeeder>();
builder.Services.AddScoped<ISeedOrchestrator, SeedOrchestrator>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRedirectService, UserRedirectService>();
builder.Services.AddScoped<IAdminDashboardService, AdminDashboardService>(); 
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<ILookupService, LookupService>();
builder.Services.AddScoped<ISpecializationService, SpecializationService>();
builder.Services.AddScoped<ISpecializationRepository, SpecializationRepository>();
builder.Services.AddScoped<IPatientService, PatientService>();


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var orchestrator = scope.ServiceProvider.GetRequiredService<ISeedOrchestrator>();
    await orchestrator.SeedAsync();
}



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

// 🔥 Route خاص بالـ Areas (مهم جدًا)
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

// 🔥 Route مباشر لـ /Admin
app.MapControllerRoute(
    name: "admin-root",
    pattern: "Admin",
    defaults: new { area = "Admin", controller = "Admin", action = "Index" });

// 🔥 Default Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
