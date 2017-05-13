using System;
using System.IO;
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
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            dbPath = Path.Combine(folder, dbPath);

            using (var connection = new SQLiteConnection(new SQLitePlatformAndroid(), dbPath))
            {
                connection.CreateTable<User>();
                connection.CreateTable<FinOperation>();
            }
        }

        public static string GetToken()
        {
            using (var connection = new SQLiteConnection(new SQLitePlatformAndroid(), dbPath))
            {
                return connection.Query<User>("SELECT * FROM User;").FirstOrDefault()?.Token;
            }
        }

        public static void SaveToken(string token)
        {
            using (var connection = new SQLiteConnection(new SQLitePlatformAndroid(), dbPath))
            {
                var existing = connection.Query<User>("SELECT * FROM User;").FirstOrDefault();
                if (existing != null)
                {
                    connection.Execute("DELETE FROM User");
                }

                connection.Insert(new User { Token = token });
            }
        }
    }
}