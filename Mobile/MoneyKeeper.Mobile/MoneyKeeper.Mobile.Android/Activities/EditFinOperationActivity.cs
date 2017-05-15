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
    [Activity(Label = "Редактировать операцию")]
    public class EditFinOperationActivity : Activity
    {
        public const string OPERATION_ID_KEY = nameof(OPERATION_ID_KEY);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.SetContentView(Resource.Layout.EditFinOperation);

            this.FillView();

            this.FindViewById<Button>(Resource.Id.edit_editBtn).Click += this.OnFinOperationEdit;
            this.FindViewById<Button>(Resource.Id.edit_deleteBtn).Click += this.OnFinOperationDelete;
        }

        private void FillView()
        {
            long id = this.Intent.GetLongExtra(OPERATION_ID_KEY, 0);
            FinOperation finOperation = Database.GetByLocalId(id);

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
                Id = Database.GetByLocalId(this.Intent.GetLongExtra(OPERATION_ID_KEY, 0)).Id,
                Value = double.Parse(this.FindViewById<EditText>(Resource.Id.edit_value).Text),
                Date = DateTime.Parse(this.FindViewById<EditText>(Resource.Id.edit_date).Text),
                Description = this.FindViewById<EditText>(Resource.Id.edit_description).Text
            };

            var dataAccess = new DataAccessService((ConnectivityManager)this.GetSystemService(ConnectivityService));

            try
            {
                dataAccess.UpdateFinOperation(finOperation);
            }
            catch (HttpRequestFailedException)
            {
                Toast.MakeText(this, "Error occured while updating operation", ToastLength.Short).Show();
            }
            finally
            {
                progress.Cancel();
            }

            var intent = new Intent(this, typeof(FinOperationsListActivity));

            this.StartActivity(intent);
        }

        private void OnFinOperationDelete(object sender, EventArgs e)
        {
            var progress = new ProgressDialog(this);
            progress.SetCancelable(false);
            progress.Show();

            var dataAccess = new DataAccessService((ConnectivityManager)this.GetSystemService(ConnectivityService));

            try
            {
                long localId = this.Intent.GetLongExtra(OPERATION_ID_KEY, 0);
                long remoteId = Database.GetByLocalId(localId).Id;
                dataAccess.DeleteFinOperation(remoteId, localId);
            }
            catch (HttpRequestFailedException)
            {
                Toast.MakeText(this, "Error occured while deleting operation", ToastLength.Short).Show();
            }
            finally
            {
                progress.Cancel();
            }

            var intent = new Intent(this, typeof(FinOperationsListActivity));

            this.StartActivity(intent);
        }
    }
}