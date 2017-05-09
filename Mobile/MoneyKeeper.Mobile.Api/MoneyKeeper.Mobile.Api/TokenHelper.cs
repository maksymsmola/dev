using System;
using System.Text;
using MoneyKeeper.BusinessLogic.Dto.User;

namespace MoneyKeeper.Mobile.Api
{
    public static class TokenHelper
    {
        public static string GenerateToken(SimpleUserDto userDto)
        {
            string str = $"id={userDto.Id};name={userDto.LoginName}";

            byte[] strBytes = Encoding.UTF8.GetBytes(str);

            return Convert.ToBase64String(strBytes);
        }

        public static SimpleUserDto GetUserFromToken(string token)
        {
            string str = Encoding.UTF8.GetString(Convert.FromBase64String(token));

            string[] credentials = str.Split(';');

            return new SimpleUserDto
            {
                Id = long.Parse(credentials[0].Split('=')[1]),
                LoginName = credentials[1].Split('=')[1]
            };
        }
    }
}