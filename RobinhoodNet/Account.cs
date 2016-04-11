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

    public class Account
    {
        public bool IsDeactivated { get; set; }
        public bool IsWithdrawalHalted { get; set; }
        public bool IsSweepEnabled { get; set; }
        public bool OnlyPositionClosingTrades { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string AccountType { get; set; }

        // TODO: What is this? Separately managed accounts?
        public dynamic Sma { get; set; }  // TODO: What is this?
        public dynamic SmaHeldForOrders { get; set; }

        public dynamic MarginBalances { get; set; }

        
        public Balance CashBalance { get; set; }

        public Url<AccountPortfolio> PortfolioUrl { get; set; }

        public Url<User> UserUrl { get; set; }

        public Url<Account> AccountUrl { get; set; }
        public Url<AccountPositions> PositionsUrl { get; set; }

        public string AccountNumber { get; set; }

        public decimal MaxAchEarlyAccessAmount { get; set; }

        public Account()
        {
            CashBalance = new Balance();
        }

        internal Account(Newtonsoft.Json.Linq.JToken json) : this()
        {
            IsDeactivated             = (bool)json["deactivated"];
            IsWithdrawalHalted        = (bool)json["withdrawal_halted"];
            IsSweepEnabled            = (bool)json["sweep_enabled"];
            OnlyPositionClosingTrades = (bool)json["only_position_closing_trades"];

            UpdatedAt = (DateTime)json["updated_at"];

            AccountUrl    = new Url<Account>((string)json["url"]);
            PortfolioUrl  = new  Url<AccountPortfolio>((string)json["portfolio"]);
            UserUrl       = new Url<User>((string)json["user"]);
            PositionsUrl  = new  Url<AccountPositions>((string)json["positions"]);

            AccountNumber = (string)json["account_number"];
            AccountType   = (string)json["type"];

            Sma = json["sma"];
            SmaHeldForOrders = json["sma_held_for_orders"];

            MarginBalances = json["margin_balances"];

            MaxAchEarlyAccessAmount = (decimal)json["max_ach_early_access_amount"];

            CashBalance = new Balance(json["cash_balances"]);
        }
    }
    
}
