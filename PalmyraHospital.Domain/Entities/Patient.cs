using System.ComponentModel.DataAnnotations;

namespace PalmyraHospital.Domain.Entities;

public class Patient
{
    public int Id { get; private set; }


    // Identity Link
    [Required]
    public string UserId { get; private set; } = default!;


    // Hospital Identifier
    [Required]
    [StringLength(50)]
    public string PatientNumber { get; private set; } = default!;


    // Personal Info

    [Required]
    [StringLength(50)]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only letters allowed.")]
    public string FirstName { get; private set; } = default!;


    [Required]
    [StringLength(50)]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only letters allowed.")]
    public string LastName { get; private set; } = default!;


    [Required]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; private set; }


    [Required]
    [RegularExpression(@"^(Male|Female)$", ErrorMessage = "Gender must be Male or Female.")]
    public string Gender { get; private set; } = default!;


    [Required]
    [StringLength(15)]
    [RegularExpression(@"^\d{10,15}$", ErrorMessage = "Invalid National ID.")]
    public string NationalId { get; private set; } = default!;


    // Contact

    [Required]
    [RegularExpression(@"^05\d{8}$", ErrorMessage = "Phone must start with 05 and contain 10 digits")]
    public string PhoneNumber { get; private set; } = default!;


    [StringLength(200)]
    public string? Address { get; private set; }


    // System

    public bool IsArchived { get; private set; }

    public DateTime CreatedAt { get; private set; }


    // Navigation

    public ICollection<Appointment> Appointments { get; private set; }
        = new List<Appointment>();


    // Required by EF Core
    private Patient() { }


    // Constructor
    public Patient(
        string userId,
        string patientNumber,
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        string gender,
        string nationalId,
        string phoneNumber,
        string? address)
    {
        UserId = userId;
        PatientNumber = patientNumber;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        NationalId = nationalId;
        PhoneNumber = phoneNumber;
        Address = address;

        CreatedAt = DateTime.UtcNow;
        IsArchived = false;
    }


    // Domain Methods

    public void UpdateContact(string phoneNumber, string? address)
    {
        PhoneNumber = phoneNumber;
        Address = address;
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
