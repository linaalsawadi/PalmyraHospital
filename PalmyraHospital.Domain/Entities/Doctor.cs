using System.ComponentModel.DataAnnotations;

namespace PalmyraHospital.Domain.Entities;

public class Doctor
{
    public int Id { get; private set; }

    public string UserId { get; private set; } = default!;

    public string DoctorNumber { get; private set; } = default!;

    public string LicenseNumber { get; private set; } = default!;

    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;

    public string PhoneNumber { get; private set; } = default!;

    public int DepartmentId { get; private set; }
    public int SpecializationId { get; private set; }

    public int YearsOfExperience { get; private set; }

    public decimal ConsultationFee { get; private set; }

    public bool IsArchived { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Department Department { get; private set; } = default!;
    public Specialization Specialization { get; private set; } = default!;

    public ICollection<Appointment> Appointments { get; private set; }
        = new List<Appointment>();

    private Doctor() { }

    //  Constructor واحد فقط (Correct)
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
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required");

        if (string.IsNullOrWhiteSpace(licenseNumber))
            throw new ArgumentException("License is required");

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

    // =========================
    // Domain Methods
    // =========================

    public void UpdateInfo(
        string firstName,
        string lastName,
        string phoneNumber,
        int departmentId,
        int specializationId)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required");

        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        DepartmentId = departmentId;
        SpecializationId = specializationId;
    }

    public void UpdateConsultationFee(decimal newFee)
    {
        if (newFee < 0)
            throw new ArgumentException("Invalid fee");

        ConsultationFee = newFee;
    }

    public void UpdateExperience(int years)
    {
        if (years < 0)
            throw new ArgumentException("Invalid years");

        YearsOfExperience = years;
    }
    public void UpdateBasicInfo(string firstName, string lastName, string phone)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phone;
    }

    public void ChangeDepartment(int departmentId)
    {
        DepartmentId = departmentId;
    }

    public void ChangeSpecialization(int specializationId)
    {
        SpecializationId = specializationId;
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