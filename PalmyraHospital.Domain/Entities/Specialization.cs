using System;
using System.Collections.Generic;

namespace PalmyraHospital.Domain.Entities
{
    public class Specialization
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = default!;

        //  Foreign Key
        public int DepartmentId { get; private set; }

        //  Navigation
        public Department Department { get; private set; } = default!;

        public ICollection<Doctor> Doctors { get; private set; } = new List<Doctor>();

        private Specialization() { } // EF Core

        //  Constructor الصحيح
        public Specialization(string name, int departmentId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Specialization name is required");

            Name = name;
            DepartmentId = departmentId;
        }

        //  Domain Method
        public void ChangeDepartment(int departmentId)
        {
            DepartmentId = departmentId;
        }

        //  Domain Method
        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Specialization name is required");

            Name = name;
        }
    }
}