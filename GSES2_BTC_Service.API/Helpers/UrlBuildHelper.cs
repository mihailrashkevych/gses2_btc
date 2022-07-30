using System.Text;

namespace GSES2_BTC_Service.API.Helpers
{
    public class UrlBuildHelper
    {
        public string BuildUrlForCoingate(string url, string from, string to)
        {
            var buider = new StringBuilder(url);
            buider.Replace("from", from);
            buider.Replace("to", to);
            return buider.ToString();    
        }
    }
}
