using MoneyKeeper.BusinessLogic.Dto;
using MoneyKeeper.BusinessLogic.Dto.User;

namespace MoneyKeeper.BusinessLogic.Services
{
    public interface IUserService
    {
        SimpleUserDto GetUserByCredentials(string userName, string password);

        long CreateUser(CreateUserDto createUserDto);
    }
}