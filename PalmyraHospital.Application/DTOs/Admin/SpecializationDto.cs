namespace PalmyraHospital.Application.DTOs.Admin;

public class SpecializationDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = default!;
}