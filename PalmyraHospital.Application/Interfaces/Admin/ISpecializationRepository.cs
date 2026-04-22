using PalmyraHospital.Domain.Entities;

public interface ISpecializationRepository
{
    Task<List<Specialization>> GetAllAsync();
    Task<Specialization?> GetByIdAsync(int id);

    Task AddAsync(Specialization specialization);
    Task UpdateAsync(Specialization specialization);
    Task DeleteAsync(Specialization specialization);
}