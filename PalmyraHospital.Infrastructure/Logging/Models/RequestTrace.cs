using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PalmyraHospital.Infrastructure.Logging.Models;

public class RequestTrace
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(20)]
    public string Method { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Path { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? IpAddress { get; set; }

    public int StatusCode { get; set; }

    public long DurationMs { get; set; }

    public DateTime CreatedAt { get; set; }
}