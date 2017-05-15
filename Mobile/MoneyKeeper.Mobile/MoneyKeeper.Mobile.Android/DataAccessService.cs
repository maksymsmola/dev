using Android.Net;
using MoneyKeeper.Mobile.Android.DataAccess;
using MoneyKeeper.Mobile.Android.Exceptions;

namespace MoneyKeeper.Mobile.Android
{
    public class DataAccessService
    {
        private readonly ConnectivityManager connectivityManager;

        public DataAccessService(ConnectivityManager connectivityManager)
        {
            this.connectivityManager = connectivityManager;
        }

        public void AddFinOperation(FinOperation finOperation)
        {
            if (this.IsOnline())
            {
                using (var httpAccess = new HttpAccessService())
                {
                    FinOperation addedFinOperation = httpAccess.AddFinOperationAsync(finOperation).Result;
                    if (addedFinOperation == null)
                    {
                        throw new HttpRequestFailedException();
                    }

                    Database.AddFinOperation(addedFinOperation);
                }
            }
            else
            {
                finOperation.State = EntityState.Added;
                Database.AddFinOperation(finOperation);
            }
        }

        public void UpdateFinOperation(FinOperation finOperation)
        {
            if (this.IsOnline())
            {
                using (var httpAccess = new HttpAccessService())
                {
                    FinOperation updatedFinOperation = httpAccess.UpdateFinOperationAsync(finOperation).Result;
                    if (updatedFinOperation == null)
                    {
                        throw new HttpRequestFailedException();
                    }

                    Database.UpdateFinOperation(updatedFinOperation);
                }
            }
            else
            {
                FinOperation existing = Database.GetByLocalId(finOperation.LocalId);
                finOperation.State = existing.State != EntityState.Synchronized
                                     ? existing.State
                                     : EntityState.Updated;
                Database.UpdateFinOperation(finOperation);
            }
        }

        public void DeleteFinOperation(long finOperationId, long localId)
        {
            if (this.IsOnline())
            {
                using (var httpAccess = new HttpAccessService())
                {
                    long deletedId = httpAccess.DeleteFinOperationAsync(finOperationId).Result;
                    if (deletedId == -1)
                    {
                        throw new HttpRequestFailedException();
                    }

                    Database.DeleteFinOperation(deletedId);
                }
            }
            else
            {
                Database.MarkAsDeletedFinOperation(localId);
            }
        }

        private bool IsOnline()
        {
            return this.connectivityManager.ActiveNetworkInfo?.IsConnected ?? false;
        }
    }
}