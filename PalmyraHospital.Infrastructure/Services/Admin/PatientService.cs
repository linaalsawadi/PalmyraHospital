using PalmyraHospital.Domain.Entities;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _repo;

    public PatientService(IPatientRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<PatientDto>> GetAllAsync()
    {
        var patients = await _repo.GetAllAsync();

        return patients.Select(p => new PatientDto
        {
            Id = p.Id,
            FullName = p.GetFullName(),
            Phone = p.PhoneNumber,
            DateOfBirth = p.DateOfBirth
        }).ToList();
    }

    public async Task<PatientDto?> GetByIdAsync(int id)
    {
        var p = await _repo.GetByIdAsync(id);
        if (p == null) return null;

        return new PatientDto
        {
            Id = p.Id,
            FullName = p.GetFullName(),
            Phone = p.PhoneNumber
        };
    }

    public async Task CreateAsync(
        string first,
        string last,
        DateTime dob,
        string gender,
        string nationalId,
        string phone,
        string? address)
    {
        var patient = new Patient(
            userId: Guid.NewGuid().ToString(),
            patientNumber: Guid.NewGuid().ToString(),
            first,
            last,
            dob,
            gender,
            nationalId,
            phone,
            address
        );

        await _repo.AddAsync(patient);
    }

    public async Task UpdateAsync(int id, string phone, string? address)
    {
        var patient = await _repo.GetByIdAsync(id)
            ?? throw new PatientNotFoundException();

        patient.UpdateContact(phone, address);

        await _repo.UpdateAsync(patient);
    }

    public async Task DeleteAsync(int id)
    {
        var patient = await _repo.GetByIdAsync(id)
            ?? throw new PatientNotFoundException();

        patient.Archive();

        await _repo.UpdateAsync(patient);
    }
}