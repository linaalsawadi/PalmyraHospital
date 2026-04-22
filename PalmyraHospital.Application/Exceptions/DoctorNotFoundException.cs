using PalmyraHospital.Application.Exceptions;

public class DoctorNotFoundException : BaseException
{
    public DoctorNotFoundException()
        : base("Doctor not found", 404)
    {
    }
}