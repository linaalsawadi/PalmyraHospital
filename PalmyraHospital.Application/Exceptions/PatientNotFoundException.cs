using PalmyraHospital.Application.Exceptions;

public class PatientNotFoundException : BaseException
{
    public PatientNotFoundException()
        : base("Patient not found", 404) { }
}