using GSES2_BTC.Core.Contracts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSES2_BTC.Core.Contracts.Service.MessagingProvider
{
    public interface IMessageSender
    {
        Task<List<Reciever>> SendMessage(List<Reciever> recievers, Message message);
    }
}
