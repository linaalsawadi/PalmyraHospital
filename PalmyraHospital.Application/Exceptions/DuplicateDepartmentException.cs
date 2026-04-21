namespace PalmyraHospital.Application.Exceptions;

public class DuplicateDepartmentException : BaseException
{
    public DuplicateDepartmentException()
        : base("Department already exists", 400)
    {
    }
}