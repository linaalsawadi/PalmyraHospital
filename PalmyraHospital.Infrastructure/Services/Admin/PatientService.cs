using Microsoft.EntityFrameworkCore;
using PalmyraHospital.Application.DTOs.Admin;
using PalmyraHospital.Application.Exceptions;
using PalmyraHospital.Application.Interfaces;
using PalmyraHospital.Domain.Entities;
using PalmyraHospital.Infrastructure.Data;

public class PatientService : IPatientService
{
    private readonly ApplicationDbContext _context;

    public PatientService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<PatientDto>> GetAllAsync()
    {
        return await _context.Patients
            .Select(p => new PatientDto
            {
                Id = p.Id,
                FullName = p.FirstName + " " + p.LastName,
                Phone = p.PhoneNumber,
                DateOfBirth = p.DateOfBirth
            })
            .ToListAsync();
    }

    public async Task<PatientDto?> GetByIdAsync(int id)
    {
        var p = await _context.Patients.FindAsync(id);
        if (p == null) return null;

        return new PatientDto
        {
            Id = p.Id,
            FullName = p.FirstName + " " + p.LastName,
            Phone = p.PhoneNumber,
            DateOfBirth = p.DateOfBirth
        };
    }

    public async Task CreateAsync(
        string firstName,
        string lastName,
        DateTime dob,
        string gender,
        string nationalId,
        string phone,
        string? address)
    {
        // مبدئيًا بدون Identity (مثل ما عملنا في Doctor لاحقًا)
        var patient = new Patient(
            userId: Guid.NewGuid().ToString(),
            patientNumber: Guid.NewGuid().ToString(),
            firstName,
            lastName,
            dob,
            gender,
            nationalId,
            phone,
            address
        );

        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, string phone, string? address)
    {
        var patient = await _context.Patients.FindAsync(id)
            ?? throw new PatientNotFoundException();

        patient.UpdateContact(phone, address);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var patient = await _context.Patients.FindAsync(id)
            ?? throw new PatientNotFoundException();

        patient.Archive(); // soft delete
        await _context.SaveChangesAsync();
    }
}