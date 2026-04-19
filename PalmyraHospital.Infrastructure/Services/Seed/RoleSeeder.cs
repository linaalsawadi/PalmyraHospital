using Microsoft.AspNetCore.Identity;
using PalmyraHospital.Application.Interfaces.Seed;
using PalmyraHospital.Domain.Constants;
using System.Data;

namespace PalmyraHospital.Infrastructure.Services.Seed;

public class RoleSeeder : IRoleSeeder
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleSeeder(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task SeedAsync()
    {
        foreach (var role in Roles.All)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(role));

                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create role: {role}");
                }
            }
        }
    }
}
