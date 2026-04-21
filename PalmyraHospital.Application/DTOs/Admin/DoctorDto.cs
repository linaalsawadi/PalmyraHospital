namespace PalmyraHospital.Application.DTOs.Admin;

public class DoctorDto
{
    public int Id { get; set; }

    public string FullName { get; set; } = default!;

    public string Department { get; set; } = default!;

    public string Specialization { get; set; } = default!;

    public string Phone { get; set; } = default!;

    public int YearsOfExperience { get; set; }

    public decimal ConsultationFee { get; set; }
}