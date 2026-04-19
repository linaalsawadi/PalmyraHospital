using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalmyraHospital.Domain.Constants
{
    public class Roles
    {
        public const string Admin = "Admin";
        public const string Doctor = "Doctor";
        public const string Patient = "Patient";

        public static readonly string[] All =
        {
        Admin,
        Doctor,
        Patient
    };
    }
}
