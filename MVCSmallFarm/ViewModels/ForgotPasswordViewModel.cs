using System.ComponentModel.DataAnnotations;

namespace MVCSmallFarm.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage ="Please enter an email.")]
        [EmailAddress(ErrorMessage ="Email format is not correct.")]
        public string Email { get; set; }
    }
}
