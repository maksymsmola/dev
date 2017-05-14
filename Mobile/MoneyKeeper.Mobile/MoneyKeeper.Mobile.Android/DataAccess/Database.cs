using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

            using (SQLiteConnection connection = CreateConnection())
            {
                connection.CreateTable<User>();
                connection.CreateTable<FinOperation>();
            }
        }

        public static string GetToken()
        {
            using (SQLiteConnection connection = CreateConnection())
            {
                return connection.Query<User>("SELECT * FROM User;").FirstOrDefault()?.Token;
            }
        }

        public static void SaveToken(string token)
        {
            using (SQLiteConnection connection = CreateConnection())
            {
                var existing = connection.Query<User>("SELECT * FROM User;").FirstOrDefault();
                if (existing != null)
                {
                    connection.Execute("DELETE FROM User;");
                }

                connection.Insert(new User { Token = token });
            }
        }

        public static List<FinOperation> GetAllFinOperations()
        {
            using (SQLiteConnection connection = CreateConnection())
            {
                return connection.Query<FinOperation>("SELECT * FROM FinOperation;");
            }
        }

        #region | For work with one FinOperation |

        public static FinOperation GetById(long id)
        {
            using (SQLiteConnection connection = CreateConnection())
            {
                return connection.Get<FinOperation>(x => x.Id == id);
            }
        }

        public static void AddFinOperation(FinOperation finOperation)
        {
            using (SQLiteConnection connection = CreateConnection())
            {
                connection.Insert(finOperation);
            }
        }

        public static void UpdateFinOperation(FinOperation finOperation)
        {
            using (SQLiteConnection connection = CreateConnection())
            {
                string query = $@"
UPDATE FinOperation
SET {nameof(FinOperation.Value)} = '{finOperation.Value}',
        {nameof(FinOperation.Description)} = '{finOperation.Description}',
        {nameof(FinOperation.Date)} = '{finOperation.Date:yyyy-MM-dd}'
    WHERE {nameof(FinOperation.Id)} = {finOperation.Id};";

                connection.Execute(query);
            }
        }

        public static void DeleteFinOperation(long id)
        {
            using (SQLiteConnection connection = CreateConnection())
            {
                connection.Execute($"DELETE FROM FinOperation WHERE Id = {id};");
            }
        }

        #endregion

        #region | For syncronization |

        public static List<FinOperation> GetNotSyncedData()
        {
            using (SQLiteConnection connection = CreateConnection())
            {
                return connection.Query<FinOperation>(
                    $"SELECT * FROM FinOperation WHERE State <> {EntityState.Synchronized:D};");
            }
        }

        public static void DeleteFinOperationsWithoutId()
        {
            using (SQLiteConnection connection = CreateConnection())
            {
                connection.Execute($"DELETE FROM FinOperation WHERE State = {EntityState.Added:D};");
            }
        }

        public static void AddFinOperations(List<FinOperation> finOperations)
        {
            finOperations.ForEach(x => x.State = EntityState.Synchronized);

            using (SQLiteConnection connection = CreateConnection())
            {
                connection.InsertAll(finOperations);
            }
        }

        public static void UpdateAllFinOperations(List<FinOperation> finOperations)
        {
            finOperations.ForEach(x => x.State = EntityState.Synchronized);

            var sb = new StringBuilder();
            finOperations.ForEach(x =>
            {
                sb.AppendLine($@"
UPDATE FinOperation
    SET
        {nameof(FinOperation.Value)} = '{x.Value}',
        {nameof(FinOperation.Description)} = '{x.Description}',
        {nameof(FinOperation.Date)} = '{x.Date:yyyy-MM-dd}'
    WHERE {nameof(FinOperation.Id)} = {x.Id};");
            });

            using (SQLiteConnection connection = CreateConnection())
            {
                connection.Execute(sb.ToString());
            }
        }

        public static void DeleteFinOperations(List<long> ids)
        {
            using (SQLiteConnection connection = CreateConnection())
            {
                connection.Execute($"DELETE FROM FinOperation WHERE Id IN ({string.Join(",", ids)});");
            }
        }

        #endregion

        private static SQLiteConnection CreateConnection()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbPath);

            return new SQLiteConnection(new SQLitePlatformAndroid(), path);
        }
    }
}