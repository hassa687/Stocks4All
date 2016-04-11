using BasicallyMe.RobinhoodNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocks4All.Model;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Net;
using System.Web;
using DiffuseDlgDemo;
using Stocks4All.CustomControls;

namespace Stocks4All.Util
{
  public static class Robinhood
  {
    public static RobinhoodClient rh;
    public static volatile bool lastOrderSuccess = false;
    public static ManualResetEvent CancelThrdComp = new ManualResetEvent(false);
    public static ManualResetEvent PlacingOrder = new ManualResetEvent(false);
    public static Account rhAccount;
    static readonly string __tokenFile = System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Stocks4All",
            "token");
    public static readonly string __stocksFile = System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Stocks4All",
            "stocks");

    public enum TradeStep
    {
      Entry,
      ProfitTarget,
      StopLoss
    }

    public static void Init(string userName, string pwd)
    {
      rh = new RobinhoodClient();
      authenticate(rh, userName, pwd).Wait();
    }

    public static List<OrderSnapshot> GetAllPendingOrders()
    {
      IList<OrderSnapshot> orders = new List<OrderSnapshot>();
      var result = Robinhood.rh.DownloadOrders().Result;
      do
      {
        List<OrderSnapshot> oList = result.Items.ToList();
        if (oList != null && oList.Any(o => o.State == "queued"))
        {
          ((List<OrderSnapshot>)orders).AddRange(oList.Where(o => o.State == "queued").ToList());
        }
        else
          break;

        result = Robinhood.rh.DownloadOrders(result.Next).Result;

      } while (result.Items != null);

      return orders.ToList();
    }

    public static void CancelOrder(object param)
    {
      CancelThrdComp.Reset();
      KeyValuePair<Form, ThreadedBindingList<OrderSnapshot>> input = (KeyValuePair<Form, ThreadedBindingList<OrderSnapshot>>)param;
      ThreadedBindingList<OrderSnapshot> orders;
      Notification notifForm = new Notification();
      while (true)
      {
        try
        {
          orders = (ThreadedBindingList<OrderSnapshot>)input.Value;
          lock (orders)
          {
            foreach (OrderSnapshot order in orders)
            {
              Robinhood.rh.CancelOrder(order.CancelUrl);
            }
            //orders.Clear();
          }
          break;
        }
        catch
        {
          //input.Key.Invoke((MethodInvoker)delegate()
          //{
          //  notifForm.label1.Text = "Order Cancellation Failed";
          //  notifForm.Show();
          //});
          Thread.Sleep(1250);
        }
      }

      try
      {
        input.Key.Invoke((MethodInvoker)delegate ()
        {
          notifForm.label1.Text = "Order Cancellation Sent";
          notifForm.Show();
        });
      }
      catch { }
      CancelThrdComp.Set();
    }

    public static Account GetAccount()
    {
      if (rhAccount == null)
        rhAccount = rh.DownloadAllAccounts().Result.First();

      return rhAccount;
    }
    public static void SignOut()
    {
      if (System.IO.File.Exists(__tokenFile))
      {
        File.Delete(__tokenFile);
      }
    }
    static async Task authenticate(RobinhoodClient client, string userName, string pwd)
    {
      if (System.IO.File.Exists(__tokenFile))
      {
        var token = System.IO.File.ReadAllText(__tokenFile);
        await client.Authenticate(token);
      }
      else
      {
        //Console.Write("username: ");
        //string userName = "jamiator21"; //Console.ReadLine();
        //Console.WriteLine("username: jamiator21");
        // Console.Write("password: ");
        //string password = "Complicated12";// getConsolePassword();

        await client.Authenticate(userName, pwd);

        System.IO.Directory.CreateDirectory(
            System.IO.Path.GetDirectoryName(__tokenFile));

        System.IO.File.WriteAllText(__tokenFile, client.AuthToken);
      }
    }

    public static List<Stock> GetQuote(List<Stock> stocks = null)
    {
      List<Stock> newList = new List<Stock>();
      if (stocks.Count() > 0 && rh != null)
      {
        try
        {
          var quotes = rh.DownloadQuote(stocks.Select(s => s.Ticker)).Result;

          //if (stocks == null)
          //  stocks = new List<Stock>();

          foreach (var q in quotes)
          {
            if (q == null)
            {
              continue;
            }
            Instrument instrument = rh.FindInstrument(q.Symbol).Result.First(i => i.Symbol == q.Symbol);

            Stock stock = stocks.Find(s => s.Ticker == q.Symbol);

            if (stock != null)
            {

            }
            else
              stock = new Stock();

            stock.Ticker = q.Symbol;
            //if (stock.Ticker != "AAPL" || stock.LastTradePrice < q.LastTradePrice)
              stock.LastTradePrice = Math.Round(q.LastTradePrice, 2);
            //if (stock.Ticker != "AAPL" )
            

            stock.ChangePctng = Math.Round(q.ChangePercentage, 2);
            stock.AskPrice = Math.Round(q.AskPrice, 2);
            stock.BidPrice = Math.Round(q.BidPrice, 2);
            stock.InstrumentURL = instrument.InstrumentUrl.Uri.AbsoluteUri;
            string chngSym = string.Empty;

            if (q.ChangePercentage < 0)
            {
              stock.Color = Color.IndianRed;
              stock.ChngSym = "-";
            }
            else
            {
              stock.Color = Color.SeaGreen;
              stock.ChngSym = "+";
            }

            //return string.("{0}: ${1} ({2}{3}%)", stock.Ticker,
            //    lastTradePrice.ToString(),
            //    chngSym,
            //    changePctng.ToString());

            newList.Add(stock);
          }
          //NotifyPropertyChanged();
          return newList;
        }
        catch (Exception e)
        {
          return newList;
          //NotifyPropertyChanged();
        }
      }
      else
      {
        return newList;
        // NotifyPropertyChanged();
      }
    }

    public static void CancelOrder()
    {
      //Console.WriteLine("{0}\t{1}\t{2} x {3}\t{4}",
      //                          order.Side,
      //                          instrument.Symbol,
      //                          order.Quantity,
      //                          order.Price.HasValue ? order.Price.ToString() : "mkt",
      //                          order.State);
      //if (!String.IsNullOrEmpty(order.RejectReason))
      //{
      //  Console.WriteLine(order.RejectReason);
      //}
      //else
      //{
      //  Console.WriteLine("Press C to cancel this order, or anything else to quit");
      //  var x = Console.ReadKey();
      //  if (x.KeyChar == 'c' || x.KeyChar == 'C')
      //  {
      //    rh.CancelOrder(order.CancelUrl).Wait();
      //    Console.WriteLine("Cancelled");
      //  }
      //}
    }
    public static void PlaceOrder(Stock stock, TimeInForce tif, TradeStep tradeStep, Form parentForm)
    {
      string errString = string.Empty;
      PricePoint pricePoint = null;
      var newOrderSingle = new NewOrderSingle();
      try
      {
        Account account = rh.DownloadAllAccounts().Result.First();
        //if (pricePoint.PendingOrders == null)
        //  pricePoint.PendingOrders = new Dictionary<decimal, String>();
        if (tradeStep == TradeStep.Entry)
          pricePoint = stock.Entry;
        else if (tradeStep == TradeStep.ProfitTarget)
        {
          pricePoint = stock.PriceTarget;
          if(pricePoint.NoOfShares>0)
          pricePoint.NoOfShares *= -1;
        }
        else if (tradeStep == TradeStep.StopLoss)
        {
          if (stock.PendingOrders != null && stock.PendingOrders.Any(o => o.Side == Side.Sell && o.Trigger == "stop"))
          {
            ThreadPool.QueueUserWorkItem(Robinhood.CancelOrder,
              new KeyValuePair<Form, ThreadedBindingList<OrderSnapshot>>(parentForm, stock.PendingOrders));
          }

          while(stock.PendingOrders!=null && stock.PendingOrders.Count>0)
          {
            Thread.Sleep(1000);
          }

          pricePoint = stock.StopLoss;
          if (pricePoint.NoOfShares > 0)
          pricePoint.NoOfShares *= -1;
        }
        else
          pricePoint = null;

        Instrument instrument = null;
        while (instrument == null)
        {
          try
          {
            instrument = rh.FindInstrument(stock.Ticker.ToUpperInvariant()).Result.First(i => i.Symbol == stock.Ticker);
          }
          catch (Exception e)
          {
            MessageBox.Show("Problem. Try again. " + e.Message);
          }
        }

        lastOrderSuccess = false;
        if (pricePoint.Execution == CustomControls.PricePointControl.Execution.Limit
          || pricePoint.Execution == CustomControls.PricePointControl.Execution.Trailing)
        {
          newOrderSingle = new NewOrderSingle(instrument);
          newOrderSingle.AccountUrl = account.AccountUrl;
          newOrderSingle.TimeInForce = tif;
          newOrderSingle.Side = pricePoint.NoOfShares > 0 ? Side.Buy : Side.Sell;
          newOrderSingle.Quantity = Math.Abs(pricePoint.NoOfShares);
          newOrderSingle.OrderType = (OrderType)Enum.Parse(typeof(OrderType), pricePoint.Type.ToString());
          newOrderSingle.Trigger = pricePoint.Trigger;
          //if (pricePoint.Value == 0)
          //{
          //  //newOrderSingle.OrderType = OrderType.Market;
          //}
          //else
          //{
          //newOrderSingle.OrderType = OrderType.Limit;
          if (pricePoint.Trigger == TriggerType.Stop)
          {
            //newOrderSingle.OrderType = OrderType.Market;
            //newOrderSingle.Trigger = "stop";
            if (newOrderSingle.OrderType == OrderType.Limit)
              newOrderSingle.Price = pricePoint.Price;
            else if (newOrderSingle.OrderType == OrderType.Market)
              newOrderSingle.Price = stock.LastTradePrice;

            newOrderSingle.StopPrice = pricePoint.Price + pricePoint.StopOffset;
          }
          else if (pricePoint.Trigger == TriggerType.Immediate)
            newOrderSingle.Price = pricePoint.Price;
          //}

          var order = rh.PlaceOrder(newOrderSingle).Result;
          PlacingOrder.Set();

          if (order.State == "queued")
            lastOrderSuccess = true;
          // var test = rh.DownloadAllOrders().Result;
          //test.Start();
          //test.Wait();
          //pricePoint.PendingOrders.Add(pricePoint.Value, order.CancelUrl.Uri.AbsoluteUri.First());
        }
        else if (pricePoint.Execution == CustomControls.PricePointControl.Execution.Spread)
        {
          int noOfShare = pricePoint.NoOfShares > 0 ? 1 : -1;
          foreach (decimal orderValue in pricePoint.ExecutionSpread)
          {
            newOrderSingle = new NewOrderSingle(instrument);
            newOrderSingle.AccountUrl = account.AccountUrl;
            newOrderSingle.TimeInForce = tif;
            newOrderSingle.Side = pricePoint.NoOfShares > 0 ? Side.Buy : Side.Sell;
            newOrderSingle.OrderType = (OrderType)Enum.Parse(typeof(OrderType), pricePoint.Type.ToString());
            newOrderSingle.Trigger = pricePoint.Trigger;
            newOrderSingle.Quantity = Math.Abs(noOfShare);
            if (pricePoint.Trigger == TriggerType.Stop)
            {
              if (newOrderSingle.OrderType == OrderType.Limit)
                newOrderSingle.Price = orderValue;
              newOrderSingle.StopPrice = orderValue + 0.02m;
            }
            else if (pricePoint.Trigger == TriggerType.Immediate)
              newOrderSingle.Price = orderValue;

            var order = rh.PlaceOrder(newOrderSingle).Result;
            //pricePoint.PendingOrders.Add(pricePoint.Value, order.CancelUrl);
          }
        }

        Notification notifForm = new Notification();
        parentForm.Invoke((MethodInvoker)delegate()
        {
          notifForm.label1.Text = string.Format("{0} {1} {2} Order Sent for {3} shares at {4}",
            stock.Ticker, newOrderSingle.Side, newOrderSingle.OrderType,
            newOrderSingle.Quantity, newOrderSingle.Price);
          notifForm.Show();
        });
      }
      catch (WebException e)
      {
        if (pricePoint.Execution != CustomControls.PricePointControl.Execution.Trailing
          && !stock.ManageTrade)
        {
          if (pricePoint.NoOfShares > 0)
            errString = String.Format("Error Placing Buy Order for {0}, Check network connection", stock.Ticker);
          else
            errString = String.Format("Error Placing Sell Order for {0}, Check network connection", stock.Ticker);

          Notification notifForm = new Notification();
          parentForm.Invoke((MethodInvoker)delegate ()
          {
            notifForm.label1.Text = string.Format(errString);
            notifForm.Show();
          });
        }
      }
      catch (HttpException e)
      {
        if (pricePoint.Execution != CustomControls.PricePointControl.Execution.Trailing
          && !(stock.ManageTrade && tradeStep == TradeStep.StopLoss))
        {
          if (pricePoint.NoOfShares > 0)
            errString = String.Format("Error Placing Buy Order for {0}, Check network connection", stock.Ticker);
          else
            errString = String.Format("Error Placing Sell Order for {0}, Check network connection", stock.Ticker);

          Notification notifForm = new Notification();
          parentForm.Invoke((MethodInvoker)delegate ()
          {
            notifForm.label1.Text = string.Format(errString);
            notifForm.Show();
          });
        }
      }
      catch
      {
        if (pricePoint.Execution != CustomControls.PricePointControl.Execution.Trailing
          && !(stock.ManageTrade && tradeStep == TradeStep.StopLoss))
        {
          if (pricePoint.NoOfShares > 0)
            errString = String.Format("Error Placing Buy Order for {0}, Check buying power", stock.Ticker);
          else
            errString = String.Format("Error Placing Sell Order for {0}, Make sure you have enough shares available", stock.Ticker);

          Notification notifForm = new Notification();
          parentForm.Invoke((MethodInvoker)delegate ()
          {
            notifForm.label1.Text = string.Format(errString);
            notifForm.Show();
          });
        }

      }
    }
  }
}
