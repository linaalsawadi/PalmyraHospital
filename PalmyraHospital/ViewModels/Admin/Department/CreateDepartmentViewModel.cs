using System.ComponentModel.DataAnnotations;

namespace PalmyraHospital.Web.ViewModels.Admin.Department;

public class CreateDepartmentViewModel
{
    [Required]
    public string Name { get; set; } = default!;

    public string? Description { get; set; }
}