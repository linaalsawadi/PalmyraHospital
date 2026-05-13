using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalmyraHospital.Infrastructure.Logging.Enums;

namespace PalmyraHospital.Infrastructure.Logging.Abstractions;

public interface ISecurityLogger
{
    Task LogSecurityEventAsync(
        string message,
        SecurityEventType eventType,
        LogSeverity severity,
        string? userId = null,
        string? ipAddress = null);
}