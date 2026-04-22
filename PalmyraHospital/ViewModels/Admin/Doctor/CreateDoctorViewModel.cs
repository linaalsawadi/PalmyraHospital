using System.ComponentModel.DataAnnotations;

namespace PalmyraHospital.Web.ViewModels.Admin.Doctor
{
    public class CreateDoctorViewModel
    {
        // Identity
        [Required, EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = default!;

        // Doctor entity
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only letters allowed.")]
        public string FirstName { get; set; } = default!;

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only letters allowed.")]
        public string LastName { get; set; } = default!;

        [Required]
        [StringLength(50)]
        public string LicenseNumber { get; set; } = default!;

        //[Required]
        //[RegularExpression(@"^05\d{9}$", ErrorMessage = "Phone must start with 05 and contain 10 digits")]
        public string PhoneNumber { get; set; } = default!;

        [Required]
        public int? DepartmentId { get; set; }

        [Required]
        public int? SpecializationId { get; set; }

        [Range(0, 80)]
        public int YearsOfExperience { get; set; }

        [Range(0, 1000000)]
        public decimal ConsultationFee { get; set; }

        // For dropdowns
        public List<(int Id, string Name)> Departments { get; set; } = new();
        public List<(int Id, string Name)> Specializations { get; set; } = new();
    }
}
