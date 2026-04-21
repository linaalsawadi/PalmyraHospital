using PalmyraHospital.Application.DTOs.Admin;

namespace PalmyraHospital.Application.Interfaces.Admin;

public interface IDoctorService
{
    // =========================
    // READ
    // =========================
    Task<List<DoctorDto>> GetAllAsync();

    Task<DoctorDto?> GetByIdAsync(int id);

    // =========================
    // CREATE
    // =========================
    Task CreateAsync(
        string email,
        string password,
        string firstName,
        string lastName,
        string licenseNumber,
        string phone,
        int departmentId,
        int specializationId,
        int years,
        decimal fee
    );

    // =========================
    // DELETE
    // =========================
    Task DeleteAsync(int id);
}