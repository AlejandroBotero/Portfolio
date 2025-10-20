namespace newmvc.Models;

public class User
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Surname { get; set; }
    public required string Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime AccountCreationDate { get; set; }

}