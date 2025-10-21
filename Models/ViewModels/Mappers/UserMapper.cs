
namespace newmvc.Models.ViewModels.Mappers;

public class UserMapper
{
    public static UserViewModel ToViewModel(User user)
    {
        return new UserViewModel
        {
            FullName = user.FullName,
            Email = user.Email,
        };
    }

    public static User ToModel(UserViewModel userViewModel)
    {
        return new User
        {
            FullName = userViewModel.FullName,
            Email = userViewModel.Email,
        };
    }
}