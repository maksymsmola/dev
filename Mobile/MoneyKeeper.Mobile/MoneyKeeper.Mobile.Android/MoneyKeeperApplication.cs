using System;
using Android.App;
using Android.Runtime;
using MoneyKeeper.Mobile.Android.DataAccess;

namespace MoneyKeeper.Mobile.Android
{
    [Application]
    public class MoneyKeeperApplication : Application
    {
        public static bool IsAuthenticated => !string.IsNullOrWhiteSpace(Token);

        public static string Token { get; set; }

        public MoneyKeeperApplication(IntPtr handle, JniHandleOwnership ownerShip)
            : base(handle, ownerShip)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            Database.Create();

            Token = Database.GetToken();
        }
    }
}