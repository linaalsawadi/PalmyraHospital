using PalmyraHospital.Infrastructure.Data;
using PalmyraHospital.Infrastructure.Logging.Models;
using System;

namespace PalmyraHospital.Infrastructure.Logging.Repositories;

public class SecurityLogRepository : ISecurityLogRepository
{
    private readonly ApplicationDbContext _context;

    public SecurityLogRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(SecurityLog log)
    {
        await _context.SecurityLogs.AddAsync(log);

        await _context.SaveChangesAsync();
    }
}