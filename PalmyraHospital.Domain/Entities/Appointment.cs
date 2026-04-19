using System.ComponentModel.DataAnnotations;

namespace PalmyraHospital.Domain.Entities;

public class Appointment
{
    public int Id { get; private set; }


    [Required]
    public DateTime AppointmentDate { get; private set; }


    [Required]
    [Range(1, 600)]
    public int DurationMinutes { get; private set; }


    [Required]
    [StringLength(20)]
    public string Status { get; private set; } = default!;


    [StringLength(500)]
    public string? Notes { get; private set; }


    // Foreign Keys
    [Required]
    public int DoctorId { get; private set; }

    [Required]
    public int PatientId { get; private set; }


    // Audit
    public DateTime CreatedAt { get; private set; }


    // Navigation Properties
    public Doctor Doctor { get; private set; } = default!;

    public Patient Patient { get; private set; } = default!;


    // Required by EF Core
    private Appointment() { }


    // Constructor
    public Appointment(
        int doctorId,
        int patientId,
        DateTime appointmentDate,
        int durationMinutes,
        string? notes = null)
    {
        DoctorId = doctorId;
        PatientId = patientId;
        AppointmentDate = appointmentDate;
        DurationMinutes = durationMinutes;
        Notes = notes;

        Status = "Scheduled";
        CreatedAt = DateTime.UtcNow;
    }


    // Domain Methods
    public void MarkCompleted()
    {
        Status = "Completed";
    }

    public void Cancel()
    {
        Status = "Cancelled";
    }

    public void Reschedule(DateTime newDate)
    {
        AppointmentDate = newDate;
    }

    public void UpdateNotes(string? notes)
    {
        Notes = notes;
    }
}
