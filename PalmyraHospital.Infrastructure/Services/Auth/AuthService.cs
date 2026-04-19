using Microsoft.AspNetCore.Identity;
using PalmyraHospital.Application.DTOs.Auth;
using PalmyraHospital.Application.Interfaces.Auth;
using PalmyraHospital.Infrastructure.Identity;

namespace PalmyraHospital.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // ================= LOGIN =================

        public async Task<(bool Success, string? Error)> LoginAsync(LoginRequest request)
        {
            var result = await _signInManager.PasswordSignInAsync(
                request.Email,
                request.Password,
                request.RememberMe,
                lockoutOnFailure: false);

            return result.Succeeded
                ? (true, null)
                : (false, "Invalid login attempt.");
        }

        // ================= REGISTER =================

        public async Task<(bool Success, IEnumerable<string> Errors)> RegisterAsync(RegisterRequest request)
        {
            var user = new ApplicationUser
            {
                FirstName = request.Name,
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = true,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description));

            if (!await _roleManager.RoleExistsAsync("User"))
                await _roleManager.CreateAsync(new IdentityRole("User"));

            await _userManager.AddToRoleAsync(user, "User");

            await _signInManager.SignInAsync(user, false);

            return (true, Enumerable.Empty<string>());
        }

        // ================= VERIFY EMAIL =================

        public async Task<(bool Success, string? Username)> VerifyEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return (false, null);

            return (true, user.UserName);
        }

        // ================= CHANGE PASSWORD =================

        public async Task<(bool Success, IEnumerable<string> Errors)> ChangePasswordAsync(ChangePasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                return (false, new[] { "User not found." });

            var removeResult = await _userManager.RemovePasswordAsync(user);
            if (!removeResult.Succeeded)
                return (false, removeResult.Errors.Select(e => e.Description));

            var addResult = await _userManager.AddPasswordAsync(user, request.NewPassword);
            if (!addResult.Succeeded)
                return (false, addResult.Errors.Select(e => e.Description));

            return (true, Enumerable.Empty<string>());
        }

        // ================= FIND USER ID =================

        public async Task<string?> FindUserIdByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user?.Id;
        }

        // ================= LOGOUT =================

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
