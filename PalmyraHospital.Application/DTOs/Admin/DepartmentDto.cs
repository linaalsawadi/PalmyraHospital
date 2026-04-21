namespace PalmyraHospital.Application.DTOs.Admin;

public class DepartmentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}