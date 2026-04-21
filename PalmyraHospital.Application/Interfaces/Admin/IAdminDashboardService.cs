using PalmyraHospital.Application.DTOs.Admin;

public interface IAdminDashboardService
{
    Task<AdminDashboardViewModel> GetDashboardAsync();
}