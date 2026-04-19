using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PalmyraHospital.Domain.Entities;

namespace PalmyraHospital.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string? FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string? LastName { get; set; }
        [Required]
        [RegularExpression(@"^05\d{8}$", ErrorMessage = "Phone must start with 05 and contain 10 digits")]
        public string PhoneNumber { get; set; } = "";
        public bool IsActive { get; set; } = true;
        public Doctor? Doctor { get; set; }
        public Patient? Patient { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string FullName => $"{FirstName} {LastName}";


    }
}
