using GSES2_BTC.Core.Contracts.Data;
using GSES2_BTC.Core.Contracts.Data.Messaging;
using GSES2_BTC.Core.Contracts.Service.ExchangeRateProvider;
using GSES2_BTC.Core.Contracts.Service.MessagingProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSES2_BTC.Core.Services.MessagingProvider
{
    public class MessageService : IMessageService
    {
        private readonly IRecieverRepo<Reciever> _recieverRepo;
        private readonly IExchangeRateService _exchangeRateService;
        private readonly IMessageSender _messageSender;
        public MessageService(IRecieverRepo<Reciever> recieverRepo, IExchangeRateService exchangeRateService, IMessageSender messageSender)
        {
            _exchangeRateService = exchangeRateService;
            _recieverRepo = recieverRepo;
            _messageSender = messageSender;
        }   
        public async Task<List<Reciever>> SendMessage(Message message)
        {
            var rate = await _exchangeRateService.GetExchangeRateAsync(message.ToSourceUrl);
            message.Rate = rate;

            var recievers = await _recieverRepo.GetAllRecieversAsync();
            var result = await _messageSender.SendMessage(recievers, message);
            return result;
        }

        public async Task<bool> Subscribe(string email)
        {
            var reciever = new Reciever();
            reciever.Email = email;
            var result = await _recieverRepo.AddRecieverAsync(reciever);
            return result;
        }
    }
}
