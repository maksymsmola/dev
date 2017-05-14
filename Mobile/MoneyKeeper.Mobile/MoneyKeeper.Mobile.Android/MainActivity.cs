using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Views;
using ModernHttpClient;
using MoneyKeeper.Mobile.Android.DataAccess;

namespace MoneyKeeper.Mobile.Android
{
    [Activity(Label = "Вход", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.Main);

            if (MoneyKeeperApp.IsAuthenticated)
            {
                SyncService.Synchronize();

                var intetn = new Intent(this, typeof(FinOperationsListActivity));

                this.StartActivity(intetn);
            }

            this.FindViewById<Button>(Resource.Id.loginBtn).Click += this.OnLoginClick;
        }

        public void OnLoginClick(object sender, EventArgs eventArgs)
        {
            string userName = this.FindViewById<EditText>(Resource.Id.userName).Text;
            string password = this.FindViewById<EditText>(Resource.Id.password).Text;

            var httpClient = new HttpClient(new NativeMessageHandler());

            var result = httpClient.PostAsync(
                    "http://10.0.2.2:54502/api/signIn",
                    new StringContent(
                        $"{{\"userName\":\"{userName}\",\"password\":\"{password}\"}}",
                        Encoding.UTF8,
                        "application/json"))
                .Result;

            string token = result.Content.ReadAsStringAsync().Result;
            if (result.IsSuccessStatusCode && !string.IsNullOrWhiteSpace(token))
            {
                Database.SaveToken(token);

                MoneyKeeperApp.Token = token;

                SyncService.Synchronize();

                var intetn = new Intent(this, typeof(FinOperationsListActivity));

                this.StartActivity(intetn);
            }
            else
            {
                Toast.MakeText(this, "Error occured while communication with server", ToastLength.Short).Show();
            }
        }
    }
}