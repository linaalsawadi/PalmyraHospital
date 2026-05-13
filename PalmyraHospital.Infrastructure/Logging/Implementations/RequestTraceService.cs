using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PalmyraHospital.Infrastructure.Logging.Abstractions;
using PalmyraHospital.Infrastructure.Logging.Models;
using PalmyraHospital.Infrastructure.Logging.Repositories;

namespace PalmyraHospital.Infrastructure.Logging.Implementations;

public class RequestTraceService
    : IRequestTraceService
{
    private readonly IRequestTraceRepository _repository;

    public RequestTraceService(
        IRequestTraceRepository repository)
    {
        _repository = repository;
    }

    public async Task TraceAsync(
        HttpContext context,
        long durationMs)
    {
        var trace = new RequestTrace
        {
            Id = Guid.NewGuid(),

            Method = context.Request.Method,

            Path = context.Request.Path,

            IpAddress =
                context.Connection.RemoteIpAddress?.ToString(),

            StatusCode = context.Response.StatusCode,

            DurationMs = durationMs,

            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(trace);
    }
}