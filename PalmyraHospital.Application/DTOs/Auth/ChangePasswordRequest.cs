using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalmyraHospital.Application.DTOs.Auth
{
    public class ChangePasswordRequest
    {
        public string Email { get; set; } = default!;
        public string NewPassword { get; set; } = default!;
    }
}
