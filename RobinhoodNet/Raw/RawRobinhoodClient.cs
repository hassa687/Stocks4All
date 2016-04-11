using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;

namespace BasicallyMe.RobinhoodNet.Raw
{
    public partial class RawRobinhoodClient
    {
        HttpClient _httpClient;

        static readonly string LOGIN_URL = "https://api.robinhood.com/api-token-auth/";
        static readonly string INVESTMENT_PROFILE_URL = "https://api.robinhood.com/user/investment_profile/";
        static readonly string ACCOUNTS_URL = "https://api.robinhood.com/accounts/";
        static readonly string ACH_IAV_AUTH = "https://api.robinhood.com/ach/iav/auth/";
        static readonly string ACH_RELATIONSHIPS_URL = "https://api.robinhood.com/ach/relationships/";
        static readonly string ACH_TRANSFERS_URL = "https://api.robinhood.com/ach/transfers/";
        static readonly string APPLICATIONS_URL = "https://api.robinhood.com/applications/";
        static readonly string DIVIDENDS_URL = "https://api.robinhood.com/dividends/";
        static readonly string EDOCUMENTS_URL = "https://api.robinhood.com/documents/";
        static readonly string INSTRUMENTS_URL = "https://api.robinhood.com/instruments/";
        static readonly string FUNDAMENTALS_URL = "https://api.robinhood.com/fundamentals/";
        static readonly string MARGIN_UPGRADES_URL = "https://api.robinhood.com/margin/upgrades/";
        static readonly string MARKETS_URL = "https://api.robinhood.com/markets/";
        static readonly string NOTIFICATIONS_URL = "https://api.robinhood.com/notifications/";
        static readonly string ORDERS_URL = "https://api.robinhood.com/orders/";
        static readonly string PASSWORD_RESET_URL = "https://api.robinhood.com/password_reset/request/";
        static readonly string QUOTES_URL = "https://api.robinhood.com/quotes/";
        static readonly string DUCUMENT_REQUESTS_URL = "https://api.robinhood.com/upload/document_requests/";
        static readonly string USER_URL = "https://api.robinhood.com/user/";
        static readonly string WATCHLISTS_URL = "https://api.robinhood.com/watchlists/";
        static string POSITIONS_URL;

        public RawRobinhoodClient ()
        {
            var handler = new HttpClientHandler();
            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip |
                                                 DecompressionMethods.Deflate;
            }

            _httpClient = new HttpClient(handler);
            _httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            _httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            _httpClient.DefaultRequestHeaders.Add(
                "Accept-Language",
                "en;q=1, fr;q=0.9, de;q=0.8, ja;q=0.7, nl;q=0.6, it;q=0.5");
            _httpClient.DefaultRequestHeaders.Add("X-Robinhood-API-Version", "1.0.0");
            _httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Robinhood/823 (iPhone; iOS 7.1.2; Scale/2.00)");
        }




        async Task<JToken>
        parseJsonResponse (Task<HttpResponseMessage> response)
        {
              var r = await response;
            r.EnsureSuccessStatusCode();
            string content = await r.Content.ReadAsStringAsync();
            
            JObject result = JObject.Parse(content);
            return result;
        }

        Task<HttpResponseMessage>
        doPost_NativeResponse (Uri uri, IEnumerable<KeyValuePair<string, string>> pairs = null)
        {
            HttpContent content = null;
            if (pairs != null)
            {
                content = new FormUrlEncodedContent(pairs);
            }

            return _httpClient.PostAsync(uri, content);
        }

        Task<HttpResponseMessage>
        doPost_NativeResponse (string uri, IEnumerable<KeyValuePair<string, string>> pairs = null)
        {
            HttpContent content = null;
            if (pairs != null)
            {
                content = new FormUrlEncodedContent(pairs);
            }

            return _httpClient.PostAsync(uri, content);
        }

        Task<JToken>
        doPost (Uri uri, IEnumerable<KeyValuePair<string, string>> pairs = null)
        {           
            return parseJsonResponse(doPost_NativeResponse(uri, pairs));
        }

        Task<JToken>
        doPost (string url, IEnumerable<KeyValuePair<string, string>> pairs = null)
        {
            return parseJsonResponse(doPost_NativeResponse(url, pairs));
        }

        Task<JToken>
        doGet (Uri uri)
        {
            var resp = _httpClient.GetAsync(uri);
            return parseJsonResponse(resp);
        }

        Task<JToken>
        doGet (string url)
        {
            var resp = _httpClient.GetAsync(url);
            return parseJsonResponse(resp);
        }
    }
}
