using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Widget;
using ModernHttpClient;
using MoneyKeeper.Mobile.Android.DataAccess;
using MoneyKeeper.Mobile.Android.Exceptions;
using Newtonsoft.Json;

namespace MoneyKeeper.Mobile.Android.Activities
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

            var dataAccess = new DataAccessService((ConnectivityManager)this.GetSystemService(ConnectivityService));

            try
            {
                dataAccess.AddFinOperation(finOperation);
            }
            catch (HttpRequestFailedException)
            {
                Toast.MakeText(this, "Error occured while ading operation", ToastLength.Short).Show();
            }
            finally
            {
                progress.Cancel();
            }

            var intent = new Intent(this, typeof(FinOperationsListActivity));

            this.StartActivity(intent);
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