using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ModernHttpClient;
using MoneyKeeper.Mobile.Android.DataAccess;
using Newtonsoft.Json;

namespace MoneyKeeper.Mobile.Android
{
    public class HttpAccessService : IDisposable
    {
        private const string SYNC_BASE_URL = "http://money-keeper-api.azurewebsites.net/api/Sync";
        private const string FIN_OPERATION_BASE_URL = "http://money-keeper-api.azurewebsites.net/api/FinOperation";
        private readonly HttpClient httpClient;

        public HttpAccessService()
        {
            this.httpClient = new HttpClient(new NativeMessageHandler());
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bareer", MoneyKeeperApp.Token);
        }

        public async Task<FinOperation> AddFinOperationAsync(FinOperation finOperation)
        {
            HttpResponseMessage response = await this.PostAsync(FIN_OPERATION_BASE_URL, finOperation);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string responseJson = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<FinOperation>(responseJson);
        }

        public async Task<FinOperation> UpdateFinOperationAsync(FinOperation finOperation)
        {
            HttpResponseMessage response = await this.PutAsync(FIN_OPERATION_BASE_URL, finOperation);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string responseJson = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FinOperation>(responseJson);
            result.LocalId = finOperation.LocalId;

            return result;
        }

        public async Task<long> DeleteFinOperationAsync(long id)
        {
            HttpResponseMessage response = await this.DeleteAsync($"{FIN_OPERATION_BASE_URL}?id={id}");

            if (!response.IsSuccessStatusCode)
            {
                return -1;
            }

            return id;
        }

        public async Task<SyncModel> SendSyncRequestAsync(SyncModel model)
        {
            HttpResponseMessage result = await this.PostAsync(SYNC_BASE_URL, model);

            if (!result.IsSuccessStatusCode)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<SyncModel>(await result.Content.ReadAsStringAsync());
        }

        public void Dispose()
        {
            this.httpClient.Dispose();
        }

        #region | Private wrappers around HttpClient |

        private async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await this.httpClient.GetAsync(url);
        }

        private async Task<HttpResponseMessage> PostAsync<T>(string url, T model)
        {
            return await this.httpClient.PostAsync(
                    url,
                    new StringContent(
                        JsonConvert.SerializeObject(model),
                        Encoding.UTF8,
                        "application/json")
                );
        }

        private async Task<HttpResponseMessage> PutAsync<T>(string url, T model)
        {
            return await this.httpClient.PutAsync(
                    url,
                    new StringContent(
                        JsonConvert.SerializeObject(model),
                        Encoding.UTF8,
                        "application/json")
                );
        }

        private async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            return await this.httpClient.DeleteAsync(url);
        }

        #endregion
    }
}