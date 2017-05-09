using System.Linq;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;

namespace MoneyKeeper.Mobile.Android.DataAccess
{
    public class Database
    {
        private static string dbPath = "moneyKeeper.sqlite";

        public static void Create()
        {
            using(var connection = new SQLiteConnection(new SQLitePlatformAndroid(), dbPath))
            {
                connection.CreateTable<User>();
                connection.CreateTable<FinOperation>();
            }
        }

        public static string GetToken()
        {
            using (var connection = new SQLiteConnection(new SQLitePlatformAndroid(), dbPath))
            {
                return connection.Query<string>("SELECT TOP 1 Token from User").FirstOrDefault();
            }
        }
    }
}