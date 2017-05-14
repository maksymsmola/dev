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
    [Activity(Label = "Добавить операцию")]
    public class AddFinOperationActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.SetContentView(Resource.Layout.AddFinOperation);

            this.FindViewById<RadioButton>(Resource.Id.radioExpense).Checked = true;
            this.FindViewById<EditText>(Resource.Id.addFinOpDateEditText).Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.FindViewById<Button>(Resource.Id.addFinOperationBtn).Click += this.OnAddFinOperationClick;
        }

        private void OnAddFinOperationClick(object sender, EventArgs eventArgs)
        {
            var progress = new ProgressDialog(this);
            progress.SetCancelable(false);
            progress.Show();

            var finOperation = new FinOperation
            {
                Value = double.Parse(this.FindViewById<EditText>(Resource.Id.addFinOpValueEditText).Text),
                Date = DateTime.Parse(this.FindViewById<EditText>(Resource.Id.addFinOpDateEditText).Text),
                Description = this.FindViewById<EditText>(Resource.Id.addFinOpDescriptionEditText).Text,
                Type = this.GetFinOperationType()
            };

            var httpClient = new HttpClient(new NativeMessageHandler());
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bareer", MoneyKeeperApp.Token);

            using (httpClient)
            {
                HttpResponseMessage response = httpClient.PostAsync(
                        "http://10.0.2.2:54502/api/FinOperation",
                        new StringContent(
                            JsonConvert.SerializeObject(finOperation),
                            Encoding.UTF8,
                            "application/json"))
                    .Result;

                if (response.IsSuccessStatusCode)
                {
                    Database.AddFinOperation(JsonConvert.DeserializeObject<FinOperation>(response.Content.ReadAsStringAsync().Result));

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

        private FinOperationType GetFinOperationType()
        {
            var incomeRadioBtn = this.FindViewById<RadioButton>(Resource.Id.radioIncome);
            var expenseRadioBtn = this.FindViewById<RadioButton>(Resource.Id.radioExpense);

            if (incomeRadioBtn.Checked)
            {
                return FinOperationType.Income;
            }
            else if (expenseRadioBtn.Checked)
            {
                return FinOperationType.Expense;
            }
            else
            {
                return FinOperationType.None;
            }
        }
    }
}