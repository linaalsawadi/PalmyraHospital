using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using PalmyraHospital.Infrastructure.Logging.Enums;

namespace PalmyraHospital.Infrastructure.Logging.Models;

public class SecurityLog
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(500)]
    public string Message { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? UserId { get; set; }

    [MaxLength(100)]
    public string? IpAddress { get; set; }

    public SecurityEventType EventType { get; set; }

    public LogSeverity Severity { get; set; }

    public DateTime CreatedAt { get; set; }
}