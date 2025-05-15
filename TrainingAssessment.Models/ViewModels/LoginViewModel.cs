using System.ComponentModel.DataAnnotations;

namespace TrainingAssessment.Models.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Email is Required")]
    [EmailAddress(ErrorMessage = "Email must be in valid format")]
    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = null!;
    public bool RememberMe { get; set; } = false;
}
