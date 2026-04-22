using Microsoft.AspNetCore.Identity;
using PalmyraHospital.Application.DTOs.Admin;
using PalmyraHospital.Application.Exceptions;
using PalmyraHospital.Application.Interfaces.Admin;
using PalmyraHospital.Domain.Entities;
using PalmyraHospital.Infrastructure.Identity;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _repo;
    private readonly ISpecializationRepository _specRepo;
    private readonly UserManager<ApplicationUser> _userManager;

    public DoctorService(
        IDoctorRepository repo,
        ISpecializationRepository specRepo,
        UserManager<ApplicationUser> userManager)
    {
        _repo = repo;
        _specRepo = specRepo;
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
            Department = d.Department?.Name ?? "",
            Specialization = d.Specialization?.Name ?? "",
            Phone = d.PhoneNumber,
            YearsOfExperience = d.YearsOfExperience,
            ConsultationFee = d.ConsultationFee
        }).ToList();
    }

    // =========================
    // GET BY ID
    // =========================
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
    // GET FOR EDIT
    // =========================
    public async Task<DoctorDto> GetForEditAsync(int id)
    {
        var doctor = await _repo.GetByIdAsync(id)
            ?? throw new DoctorNotFoundException();

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
    // UPDATE
    // =========================
    public async Task UpdateAsync(
        int id,
        string firstName,
        string lastName,
        string phone,
        int? departmentId,
        int? specializationId,
        int years,
        decimal fee)
    {
        var doctor = await _repo.GetByIdAsync(id)
            ?? throw new DoctorNotFoundException();

        // =========================
        // Validation
        // =========================
        if (departmentId == null || specializationId == null)
            throw new Exception("Department and Specialization are required");

        if (years < 0)
            throw new Exception("Years cannot be negative");

        if (fee < 0)
            throw new Exception("Fee cannot be negative");

        // 🔥 Validate specialization belongs to department
        var specialization = await _specRepo.GetByIdAsync(specializationId.Value);

        if (specialization == null)
            throw new Exception("Specialization not found");

        if (specialization.DepartmentId != departmentId.Value)
            throw new Exception("Invalid specialization for selected department");

        // =========================
        // Domain Update
        // =========================
        doctor.UpdateBasicInfo(firstName, lastName, phone);
        doctor.ChangeDepartment(departmentId.Value);
        doctor.ChangeSpecialization(specializationId.Value);
        doctor.UpdateExperience(years);
        doctor.UpdateConsultationFee(fee);

        await _repo.UpdateAsync(doctor);
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
        int? departmentId,
        int? specializationId,
        int years,
        decimal fee)
    {
        // =========================
        // Validation
        // =========================
        if (departmentId == null || specializationId == null)
            throw new Exception("Department and Specialization are required");

        if (years < 0)
            throw new Exception("Years of experience cannot be negative");

        if (fee < 0)
            throw new Exception("Consultation fee cannot be negative");

        if (string.IsNullOrWhiteSpace(email))
            throw new Exception("Email is required");

        if (string.IsNullOrWhiteSpace(password))
            throw new Exception("Password is required");

        if (await _repo.ExistsByLicenseAsync(licenseNumber))
            throw new DuplicateDoctorException();

        // 🔥 Validate specialization
        var specialization = await _specRepo.GetByIdAsync(specializationId.Value);

        if (specialization == null)
            throw new Exception("Specialization not found");

        if (specialization.DepartmentId != departmentId.Value)
            throw new Exception("Invalid specialization for selected department");

        // =========================
        // Create Identity User
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
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

        await _userManager.AddToRoleAsync(user, "Doctor");

        // =========================
        // Create Domain Doctor
        // =========================
        var doctor = new Doctor(
            user.Id,
            GenerateDoctorNumber(),
            licenseNumber,
            firstName,
            lastName,
            phone,
            departmentId.Value,
            specializationId.Value,
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
        var doctor = await _repo.GetByIdAsync(id)
            ?? throw new DoctorNotFoundException();

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