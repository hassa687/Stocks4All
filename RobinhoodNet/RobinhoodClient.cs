// The MIT License (MIT)
// 
// Copyright (c) 2015 Filip Frącz
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.Linq;
using Newtonsoft.Json.Linq;

namespace BasicallyMe.RobinhoodNet
{
    public partial class RobinhoodClient
    {
        Raw.RawRobinhoodClient _rawClient;


        public RobinhoodClient ()
        {
            _rawClient = new Raw.RawRobinhoodClient();
        }

        public string AuthToken
        {
            get { return _rawClient.AuthToken; }
        }

        public Task
        Authenticate (string userName, string password)
        {
            return _rawClient.Authenticate(userName, password);
        }

        public Task Authenticate (string token)
        {
            return _rawClient.Authenticate(token);
        }

        async Task<IList<TResult>>
        downloadAll<TResult>(Func<PagedResponse<TResult>.Cursor, Task<PagedResponse<TResult>>> downloadSingle)
        {
            var all = new List<TResult>();

            PagedResponse<TResult>.Cursor cursor = null;
            PagedResponse<TResult> r = null;
            do
            {
              r = await downloadSingle(cursor).ConfigureAwait(continueOnCapturedContext: false); 
                all.AddRange(r.Items);
                cursor = r.Next;

            } while (cursor != null);

            return all;
        }

        async Task<PagedResponse<TResult>>
        downloadPagedResult<TResult> (
            PagedResponse<TResult>.Cursor cursor,
            Func<string, Task<JToken>> downloader,
            Func<JToken, TResult> decoder)
        {
          var resp = await downloader(cursor == null ? null : cursor.Uri.ToString()).ConfigureAwait(continueOnCapturedContext: false); ;
            var result = new PagedJsonResponse<TResult>(resp, decoder);
            return result;
        }

        public IList<Position>
          DownloadPositions(string url, PagedResponse<Position>.Cursor cursor = null)
        {
          cursor = new PagedResponse<Position>.Cursor(url);
          List<Position> list = new List<Position>();
          while(true)
          {
            var result = downloadPagedResult<Position>(cursor, _rawClient.DownloadPositions, json => new Position(json));
            if (result.Result != null && result.Result.Items != null)
              list.AddRange(result.Result.Items);
            if (result.Result == null || result.Result.Next == null)
              break;
            cursor = result.Result.Next;
          }
          
          return list;
        }

        public Task<IList<Account>>
        DownloadAllAccounts ()
        {
            return downloadAll<Account>(this.DownloadAccounts);
        }

        public Task<PagedResponse<Account>>
        DownloadAccounts(PagedResponse<Account>.Cursor cursor = null)
        {
            return downloadPagedResult<Account>(cursor, _rawClient.DownloadAccounts, json => new Account(json));
        }


        public Task<IList<OrderSnapshot>>
        DownloadAllOrders ()
        {
            return downloadAll<OrderSnapshot>(this.DownloadOrders);
        }

        public Task<PagedResponse<OrderSnapshot>>
        DownloadOrders (PagedResponse<OrderSnapshot>.Cursor cursor = null)
        {
            return downloadPagedResult<OrderSnapshot>(cursor, _rawClient.DownloadOrders, json => new OrderSnapshot(json));
        }


        public async Task<OrderSnapshot>
        PlaceOrder (NewOrderSingle newOrderSingle)
        {
            var json = await _rawClient.PlaceOrder(newOrderSingle.ToDictionary());
            return new OrderSnapshot(json);
        }

        public Task
        CancelOrder (Url<OrderCancellation> cancellationUrl)
        {
            return _rawClient.CancelOrder(cancellationUrl.Uri.ToString());
        }


        public async Task<Instrument>
        DownloadInstrument (Url<Instrument> instrumentUrl)
        {
            var json = await _rawClient.DownloadInstrument(instrumentUrl.Uri.ToString());
            return new Instrument(json);
        }

        public async Task<IList<Instrument>>
        FindInstrument (string symbol)
        {
            var resp = await _rawClient.FindInstrument(symbol);
            var result = new PagedJsonResponse<Instrument>(resp, item => new Instrument(item));
            return result.Items;
        }

        public async Task<Quote>
        DownloadQuote (string symbol)
        {
            var q = await _rawClient.DownloadQuote(symbol);
            return new Quote(q);
        }

        public async Task<Quote>
          DownloadInstrument(string InstrumentURL)
        {
          var q = await _rawClient.DownloadInstrument(InstrumentURL);
          return new Quote(q);
        }

        public async Task<IList<Quote>>
        DownloadQuote (IEnumerable<string> symbols)
        {
            var qq = await _rawClient.DownloadQuote(symbols);

            List<Quote> quotes = new List<Quote>();
            foreach (var o in (JArray)qq)
            {
                Quote q = null;
                if (o != null && o.HasValues)
                {
                    q = new Quote(o);
                }
                quotes.Add(q);
            }

            return quotes;
        }

        public Task<IList<Quote>>
        DownloadQuote (params string[] symbols)
        {
            return DownloadQuote((IEnumerable<string>)symbols);
        }
    }
}
