using Microsoft.EntityFrameworkCore;
using PalmyraHospital.Domain.Entities;
using PalmyraHospital.Infrastructure.Data;

public class PatientRepository : IPatientRepository
{
    private readonly ApplicationDbContext _context;

    public PatientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Patient>> GetAllAsync()
        => await _context.Patients.ToListAsync();

    public async Task<Patient?> GetByIdAsync(int id)
        => await _context.Patients.FindAsync(id);

    public async Task AddAsync(Patient patient)
    {
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Patient patient)
    {
        _context.Patients.Update(patient);
        await _context.SaveChangesAsync();
    }
}