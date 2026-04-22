using PalmyraHospital.Application.DTOs.Admin;

namespace PalmyraHospital.Application.Interfaces.Admin;

public interface ISpecializationService
{
    Task<List<SpecializationDto>> GetAllAsync();
    Task<SpecializationDto?> GetByIdAsync(int id);

    Task CreateAsync(string name, int departmentId);
    Task UpdateAsync(int id, string name, int departmentId);
    Task DeleteAsync(int id);
}