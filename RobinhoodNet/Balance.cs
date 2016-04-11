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
    public class Balance
    {
        public DateTime CreatedAt                           { get; set; }
        public DateTime UpdatedAt                           { get; set; }

        public decimal CashHeldForOrders                    { get; set; }        
        public decimal Cash                                 { get; set; }
        public decimal BuyingPower                          { get; set; }        
        public decimal CashAvailableForWithdrawal           { get; set; }
        public decimal UnclearedDeposits                    { get; set; }
        public decimal UnsettledFunds                       { get; set; }

        public Balance()
        {

        }

        internal Balance(Newtonsoft.Json.Linq.JToken json)
        {
            this.CreatedAt = (DateTime)json["created_at"];
            this.UpdatedAt = (DateTime)json["updated_at"];

            this.CashHeldForOrders = (decimal)json["cash_held_for_orders"];

            this.Cash        = (decimal)json["cash"];
            this.BuyingPower = (decimal)json["buying_power"];

            this.CashAvailableForWithdrawal = (decimal)json["cash_available_for_withdrawal"];
            this.UnclearedDeposits          = (decimal)json["uncleared_deposits"];
            this.UnsettledFunds             = (decimal)json["unsettled_funds"];
        }
    }
    
}
