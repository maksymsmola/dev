using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ModernHttpClient;
using MoneyKeeper.Mobile.Android.DataAccess;
using Newtonsoft.Json;

namespace MoneyKeeper.Mobile.Android
{
    [Activity(Label = "Редактировать операцию")]
    public class EditFinOperationActivity : Activity
    {
        public const string OPERATION_ID_KEY = nameof(OPERATION_ID_KEY);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.SetContentView(Resource.Layout.EditFinOperation);

            this.FillView();

            this.FindViewById<Button>(Resource.Id.edit_editBtn).Click += OnFinOperationEdit;
            this.FindViewById<Button>(Resource.Id.edit_deleteBtn).Click += OnFinOperationDelete;
        }

        private void FillView()
        {
            long id = this.Intent.GetLongExtra(OPERATION_ID_KEY, 0);
            FinOperation finOperation = Database.GetById(id);

            this.FindViewById<EditText>(Resource.Id.edit_value).Text = finOperation.Value.ToString();
            this.FindViewById<EditText>(Resource.Id.edit_date).Text = finOperation.Date.ToString("yyyy-MM-dd");
            this.FindViewById<EditText>(Resource.Id.edit_description).Text = finOperation.Description;
        }

        private void OnFinOperationEdit(object sender, EventArgs eventArgs)
        {
            var progress = new ProgressDialog(this);
            progress.SetCancelable(false);
            progress.Show();

            var finOperation = new FinOperation
            {
                Id = this.Intent.GetLongExtra(OPERATION_ID_KEY, 0),
                Value = double.Parse(this.FindViewById<EditText>(Resource.Id.edit_value).Text),
                Date = DateTime.Parse(this.FindViewById<EditText>(Resource.Id.edit_date).Text),
                Description = this.FindViewById<EditText>(Resource.Id.edit_description).Text
            };

            var httpClient = new HttpClient(new NativeMessageHandler());
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bareer", MoneyKeeperApp.Token);

            using (httpClient)
            {
                HttpResponseMessage response = httpClient.PutAsync(
                        "http://10.0.2.2:54502/api/FinOperation",
                        new StringContent(
                            JsonConvert.SerializeObject(finOperation),
                            Encoding.UTF8,
                            "application/json"))
                    .Result;

                if (response.IsSuccessStatusCode)
                {
                    Database.UpdateFinOperation(finOperation);

                    progress.Cancel();

                    var intent = new Intent(this, typeof(FinOperationsListActivity));

                    this.StartActivity(intent);
                }
                else
                {
                    progress.Cancel();
                    Toast.MakeText(this, "Error occured while ading operation", ToastLength.Short).Show();
                }
            }
        }

        private void OnFinOperationDelete(object sender, EventArgs e)
        {
            var progress = new ProgressDialog(this);
            progress.SetCancelable(false);
            progress.Show();

            var httpClient = new HttpClient(new NativeMessageHandler());
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bareer", MoneyKeeperApp.Token);

            using (httpClient)
            {
                long id = this.Intent.GetLongExtra(OPERATION_ID_KEY, 0);
                string uri = "http://10.0.2.2:54502/api/FinOperation?id=" + id;

                HttpResponseMessage response = httpClient.DeleteAsync(uri).Result;

                if (response.IsSuccessStatusCode)
                {
                    Database.DeleteFinOperation(id);

                    progress.Cancel();

                    var intent = new Intent(this, typeof(FinOperationsListActivity));

                    this.StartActivity(intent);
                }
                else
                {
                    progress.Cancel();
                    Toast.MakeText(this, "Error occured while ading operation", ToastLength.Short).Show();
                }
            }
        }
    }
}