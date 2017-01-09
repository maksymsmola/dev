using MoneyKeeper.BusinessLogic.Dto;

namespace MoneyKeeper.BusinessLogic.Services
{
    public interface IUserService
    {
        SimpleUserDto GetUserByCredentials(string userName, string password);

        long CreateUser(CreateUserDto createUserDto);
    }
}