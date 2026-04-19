using System.Security.Claims;

namespace PalmyraHospital.Application.Interfaces.Redirect;

public interface IUserRedirectService
{
    (string controller, string action) GetRedirect(ClaimsPrincipal user);
}
