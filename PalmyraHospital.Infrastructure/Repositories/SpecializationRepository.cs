using Microsoft.EntityFrameworkCore;
using PalmyraHospital.Domain.Entities;
using PalmyraHospital.Infrastructure.Data;

public class SpecializationRepository : ISpecializationRepository
{
    private readonly ApplicationDbContext _context;

    public SpecializationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Specialization>> GetAllAsync()
        => await _context.Specializations
            .Include(s => s.Department)
            .ToListAsync();

    public async Task<Specialization?> GetByIdAsync(int id)
        => await _context.Specializations.FindAsync(id);

    public async Task AddAsync(Specialization specialization)
    {
        _context.Specializations.Add(specialization);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Specialization specialization)
    {
        _context.Specializations.Update(specialization);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Specialization specialization)
    {
        _context.Specializations.Remove(specialization);
        await _context.SaveChangesAsync();
    }
}