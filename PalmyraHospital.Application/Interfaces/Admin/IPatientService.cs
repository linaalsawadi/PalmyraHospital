using PalmyraHospital.Application.DTOs.Admin;

public interface IPatientService
{
    Task<List<PatientDto>> GetAllAsync();
    Task<PatientDto?> GetByIdAsync(int id);

    Task CreateAsync(
        string firstName,
        string lastName,
        DateTime dob,
        string gender,
        string nationalId,
        string phone,
        string? address
    );

    Task UpdateAsync(
        int id,
        string phone,
        string? address
    );

    Task DeleteAsync(int id);
}