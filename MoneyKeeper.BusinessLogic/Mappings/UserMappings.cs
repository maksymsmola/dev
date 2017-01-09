using MoneyKeeper.BusinessLogic.Dto;
using MoneyKeeper.Core.Entities;
using MoneyKeeper.Core.Extensions;

namespace MoneyKeeper.BusinessLogic.Mappings
{
    internal static class UserMappings
    {
        public static User ToUser(this CreateUserDto createDto)
        {
            return new User
            {
                LoginName = createDto.LoginName,
                FirstName = createDto.FirstName,
                LastName = createDto.LastName,
                HashedPasword = createDto.Password.Encode()
            };
        }

        public static SimpleUserDto ToSimpleUserDto(this User user)
        {
            return new SimpleUserDto
            {
                Id = user.Id,
                LoginName = user.LoginName
            };
        }
    }
}