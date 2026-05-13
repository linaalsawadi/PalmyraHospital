using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalmyraHospital.Infrastructure.Logging.Abstractions;
using PalmyraHospital.Infrastructure.Logging.Enums;
using PalmyraHospital.Infrastructure.Logging.Models;
using PalmyraHospital.Infrastructure.Logging.Repositories;

namespace PalmyraHospital.Infrastructure.Logging.Implementations;

public class SecurityLogger : ISecurityLogger
{
    private readonly ISecurityLogRepository _repository;

    public SecurityLogger(
        ISecurityLogRepository repository)
    {
        _repository = repository;
    }

    public async Task LogSecurityEventAsync(
        string message,
        SecurityEventType eventType,
        LogSeverity severity,
        string? userId = null,
        string? ipAddress = null)
    {
        var log = new SecurityLog
        {
            Id = Guid.NewGuid(),
            Message = message,
            UserId = userId,
            IpAddress = ipAddress,
            EventType = eventType,
            Severity = severity,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(log);
    }
}