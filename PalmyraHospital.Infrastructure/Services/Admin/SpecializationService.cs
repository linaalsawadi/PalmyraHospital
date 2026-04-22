using PalmyraHospital.Application.DTOs.Admin;
using PalmyraHospital.Application.Interfaces.Admin;
using PalmyraHospital.Domain.Entities;

public class SpecializationService : ISpecializationService
{
    private readonly ISpecializationRepository _repo;

    public SpecializationService(ISpecializationRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<SpecializationDto>> GetAllAsync()
    {
        var data = await _repo.GetAllAsync();

        return data.Select(s => new SpecializationDto
        {
            Id = s.Id,
            Name = s.Name,
            DepartmentId = s.DepartmentId,
            DepartmentName = s.Department.Name
        }).ToList();
    }

    public async Task<SpecializationDto?> GetByIdAsync(int id)
    {
        var s = await _repo.GetByIdAsync(id);
        if (s == null) return null;

        return new SpecializationDto
        {
            Id = s.Id,
            Name = s.Name,
            DepartmentId = s.DepartmentId
        };
    }

    public async Task CreateAsync(string name, int departmentId)
    {
        var spec = new Specialization(name, departmentId);
        await _repo.AddAsync(spec);
    }

    public async Task UpdateAsync(int id, string name, int departmentId)
    {
        var spec = await _repo.GetByIdAsync(id)
            ?? throw new Exception("Not found");

        spec.UpdateName(name);
        spec.ChangeDepartment(departmentId);

        await _repo.UpdateAsync(spec);
    }

    public async Task DeleteAsync(int id)
    {
        var spec = await _repo.GetByIdAsync(id)
            ?? throw new Exception("Not found");

        await _repo.DeleteAsync(spec);
    }
}