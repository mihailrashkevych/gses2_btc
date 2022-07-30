namespace GSES2_BTC.Core.Contracts.Service.MessagingProvider
{
    public class Message
    {
        public string Subject { get; set; } = "GSES2 Crypto Exchange Rate Service";
        public string Body { get; set; } = "GSES2 Crypto Exchange Rate Service";
        public string? Rate { get; set; }
        public string From { get; set; } = "BTC";
        public string To { get; set; } = "UAH";
        public string ToSourceUrl { get; set; } = "";
        public string Footer { get; set; } = "Thanks for using GSES2 Service!";
    }
}