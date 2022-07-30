using GSES2_BTC.Core.Contracts.Data;
using GSES2_BTC.Core.Contracts.Data.Messaging;

namespace FileDataStorage
{
    public class UserRepo : IRecieverRepo<Reciever>
    {
        private readonly FileStorageProvider<Reciever> _storageProvider;
        public UserRepo(string connectionString)
        {
            _storageProvider = new FileStorageProvider<Reciever>(connectionString);
        }

        public async Task<bool> AddRecieverAsync(Reciever entity)
        {
            var result = false;
            var isAny = false;
            var recievers = await _storageProvider.GetValues();
            if (recievers != null)
            {
                 isAny = recievers.Any(i => i.Email == entity.Email);
            }
            if (!isAny)
            {
                result = await _storageProvider.AddValue(entity);
                return result;
            }
            return result;
        }

        public async Task<List<Reciever>?> GetAllRecieversAsync()
        {
            var result = await _storageProvider.GetValues();
            return result;
        }
    }
}