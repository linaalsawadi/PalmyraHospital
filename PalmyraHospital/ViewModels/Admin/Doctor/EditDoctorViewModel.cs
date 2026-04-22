public class EditDoctorViewModel
{
    public int Id { get; set; }

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;

    public int DepartmentId { get; set; }
    public int SpecializationId { get; set; }

    public int YearsOfExperience { get; set; }
    public decimal ConsultationFee { get; set; }

    // 🔥 Dropdowns
    public List<(int Id, string Name)> Departments { get; set; } = new();
    public List<(int Id, string Name)> Specializations { get; set; } = new();
}