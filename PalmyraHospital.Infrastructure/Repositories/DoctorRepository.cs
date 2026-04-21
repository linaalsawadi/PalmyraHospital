using Microsoft.EntityFrameworkCore;
using PalmyraHospital.Application.Interfaces.Admin;
using PalmyraHospital.Domain.Entities;
using PalmyraHospital.Infrastructure.Data;

namespace PalmyraHospital.Infrastructure.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly ApplicationDbContext _context;

    public DoctorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Doctor>> GetAllAsync()
        => await _context.Doctors
            .AsNoTracking()
            .Include(d => d.Department)
            .Include(d => d.Specialization)
            .ToListAsync();

    public async Task<Doctor?> GetByIdAsync(int id)
        => await _context.Doctors
            .Include(d => d.Department)
            .Include(d => d.Specialization)
            .FirstOrDefaultAsync(d => d.Id == id);

    public async Task AddAsync(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Doctor doctor)
    {
        _context.Doctors.Update(doctor);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Doctor doctor)
    {
        _context.Doctors.Remove(doctor);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByLicenseAsync(string licenseNumber)
        => await _context.Doctors
            .AnyAsync(d => d.LicenseNumber == licenseNumber);
}