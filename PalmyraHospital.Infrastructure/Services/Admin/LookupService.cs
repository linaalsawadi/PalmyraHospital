using Microsoft.EntityFrameworkCore;
using PalmyraHospital.Application.Interfaces;
using PalmyraHospital.Infrastructure.Data;

public class LookupService : ILookupService
{
    private readonly ApplicationDbContext _context;

    public LookupService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<(int Id, string Name)>> GetDepartmentsAsync()
        => await _context.Departments
            .Select(d => new ValueTuple<int, string>(d.Id, d.Name))
            .ToListAsync();

    public async Task<List<(int Id, string Name)>> GetSpecializationsAsync()
        => await _context.Specializations
            .Select(s => new ValueTuple<int, string>(s.Id, s.Name))
            .ToListAsync();
    public async Task<List<(int Id, string Name)>> GetSpecializationsByDepartmentAsync(int departmentId)
    {
        return await _context.Specializations
            .Where(s => s.DepartmentId == departmentId)
            .Select(s => new ValueTuple<int, string>(s.Id, s.Name))
            .ToListAsync();
    }
}