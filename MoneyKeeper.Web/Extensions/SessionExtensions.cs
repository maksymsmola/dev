using System;
using System.Web;

namespace MoneyKeeper.Web.Extensions
{
    internal static class SessionExtensions
    {
        private const string USER_KEY = "USER_KEY";

        public static void SetCurrentUserId(this HttpSessionStateBase session, long userId)
        {
            session[USER_KEY] = userId;
        }

        public static long GetCurrentUserId(this HttpSessionStateBase session)
        {
            object userId = session[USER_KEY];
            if (userId == null)
            {
                throw new InvalidOperationException("Current UserId not set. Please, set current user Id before using it.");
            }

            return (long)userId;
        }
    }
}