namespace PalmyraHospital.Application.Exceptions;

public class DepartmentNotFoundException : BaseException
{
    public DepartmentNotFoundException()
        : base("Department not found", 404)
    {
    }
}