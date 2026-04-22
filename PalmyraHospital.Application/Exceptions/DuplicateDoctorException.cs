using PalmyraHospital.Application.Exceptions;

public class DuplicateDoctorException : BaseException
{
    public DuplicateDoctorException()
        : base("Doctor already exists", 400)
    {
    }
}