using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalmyraHospital.Infrastructure.Logging.Enums;

namespace PalmyraHospital.Infrastructure.Logging.Models;

public class AuditLog
{
    public Guid Id { get; set; }

    public string EntityName { get; set; } = string.Empty;

    public string Action { get; set; } = string.Empty;

    public string? UserId { get; set; }

    public AuditActionType ActionType { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}