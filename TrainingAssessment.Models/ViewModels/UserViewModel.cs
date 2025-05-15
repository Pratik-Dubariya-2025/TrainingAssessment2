using System.ComponentModel.DataAnnotations;

namespace TrainingAssessment.Models.ViewModels;

public class UserViewModel
{
    public int? Id { get; set; }
    public int RoleId { get; set; } = 2;
    [Required(ErrorMessage = "First name is required")]
    [RegularExpression(@"^[^\d].*$", ErrorMessage = "First name cannot start with a digit.")]
    public string? FirstName { get; set; } = string.Empty;
    [Required]
    [RegularExpression(@"^[^\d].*$", ErrorMessage = "Last name cannot start with a digit.")]
    public string? LastName { get; set; } = string.Empty;
    public string Username { get; set; } = null!;
    [Required]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = null!;
    [Required]
    [RegularExpression(@"^[^\s](?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,6}[^\s]$", ErrorMessage = "Password is not in valid format.")]
    public string Password { get; set; } = null!;
}
