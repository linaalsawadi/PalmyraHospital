using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalmyraHospital.Application.DTOs.Auth;


namespace PalmyraHospital.Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<(bool Success, string? Error)> LoginAsync(LoginRequest request);

        Task<(bool Success, IEnumerable<string> Errors)> RegisterAsync(RegisterRequest request);
        Task<(bool Success, string? Username)> VerifyEmailAsync(string email);

        Task<(bool Success, IEnumerable<string> Errors)> ChangePasswordAsync(ChangePasswordRequest request);

        Task LogoutAsync();

        Task<string?> FindUserIdByEmailAsync(string email);
    }

}
