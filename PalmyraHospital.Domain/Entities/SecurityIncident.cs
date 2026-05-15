using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PalmyraHospital.Domain.Entities
{
    public class SecurityIncident
    {
        public int Id { get; set; }

        public string IncidentType { get; set; } = null!;

        public string Severity { get; set; } = "Medium";

        public string Description { get; set; } = null!;

        public string? Username { get; set; }

        public string? IpAddress { get; set; }

        public DateTime DetectedAt { get; set; } = DateTime.UtcNow;

        public string Status { get; set; } = "Open";

        public string? Resolution { get; set; }
    }
}