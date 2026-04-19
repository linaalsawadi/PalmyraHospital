using PalmyraHospital.Application.DTOs.Admin;

namespace PalmyraHospital.Application.Interfaces.Admin;

public interface IAdminDashboardService
{
    Task<AdminDashboardViewModel> GetDashboardAsync();
}
