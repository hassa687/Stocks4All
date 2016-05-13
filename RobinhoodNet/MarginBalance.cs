using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicallyMe.RobinhoodNet
{
	public class MarginBalance : Balance
	{
		internal MarginBalance(Newtonsoft.Json.Linq.JToken json)
		{
			this.CreatedAt = (DateTime)json["created_at"];
			this.UpdatedAt = (DateTime)json["updated_at"];

			this.CashHeldForOrders = (decimal)json["cash_held_for_orders"];

			this.Cash = (decimal)json["cash"];
			this.BuyingPower = (decimal)json["day_trade_buying_power"];

			this.CashAvailableForWithdrawal = (decimal)json["cash_available_for_withdrawal"];
			this.UnclearedDeposits = (decimal)json["uncleared_deposits"];
			//this.UnsettledFunds = (decimal)json["unsettled_funds"];
		}
	}
}
