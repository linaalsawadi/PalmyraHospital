using PalmyraHospital.Application.DTOs.Admin;

namespace PalmyraHospital.Application.Interfaces.Admin;

public interface IDepartmentService
{
    Task<List<DepartmentDto>> GetAllAsync();

    Task<DepartmentDto?> GetByIdAsync(int id);
    Task<DepartmentDto> GetForEditAsync(int id);
    Task CreateAsync(string name, string? description);

    Task UpdateAsync(int id, string name, string? description);

    Task DeleteAsync(int id);
}