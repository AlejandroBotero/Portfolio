namespace newmvc.Models.ViewModels;

public class UserViewModel
{
    public required string Name { get; set; }
    public string? Surname { get; set; }
    public required string Email { get; set; }
    public string? Bio { get; set; }
    public DateTime? DateOfBirth { get; set; }
}
