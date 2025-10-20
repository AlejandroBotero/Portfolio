
namespace newmvc.Models.ViewModels.Mappers;

public class UserMapper
{
    public static UserViewModel ToViewModel(User user)
    {
        return new UserViewModel
        {
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth
        };
    }

    public static User ToModel(UserViewModel userViewModel)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Name = userViewModel.Name,
            Surname = userViewModel.Surname,
            Email = userViewModel.Email,
            DateOfBirth = userViewModel.DateOfBirth,
            AccountCreationDate = DateTime.UtcNow
        };
    }
}