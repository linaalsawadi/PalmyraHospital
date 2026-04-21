using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalmyraHospital.Domain.Entities
{
    public class Department
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string? Description { get; private set; }

        public DateTime CreatedAt { get; private set; }

        // Navigation
        public ICollection<Doctor> Doctors { get; private set; } = new List<Doctor>();

        private Department() { } // EF Core

        public Department(string name, string? description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Department name is required");

            Name = name;
            Description = description;
            CreatedAt = DateTime.UtcNow;
        }
        public void Update(string name, string? description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required");

            Name = name;
            Description = description;
        }
    }
}
