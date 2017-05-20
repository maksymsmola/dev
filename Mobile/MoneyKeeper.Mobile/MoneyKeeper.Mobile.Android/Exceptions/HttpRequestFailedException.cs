using System;

namespace MoneyKeeper.Mobile.Android.Exceptions
{
    public class HttpRequestFailedException : ApplicationException
    {
        public override string Message => "An error occured while performing http request";
    }
}