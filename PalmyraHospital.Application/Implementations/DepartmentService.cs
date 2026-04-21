using PalmyraHospital.Application.Interfaces.Admin;
using PalmyraHospital.Application.DTOs.Admin;
using PalmyraHospital.Domain.Entities;
using PalmyraHospital.Application.Exceptions;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _repo;

    public DepartmentService(IDepartmentRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<DepartmentDto>> GetAllAsync()
    {
        var departments = await _repo.GetAllAsync();

        return departments.Select(d => new DepartmentDto
        {
            Id = d.Id,
            Name = d.Name,
            Description = d.Description
        }).ToList();
    }

    public async Task<DepartmentDto?> GetByIdAsync(int id)
    {
        var dept = await _repo.GetByIdAsync(id);

        if (dept == null) return null;

        return new DepartmentDto
        {
            Id = dept.Id,
            Name = dept.Name,
            Description = dept.Description
        };
    }

    public async Task<DepartmentDto> GetForEditAsync(int id)
    {
        var dept = await _repo.GetByIdAsync(id)
            ?? throw new DepartmentNotFoundException();

        return new DepartmentDto
        {
            Id = dept.Id,
            Name = dept.Name,
            Description = dept.Description
        };
    }

    public async Task CreateAsync(string name, string? description)
    {
        if (await _repo.ExistsByNameAsync(name))
            throw new DuplicateDepartmentException();

        var department = new Department(name, description);

        await _repo.AddAsync(department);
    }

    public async Task UpdateAsync(int id, string name, string? description)
    {
        var dept = await _repo.GetByIdAsync(id)
            ?? throw new DepartmentNotFoundException();

        dept.Update(name, description);

        await _repo.UpdateAsync(dept);
    }

    public async Task DeleteAsync(int id)
    {
        var dept = await _repo.GetByIdAsync(id)
            ?? throw new DepartmentNotFoundException();

        await _repo.DeleteAsync(dept);
    }
}