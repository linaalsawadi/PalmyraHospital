public class PatientDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public DateTime DateOfBirth { get; set; }
}