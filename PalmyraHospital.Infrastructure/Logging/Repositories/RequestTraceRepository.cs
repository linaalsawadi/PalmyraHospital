using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalmyraHospital.Infrastructure.Data;
using PalmyraHospital.Infrastructure.Logging.Models;

namespace PalmyraHospital.Infrastructure.Logging.Repositories;

public class RequestTraceRepository
    : IRequestTraceRepository
{
    private readonly ApplicationDbContext _context;

    public RequestTraceRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(RequestTrace trace)
    {
        await _context.RequestTraces.AddAsync(trace);

        await _context.SaveChangesAsync();
    }
}