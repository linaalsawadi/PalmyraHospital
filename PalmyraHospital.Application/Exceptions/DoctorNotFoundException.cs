public class DoctorNotFoundException : Exception
{
    public DoctorNotFoundException()
        : base("Doctor not found") { }
}