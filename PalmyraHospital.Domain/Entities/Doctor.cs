using PalmyraHospital.Domain.Entities;
using System.ComponentModel.DataAnnotations;

public class Doctor
{
    public int Id { get; private set; }

    // Identity Link
    public string UserId { get; private set; } = default!;

    // Hospital Identifier
    public string DoctorNumber { get; private set; } = default!;

    public string LicenseNumber { get; private set; } = default!;

    // Personal Info
    [Required]
    [StringLength(50)]
    public string FirstName { get; private set; } = default!;

    [Required]
    [StringLength(50)]
    public string LastName { get; private set; } = default!;

    public string PhoneNumber { get; private set; } = default!;

    // Professional Info
    [Required(ErrorMessage = "Please select a department")]

    public int DepartmentId { get; private set; }

    [Required(ErrorMessage = "Please select a specialization")]

    public int SpecializationId { get; private set; }

    public int YearsOfExperience { get; private set; }

    public decimal ConsultationFee { get; private set; }

    public bool IsArchived { get; private set; }

    public DateTime CreatedAt { get; private set; }


    // Navigation Properties (Domain only)
    public Department Department { get; private set; } = default!;

    public Specialization Specialization { get; private set; } = default!;

    public ICollection<Appointment> Appointments { get; private set; }
        = new List<Appointment>();


    // Required by EF Core
    private Doctor() { }


    // Constructor
    public Doctor(
    string userId,
    string doctorNumber,
    string licenseNumber,
    string firstName,
    string lastName,
    string phoneNumber,
    int departmentId,
    int specializationId,
    int yearsOfExperience,
    decimal consultationFee)
    {
        UserId = userId;
        DoctorNumber = doctorNumber;
        LicenseNumber = licenseNumber;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        DepartmentId = departmentId;
        SpecializationId = specializationId;
        YearsOfExperience = yearsOfExperience;
        ConsultationFee = consultationFee;
        CreatedAt = DateTime.UtcNow;
        IsArchived = false;
    }


    public Doctor(string userId, string doctorNumber, string licenseNumber, string phoneNumber, int departmentId, int specializationId, int yearsOfExperience, decimal consultationFee)
    {
        UserId = userId;
        DoctorNumber = doctorNumber;
        LicenseNumber = licenseNumber;
        PhoneNumber = phoneNumber;
        DepartmentId = departmentId;
        SpecializationId = specializationId;
        YearsOfExperience = yearsOfExperience;
        ConsultationFee = consultationFee;
    }


    // Domain Methods
    public void UpdateConsultationFee(decimal newFee)
    {
        ConsultationFee = newFee;
    }

    public void UpdateExperience(int years)
    {
        YearsOfExperience = years;
    }

    public void Archive()
    {
        IsArchived = true;
    }

    public string GetFullName()
    {
        return $"{FirstName} {LastName}";
    }
}