using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace MVCSmallFarm.ViewModels;

public class VerifyAuthenticatorCodeViewModel
{
    [Required]
    public string Code { get; set; }
}
