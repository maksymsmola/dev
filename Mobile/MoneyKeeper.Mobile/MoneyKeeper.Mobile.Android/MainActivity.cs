using System;
using System.IO;
using System.Net;
using System.Text;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Views;

namespace MoneyKeeper.Mobile.Android
{
    [Activity(Label = "MoneyKeeper.Mobile.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.Main);

            //var textView = this.FindViewById<TextView>(Resource.Id.textView);
            //if (MoneyKeeperApplication.IsAuthenticated)
            //{
            //    textView.SetText("Authenticated", TextView.BufferType.Normal);
            //}
            //else
            //{
            //    textView.SetText("Ni figa ne authenticated", TextView.BufferType.Normal);
            //}
        }

        public void OnLoginClick(View view)
        {
            string userName = this.FindViewById<EditText>(Resource.Id.userName).Text;
            string password = this.FindViewById<EditText>(Resource.Id.password).Text;
            string body = $"{{\"userName\":\"{userName}\",\"password\":\"{password}\"}}";
            byte[] bodyByte = Encoding.UTF8.GetBytes(body);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://10.0.2.2:54502/api/signIn");
            request.Method = "POST";
            request.ContentType = "application/json";
            request.TransferEncoding = "utf-8";
            request.ContentLength = bodyByte.LongLength;

            var dataStream = request.GetRequestStream();
            dataStream.Write(bodyByte, 0, bodyByte.Length);
            dataStream.Close();

            string token = string.Empty;
            using (WebResponse response = request.GetResponse())
                using (var responseStream = response.GetResponseStream())
                    using (StreamReader sr = new StreamReader(responseStream))
                    {
                        token = sr.ReadToEnd();
                    }

            if (!string.IsNullOrWhiteSpace(token))
            {
                var intetn = new Intent(this, typeof(FinOperationsListActivity));

                this.StartActivity(intetn);
            }
        }
    }
}