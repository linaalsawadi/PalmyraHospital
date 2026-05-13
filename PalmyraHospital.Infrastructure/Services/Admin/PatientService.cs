using PalmyraHospital.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using PalmyraHospital.Infrastructure.Identity;
public class PatientService : IPatientService
{
    private readonly IPatientRepository _repo;
    private readonly UserManager<ApplicationUser> _userManager;
    public PatientService(IPatientRepository repo,UserManager<ApplicationUser> userManager)

    {
        _repo = repo;
        _userManager = userManager;

    }

    public async Task<List<PatientDto>> GetAllAsync()
    {
        var patients = await _repo.GetAllAsync();

        return patients.Select(p => new PatientDto
        {
            Id = p.Id,
            FullName = p.GetFullName(),
            Phone = p.PhoneNumber,
            DateOfBirth = p.DateOfBirth
        }).ToList();
    }

    public async Task<PatientDto?> GetByIdAsync(int id)
    {
        var p = await _repo.GetByIdAsync(id);
        if (p == null) return null;

        return new PatientDto
        {
            Id = p.Id,
            FullName = p.GetFullName(),
            Phone = p.PhoneNumber
        };
    }

    public async Task CreateAsync(
    string first,
    string last,
    DateTime dob,
    string gender,
    string nationalId,
    string phone,
    string? address)
{
    // ================= CREATE IDENTITY USER =================

    var user = new ApplicationUser
    {
        UserName = phone,
        Email = $"{nationalId}@patient.palmyra.com",
        FirstName = first,
        LastName = last,
        PhoneNumber = phone
    };

    var identityResult = await _userManager.CreateAsync(user, "Patient@123");

    if (!identityResult.Succeeded)
    {
        var errors = string.Join(" | ",
            identityResult.Errors.Select(e => e.Description));

        throw new Exception(errors);
    }

    // ================= GENERATE PATIENT NUMBER =================

    var patientNumber =
        $"PAT-{DateTime.UtcNow:yyyyMMdd}-{Random.Shared.Next(1000, 9999)}";

    // ================= CREATE PATIENT PROFILE =================

    var patient = new Patient(
        userId: user.Id,
        patientNumber: patientNumber,
        first,
        last,
        dob,
        gender,
        nationalId,
        phone,
        address
    );

    // ================= SAVE =================

    await _repo.AddAsync(patient);
}

    public async Task UpdateAsync(int id, string phone, string? address)
    {
        var patient = await _repo.GetByIdAsync(id)
            ?? throw new PatientNotFoundException();

        patient.UpdateContact(phone, address);

        await _repo.UpdateAsync(patient);
    }

    public async Task DeleteAsync(int id)
    {
        var patient = await _repo.GetByIdAsync(id)
            ?? throw new PatientNotFoundException();

        patient.Archive();

        await _repo.UpdateAsync(patient);
    }
}