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

    public enum TimeInForce
    {
        Unknown,
        GoodTillCancel,
        GoodForDay
    }

    public enum Side
    {
        Buy,
        Sell
    }

    public enum TriggerType
    {
      Immediate,
      Stop
    }

    public enum OrderType
    {
        Unknown,
        Limit,
        Market,
        StopLoss
    }

    

    public class OrderSnapshot
    {
        public decimal CumulativeQuantity { get; set; }

        public DateTime CreatedAt { get; set; }

        public string AccountId { get; set; }

        public decimal? StopPrice { get; set; }

        public string RejectReason { get; set; }

        public string OrderId { get; set; }

        public decimal? AveragePrice { get; set; }

        public TimeInForce TimeInForce { get; set; }

        public DateTime UpdatedAt { get; set; }

        public decimal? Price { get; set; }

        public decimal? TotalNotional
        {
          get
          {
            decimal result = 0;
            if (Executions != null)
            {
              foreach (Execution e in Executions)
              {
                result += (decimal)(e.Price * e.Quantity);
              }
            }

            return result;
          }

        }

        public string InstrumentId { get; set; }

        public string State { get; set; }

        public string Trigger { get; set; }

        public DateTime LastTransactionAt { get; set; }

        public decimal Fees { get; set; }

        public Url<OrderCancellation> CancelUrl { get; set; }

        // TODO: Wrap position
        public Url<Position> PositionUrl { get; set; }

        public IList<Execution> Executions { get; private set; }

        public OrderType Type { get; set; }

        public Side Side { get; set; }

        public int Quantity { get; set; }

        public OrderSnapshot ()
        {
            this.Executions = new List<Execution>();
        }

        internal OrderSnapshot (Newtonsoft.Json.Linq.JToken json) : this()
        {
            this.CumulativeQuantity = (decimal)json["cumulative_quantity"];
            this.CreatedAt = (DateTime)json["created_at"];
            this.AccountId = (string)json["account"];
            this.StopPrice = (decimal?)json["stop_price"];
            this.RejectReason = (string)json["reject_reason"];
            this.OrderId = (string)json["url"];
            this.AveragePrice = (decimal?)json["average_price"];

            this.TimeInForce = parseTif((string)json["time_in_force"]);


            this.UpdatedAt = (DateTime)json["updated_at"];
            this.Price = (decimal?)json["price"];
            this.InstrumentId = (string)json["instrument"];
            this.State = (string)json["state"];
            this.Trigger = (string)json["trigger"];
            this.LastTransactionAt = (DateTime)json["last_transaction_at"];
            this.Fees = (decimal)json["fees"];

            string url = (string)json["cancel"];
            this.CancelUrl = url != null ? new Url<OrderCancellation>(url) : null;

            url = (string)json["position"];
            this.PositionUrl = url != null ? new Url<Position>(url) : null;

            foreach (var e in (Newtonsoft.Json.Linq.JArray)json["executions"])
            {
                this.Executions.Add(new Execution(e));
            }

            this.Type = parseOrderType((string)json["type"]);
            this.Side = parseSide((string)json["side"]);
            this.Quantity = (int)(decimal)json["quantity"];
        }

        static TimeInForce parseTif (string tif)
        {
            TimeInForce result = 0;

            if (tif.Equals("gfd", StringComparison.OrdinalIgnoreCase))
            {
                result = TimeInForce.GoodForDay;
            }
            else if (tif.Equals("gtc", StringComparison.OrdinalIgnoreCase))
            {
                result = TimeInForce.GoodTillCancel;
            }

            return result;
        }

        static OrderType parseOrderType (string type)
        {
            OrderType result = 0;

            if (type.Equals("limit", StringComparison.OrdinalIgnoreCase))
            {
                result = OrderType.Limit;
            }
            else if (type.Equals("market", StringComparison.OrdinalIgnoreCase))
            {
                result = OrderType.Market;
            }

            return result;
        }

        static Side parseSide (string side)
        {
            Side result = 0;

            if (side.Equals("buy", StringComparison.OrdinalIgnoreCase))
            {
                result = Side.Buy;
            }
            else if (side.Equals("sell", StringComparison.OrdinalIgnoreCase))
            {
                result = Side.Sell;
            }

            return result;
        }       
    }
}
