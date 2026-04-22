using System.ComponentModel.DataAnnotations;

public class EditPatientViewModel
{
    public int Id { get; set; }

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    [Required(ErrorMessage = "Phone is required")]
    [RegularExpression(@"^05\d{8}$", ErrorMessage = "Phone must start with 05 and be 10 digits")]
    public string PhoneNumber { get; set; } = default!;

    [StringLength(200)]
    public string? Address { get; set; }
}