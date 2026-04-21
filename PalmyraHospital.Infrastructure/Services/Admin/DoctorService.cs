using Microsoft.AspNetCore.Identity;
using PalmyraHospital.Application.DTOs.Admin;
using PalmyraHospital.Application.Exceptions;
using PalmyraHospital.Application.Interfaces.Admin;
using PalmyraHospital.Domain.Entities;
using PalmyraHospital.Infrastructure.Identity;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _repo;
    private readonly UserManager<ApplicationUser> _userManager;

    public DoctorService(
        IDoctorRepository repo,
        UserManager<ApplicationUser> userManager)
    {
        _repo = repo;
        _userManager = userManager;
    }

    // =========================
    // GET ALL
    // =========================
    public async Task<List<DoctorDto>> GetAllAsync()
    {
        var doctors = await _repo.GetAllAsync();

        return doctors.Select(d => new DoctorDto
        {
            Id = d.Id,
            FullName = d.GetFullName(),
            Department = d.Department.Name,
            Specialization = d.Specialization.Name,
            Phone = d.PhoneNumber
        }).ToList();
    }
    public async Task<DoctorDto?> GetByIdAsync(int id)
    {
        var doctor = await _repo.GetByIdAsync(id);

        if (doctor == null)
            return null;

        return new DoctorDto
        {
            Id = doctor.Id,
            FullName = doctor.GetFullName(),
            Department = doctor.Department?.Name ?? "",
            Specialization = doctor.Specialization?.Name ?? "",
            Phone = doctor.PhoneNumber,
            YearsOfExperience = doctor.YearsOfExperience,
            ConsultationFee = doctor.ConsultationFee
        };
    }
    // =========================
    // CREATE 
    // =========================
    public async Task CreateAsync(
        string email,
        string password,
        string firstName,
        string lastName,
        string licenseNumber,
        string phone,
        int departmentId,
        int specializationId,
        int years,
        decimal fee)
    {
        //  Validation
        if (await _repo.ExistsByLicenseAsync(licenseNumber))
            throw new DuplicateDoctorException();

        // =========================
        // 1. Create Identity User
        // =========================
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phone,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            throw new Exception(
                string.Join(", ", result.Errors.Select(e => e.Description)));

        // =========================
        // 2. Assign Role
        // =========================
        await _userManager.AddToRoleAsync(user, "Doctor");

        // =========================
        // 3. Create Domain Doctor
        // =========================
        var doctor = new Doctor(
            user.Id,
            GenerateDoctorNumber(),
            licenseNumber,
            firstName,
            lastName,
            phone,
            departmentId,
            specializationId,
            years,
            fee
        );

        await _repo.AddAsync(doctor);
    }

    // =========================
    // DELETE
    // =========================
    public async Task DeleteAsync(int id)
    {
        var doctor = await _repo.GetByIdAsync(id);

        if (doctor == null)
            throw new DoctorNotFoundException();

        await _repo.DeleteAsync(doctor);
    }

    // =========================
    // PRIVATE HELPERS
    // =========================
    private string GenerateDoctorNumber()
    {
        return $"DOC-{Guid.NewGuid().ToString().Substring(0, 6)}";
    }
}