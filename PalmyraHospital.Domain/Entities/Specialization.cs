using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalmyraHospital.Domain.Entities
{
    public class Specialization
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = default!;

        public ICollection<Doctor> Doctors { get; private set; } = new List<Doctor>();

        private Specialization() { }

        public Specialization(string name)
        {
            Name = name;
        }
    }
}
