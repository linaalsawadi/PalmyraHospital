using PalmyraHospital.Application.DTOs.Admin;

namespace PalmyraHospital.Application.Interfaces.Admin;

public interface IDoctorService
{
    // =========================
    // READ
    // =========================
    Task<List<DoctorDto>> GetAllAsync();

    Task<DoctorDto?> GetByIdAsync(int id);
    Task<DoctorDto> GetForEditAsync(int id);
    Task UpdateAsync(
        int id,
        string firstName,
        string lastName,
        string phone,
        int ?departmentId,
        int ?specializationId,
        int years,
        decimal fee
    );
    // =========================
    // CREATE
    // =========================
    Task CreateAsync(string email, string password, string firstName, string lastName, string licenseNumber, string phoneNumber, int? departmentId, int? specializationId, int yearsOfExperience, decimal consultationFee);


    // =========================
    // DELETE
    // =========================
    Task DeleteAsync(int id);
}