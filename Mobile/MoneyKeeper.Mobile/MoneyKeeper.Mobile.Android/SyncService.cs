using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using ModernHttpClient;
using MoneyKeeper.Mobile.Android.DataAccess;
using Newtonsoft.Json;

namespace MoneyKeeper.Mobile.Android
{
    public class SyncService
    {
        public static void Synchronize()
        {
            SyncModel syncRequest = CreateSyncRequest();

            SyncModel syncResponse = GetResponseFromServer(syncRequest);

            UpdateClientDb(syncResponse);
        }

        private static SyncModel CreateSyncRequest()
        {
            List<FinOperation> notSyncData = Database.GetNotSyncedData();

            return new SyncModel
            {
                DeletedFinOperationsIds = notSyncData.Where(x => x.State == EntityState.Deleted).Select(x => x.Id).ToList(),
                AddedFinOperations = notSyncData.Where(x => x.State == EntityState.Added).ToList(),
                UpdatedFinOperations = notSyncData.Where(x => x.State == EntityState.Updated).ToList()
            };
        }

        private static SyncModel GetResponseFromServer(SyncModel model)
        {
            var httpClient = new HttpClient(new NativeMessageHandler());

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bareer", MoneyKeeperApp.Token);

            string requestBody = JsonConvert.SerializeObject(model);

            var result = httpClient.PostAsync(
                    "http://10.0.2.2:54502/api/sync",
                    new StringContent(
                        requestBody,
                        Encoding.UTF8,
                        "application/json"))
                .Result;

            return JsonConvert.DeserializeObject<SyncModel>(result.Content.ReadAsStringAsync().Result);
        }

        private static void UpdateClientDb(SyncModel model)
        {
            Database.DeleteFinOperationsWithoutId();

            if (model.AddedFinOperations.Any())
            {
                Database.AddFinOperations(model.AddedFinOperations);
            }

            if (model.UpdatedFinOperations.Any())
            {
                Database.UpdateAllFinOperations(model.UpdatedFinOperations);
            }

            if (model.DeletedFinOperationsIds.Any())
            {
                Database.DeleteFinOperations(model.DeletedFinOperationsIds);
            }
        }
    }
}