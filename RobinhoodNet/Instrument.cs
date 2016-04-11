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

namespace BasicallyMe.RobinhoodNet
{
    public class Instrument
    {
        // TODO: Wrap splits
        public Url<int>  InstrumentSplitsUrl { get; set; }

        public decimal MarginInitialRatio { get; set; }

        public Url<Instrument> InstrumentUrl { get; set; }

        public Url<Quote> QuoteUrl { get; set; }

        public string Symbol { get; set; }

        public string BloombergUnique { get; set; }

        // TODO: Wrap fundamentals
        public Url<InstrumentFundamentals> InstrumentFundamentalsUrl { get; set; }

        public string State { get; set; }

        public bool IsTradeable { get; set; }

        public decimal MaintenanceRatio { get; set; }

        // TODO: Wrap market
        public Url<Market> MarketUrl { get; set; }

        public string Name { get; set; }


        public Instrument ()
        {

        }

        internal Instrument (Newtonsoft.Json.Linq.JToken json)
        {
            // "splits": "https:\\/\\/api.robinhood.com\\/instruments\\/376ca781-1333-40ca-bd61-a48f46ebd950\\/splits\\/",
            this.InstrumentSplitsUrl = new Url<int>((string)json["splits"]);

            //"margin_initial_ratio": "0.5000",
            this.MarginInitialRatio = (decimal)json["margin_initial_ratio"];

            //"url": "https:\\/\\/api.robinhood.com\\/instruments\\/376ca781-1333-40ca-bd61-a48f46ebd950\\/",
            this.InstrumentUrl = new Url<Instrument>((string)json["url"]);

            //"quote": "https:\\/\\/api.robinhood.com\\/quotes\\/PEP\\/",
            this.QuoteUrl = new  Url<Quote>((string)json["quote"]);

            //"symbol": "PEP",
            this.Symbol = (string)json["symbol"];

            //"bloomberg_unique": "EQ0010115800001000",
            this.BloombergUnique = (string)json["bloomberg_unique"];

            //"fundamentals": "https:\\/\\/api.robinhood.com\\/fundamentals\\/PEP\\/",
            this.InstrumentFundamentalsUrl = new Url<InstrumentFundamentals>((string)json["fundamentals"]);

            //"state": "active",
            this.State = (string)json["state"];

            //"tradeable": true,
            this.IsTradeable = (bool)json["tradeable"];

            //"maintenance_ratio": "0.2500",
            this.MaintenanceRatio = (decimal)json["maintenance_ratio"];

            //"market": "https:\\/\\/api.robinhood.com\\/markets\\/XNYS\\/",
            this.MarketUrl = new Url<Market>((string)json["market"]);

            //"name": "PepsiCo Inc."
            this.Name = (string)json["name"];
        }
    }


}
