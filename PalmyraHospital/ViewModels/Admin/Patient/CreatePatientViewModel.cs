using System.ComponentModel.DataAnnotations;

public class CreatePatientViewModel
{
    [Required(ErrorMessage = "First name is required")]
    [StringLength(50)]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only letters allowed")]
    public string FirstName { get; set; } = default!;

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50)]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only letters allowed")]
    public string LastName { get; set; } = default!;

    [Required(ErrorMessage = "Date of birth is required")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "Gender is required")]
    [RegularExpression(@"^(Male|Female)$", ErrorMessage = "Invalid gender")]
    public string Gender { get; set; } = default!;

    [Required(ErrorMessage = "National ID is required")]
    [RegularExpression(@"^\d{10,15}$", ErrorMessage = "Invalid National ID")]
    public string NationalId { get; set; } = default!;

    [Required(ErrorMessage = "Phone number is required")]
    [RegularExpression(@"^05\d{8}$", ErrorMessage = "Phone must start with 05 and be 10 digits")]
    public string PhoneNumber { get; set; } = default!;

    [StringLength(200)]
    public string? Address { get; set; }
}