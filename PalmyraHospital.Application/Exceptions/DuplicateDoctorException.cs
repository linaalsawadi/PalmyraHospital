public class DuplicateDoctorException : Exception
{
    public DuplicateDoctorException()
        : base("Doctor already exists") { }
}