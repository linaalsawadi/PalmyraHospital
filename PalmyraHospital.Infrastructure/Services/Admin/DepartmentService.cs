using PalmyraHospital.Application.DTOs.Admin;
using PalmyraHospital.Application.Interfaces.Admin;
using PalmyraHospital.Domain.Entities;

namespace PalmyraHospital.Infrastructure.Services.Admin;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _repo;

    public DepartmentService(IDepartmentRepository repo)
    {
        _repo = repo;
    }

    // =========================
    // GET ALL
    // =========================
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
    // =========================
    // GetByIdAsync
    // =========================
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
    // =========================
    // GET FOR EDIT
    // =========================
    public async Task<DepartmentDto> GetForEditAsync(int id)
    {
        var dept = await _repo.GetByIdAsync(id);

        if (dept == null)
            throw new Exception("Department not found");

        return new DepartmentDto
        {
            Id = dept.Id,
            Name = dept.Name,
            Description = dept.Description
        };
    }

    // =========================
    // CREATE
    // =========================
    public async Task CreateAsync(string name, string? description)
    {
        if (await _repo.ExistsByNameAsync(name))
            throw new Exception("Department already exists");

        var department = new Department(name, description);

        await _repo.AddAsync(department);
    }

    // =========================
    // UPDATE
    // =========================
    public async Task UpdateAsync(int id, string name, string? description)
    {
        var department = await _repo.GetByIdAsync(id);

        if (department == null)
            throw new Exception("Department not found");

        if (await _repo.ExistsByNameAsync(name))
            throw new Exception("Department name already exists");

        department.Update(name, description);

        await _repo.UpdateAsync(department);
    }

    // =========================
    // DELETE
    // =========================
    public async Task DeleteAsync(int id)
    {
        var department = await _repo.GetByIdAsync(id);

        if (department == null)
            throw new Exception("Department not found");

        await _repo.DeleteAsync(department);
    }
}