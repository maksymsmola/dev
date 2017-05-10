using MoneyKeeper.BusinessLogic.Dto.Synchronization;

namespace MoneyKeeper.BusinessLogic.Services
{
    public interface ISynchronizationService
    {
        SyncResponse SynchronizeData(SyncRequest dataFromClient);
    }
}