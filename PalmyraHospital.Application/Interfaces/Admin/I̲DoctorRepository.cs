using PalmyraHospital.Domain.Entities;

namespace PalmyraHospital.Application.Interfaces.Admin;

public interface IDoctorRepository
{
    Task<List<Doctor>> GetAllAsync();

    Task<Doctor?> GetByIdAsync(int id);

    Task AddAsync(Doctor doctor);

    Task UpdateAsync(Doctor doctor);

    Task DeleteAsync(Doctor doctor);

    Task<bool> ExistsByLicenseAsync(string licenseNumber);
}