using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace BasicallyMe.RobinhoodNet.Raw
{
    public partial class RawRobinhoodClient
    {
        
        public Task<JToken>
        DownloadInstrument (string instrumentUrl)
        {
            return doGet(instrumentUrl);
        }
        
        public Task<JToken>
        FindInstrument (string symbol)
        {
            var b = new UriBuilder(INSTRUMENTS_URL);
            b.Query = "query=" + symbol;
            return doGet(b.Uri);
        }
        
        public Task<JToken>
        DownloadInstrumentFundamentals (string symbol)
        {
            var b = new UriBuilder(FUNDAMENTALS_URL);
            b.Path = symbol + "/";
            return doGet(b.Uri);
        }

        public async Task<JToken>
        DownloadQuote (string symbol)
        {
            var b = new UriBuilder(QUOTES_URL);
            b.Query = "symbols=" + symbol;

            var json = await doGet(b.Uri);
            return json["results"][0];
        }

        public async Task<JToken>
        DownloadQuote (IEnumerable<string> symbols)
        {
            var b = new UriBuilder(QUOTES_URL);
            b.Query = "symbols=" + String.Join(",", symbols);

            var json = await doGet(b.Uri);

            return json["results"];
        }

        public Task<JToken>
        DownloadQuote (params string[] symbols)
        {
            return DownloadQuote((IEnumerable<string>)symbols);
        }
    }
}
