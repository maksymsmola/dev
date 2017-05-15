using System;
using System.Net.Http;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using ModernHttpClient;
using MoneyKeeper.Mobile.Android.DataAccess;

namespace MoneyKeeper.Mobile.Android.Activities
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
                var progress = new ProgressDialog(this);
                progress.SetCancelable(false);

                progress.Show();

                SyncService.Synchronize();

                progress.Cancel();

                var intetn = new Intent(this, typeof(FinOperationsListActivity));

                this.StartActivity(intetn);
            }

            this.FindViewById<Button>(Resource.Id.loginBtn).Click += this.OnLoginClick;
        }

        public void OnLoginClick(object sender, EventArgs eventArgs)
        {
            var progress = new ProgressDialog(this);
            progress.SetCancelable(false);

            progress.Show();

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

                progress.Cancel();

                var intetn = new Intent(this, typeof(FinOperationsListActivity));

                this.StartActivity(intetn);
            }
            else
            {
                progress.Cancel();

                Toast.MakeText(this, "Error occured while communication with server", ToastLength.Short).Show();
            }
        }
    }
}