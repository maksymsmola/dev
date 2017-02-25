using MoneyKeeper.BusinessLogic.Dto;
using MoneyKeeper.BusinessLogic.Dto.User;
using MoneyKeeper.Web.Models;

namespace MoneyKeeper.Web.Mappings
{
    internal static class UserMappings
    {
        public static CreateUserDto ToCreateUserDto(this SignUpUserModel signUpModel)
        {
            return new CreateUserDto
            {
                LoginName = signUpModel.Name,
                FirstName = signUpModel.FirstName,
                LastName = signUpModel.LastName,
                Password = signUpModel.Password
            };
        }
    }
}