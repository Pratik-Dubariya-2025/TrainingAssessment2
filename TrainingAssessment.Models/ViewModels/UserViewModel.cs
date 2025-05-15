namespace TrainingAssessment.Models.ViewModels;

public class UserViewModel
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
