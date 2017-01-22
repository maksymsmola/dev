﻿using MoneyKeeper.BusinessLogic.Dto;
using MoneyKeeper.Web.Models;

namespace MoneyKeeper.Web.Mappings
{
    internal static class UserMappings
    {
        public static CreateUserDto ToCreateUserDto(this SignUpUserModel signUpModel)
        {
            return new CreateUserDto
            {
                LoginName = signUpModel.LoginName,
                FirstName = signUpModel.FirstName,
                LastName = signUpModel.LastName,
                Password = signUpModel.Password
            };
        }
    }
}