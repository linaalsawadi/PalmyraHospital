using System.Data;
using System.Security.Claims;
using PalmyraHospital.Application.Interfaces.Redirect;
using PalmyraHospital.Domain.Constants;

namespace PalmyraHospital.Application.Implementations;

public class UserRedirectService : IUserRedirectService
{
    public (string controller, string action) GetRedirect(ClaimsPrincipal user)
    {
        if (user.IsInRole(Roles.Admin))
            return ("Admin", "Index");

        if (user.IsInRole(Roles.Doctor))
            return ("Doctor", "Index");

        if (user.IsInRole(Roles.Patient))
            return ("Patient", "Index");

        return ("Home", "Index");
    }
}
