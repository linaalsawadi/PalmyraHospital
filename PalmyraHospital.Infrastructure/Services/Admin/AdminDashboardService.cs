using Microsoft.EntityFrameworkCore;
using PalmyraHospital.Application.DTOs.Admin;
using PalmyraHospital.Application.Interfaces.Admin;
using PalmyraHospital.Infrastructure.Data;

namespace PalmyraHospital.Infrastructure.Services.Admin;

public class AdminDashboardService : IAdminDashboardService
{
    private readonly ApplicationDbContext _context;

    public AdminDashboardService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AdminDashboardViewModel> GetDashboardAsync()
    {
        return new AdminDashboardViewModel
        {
            TotalDoctors = await _context.Doctors.CountAsync(d => !d.IsArchived),
            ArchivedDoctors = await _context.Doctors.CountAsync(d => d.IsArchived),
            TotalPatients = await _context.Patients.CountAsync(),
            TotalDepartments = await _context.Departments.CountAsync(),
            TodayAppointments = await _context.Appointments
                .CountAsync(a => a.AppointmentDate.Date == DateTime.Today)
        };
    }
}
