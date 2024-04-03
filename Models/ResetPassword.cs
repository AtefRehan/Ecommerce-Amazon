using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models;

public class ResetPassword
{
    [Required]
    public string Password { get; set; } = null;
    [Compare("Password",ErrorMessage = "Passwords doesn't match ! ")]
    public string ConfirmPassword { get; set; } = null;
    public string Email { get; set; }=null;
    public string Token { get; set; }=null;
}