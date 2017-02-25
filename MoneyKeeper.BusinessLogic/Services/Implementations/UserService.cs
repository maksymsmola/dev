using MoneyKeeper.BusinessLogic.Dto;
using MoneyKeeper.BusinessLogic.Dto.User;
using MoneyKeeper.BusinessLogic.Mappings;
using MoneyKeeper.Core.Entities;
using MoneyKeeper.Core.Extensions;
using MoneyKeeper.DataAccess.Repository;

namespace MoneyKeeper.BusinessLogic.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IRepository repository;

        public UserService(IRepository repository)
        {
            this.repository = repository;
        }

        public SimpleUserDto GetUserByCredentials(string userName, string password)
        {
            string encodedPassword = password.Encode();

            var foundUser = this.repository.FindByCriteria<User>(user => user.LoginName == userName && user.HashedPasword == encodedPassword);

            return foundUser?.ToSimpleUserDto();
        }

        public long CreateUser(CreateUserDto createUserDto)
        {
            User user = createUserDto.ToUser();

            this.repository.Add(user);
            this.repository.SaveChanges();

            return user.Id;
        }
    }
}