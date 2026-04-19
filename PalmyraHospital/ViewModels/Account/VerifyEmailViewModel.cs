using System.ComponentModel.DataAnnotations;

namespace PalmyraHospital.Web.ViewModels.Account
{
    public class VerifyEmailViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }
    
    }
}
