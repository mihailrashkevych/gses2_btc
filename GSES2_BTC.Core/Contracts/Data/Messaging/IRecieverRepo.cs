using GSES2_BTC.Core.Contracts.Data;

namespace GSES2_BTC.Core.Contracts.Data.Messaging
{
    public interface IRecieverRepo<T> where T : BaseEntity
    {
        Task<List<T>?> GetAllRecieversAsync();

        Task<bool> AddRecieverAsync(T entity);
    }
}
