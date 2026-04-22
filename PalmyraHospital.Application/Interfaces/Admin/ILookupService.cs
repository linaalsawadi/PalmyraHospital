public interface ILookupService
{
    Task<List<(int Id, string Name)>> GetDepartmentsAsync();
    Task<List<(int Id, string Name)>> GetSpecializationsAsync();
    Task<List<(int Id, string Name)>> GetSpecializationsByDepartmentAsync(int departmentId);
}