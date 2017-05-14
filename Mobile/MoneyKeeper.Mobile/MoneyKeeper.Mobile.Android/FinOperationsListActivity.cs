using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using ModernHttpClient;
using MoneyKeeper.Mobile.Android.Adapters;
using MoneyKeeper.Mobile.Android.DataAccess;
using Newtonsoft.Json;

namespace MoneyKeeper.Mobile.Android
{
    [Activity(Label = "Операции")]
    public class FinOperationsListActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.SetContentView(Resource.Layout.FinOperationsList);

            this.GetList();

            this.FindViewById<Button>(Resource.Id.goToAddFinOperationBtn).Click += (sender, args) =>
            {
                var intent = new Intent(this, typeof(AddFinOperationActivity));

                this.StartActivity(intent);
            };
        }

        private void GetList()
        {
            List<FinOperation> finOperations
                = Database.GetAllFinOperations().OrderByDescending(x => x.Date).ToList();

            var listView = this.FindViewById<ListView>(Resource.Id.listView);

            listView.Adapter = new FinOperationsAdapter(this, this.LayoutInflater, finOperations);
        }
    }
}