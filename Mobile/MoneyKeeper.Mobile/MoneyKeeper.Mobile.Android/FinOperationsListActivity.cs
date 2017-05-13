using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Android.App;
using Android.OS;
using Android.Widget;
using ModernHttpClient;
using MoneyKeeper.Mobile.Android.Adapters;
using MoneyKeeper.Mobile.Android.DataAccess;
using Newtonsoft.Json;

namespace MoneyKeeper.Mobile.Android
{
    [Activity(Label = "FinOperationsListActivity")]
    public class FinOperationsListActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.SetContentView(Resource.Layout.FinOperationsList);

            this.GetList();
        }

        private void GetList()
        {
            var httpClient = new HttpClient(new NativeMessageHandler());
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bareer", MoneyKeeperApp.Token);

            var response = httpClient.GetAsync("http://10.0.2.2:54502/api/FinOperations").Result;

            var finOperations =
                JsonConvert.DeserializeObject<List<FinOperation>>(response.Content.ReadAsStringAsync().Result);

            var listView = this.FindViewById<ListView>(Resource.Id.listView);

            listView.Adapter = new FinOperationsAdapter(this, this.LayoutInflater, finOperations);
        }
    }
}