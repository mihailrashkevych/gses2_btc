using GSES2_BTC.Core.Contracts.Data;

namespace GSES2_BTC.Core.Contracts.Service.MessagingProvider
{
    public interface IMessageService
    {
        Task<List<Reciever>> SendMessage(Message message);
        Task<bool> Subscribe(string email);
    }
}
