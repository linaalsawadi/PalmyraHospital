using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PalmyraHospital.Application.Interfaces.Seed;
using PalmyraHospital.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalmyraHospital.Infrastructure.Services.Seed
{
    public class UserSeeder : IUserSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public UserSeeder(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task SeedAsync()
        {
            var adminEmail = _configuration["SeedSettings:AdminEmail"];
            var adminPassword = _configuration["SeedSettings:AdminPassword"];

            if (await _userManager.FindByEmailAsync(adminEmail) != null)
                return;

            var adminUser = new ApplicationUser
            {
                FirstName = "System",
                LastName = "Administrator",
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                PhoneNumber = "0512345678",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _userManager.CreateAsync(adminUser, adminPassword);
            await _userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
