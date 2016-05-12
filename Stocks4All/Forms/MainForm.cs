using BasicallyMe.RobinhoodNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using System.Xml.Serialization;
using Stocks4All.CustomControls;
using Stocks4All.Model;
using Stocks4All.Util;
using Stocks4All.Forms;
using NLog;

namespace Stocks4All
{
  public partial class MainForm : BaseForm
  {
    public Stock market, TVIX, nugt;
    int refreshTime = 2500;
    ThreadedBindingList<Stock> stocks;
    ThreadedBindingList<OrderSnapshot> AllPendingOrders;
    ThreadedBindingList<OrderSnapshot> RecentOrders;
    BindingSource bindingSource = new BindingSource();
    string filePath = Path.GetDirectoryName(Application.ExecutablePath) + @"\stocks.xml";
    volatile bool ready = false;
    public volatile bool formClosing = false;
    List<String> trailStopping = new List<string>();
    List<String> priceTargeting = new List<string>();
    Logger logger = LogManager.GetLogger("MainForm");

    #region Updater Threads
    /// <summary>
    /// Refreshes Order history/Queued orders
    /// </summary>
    /// <param name="param"></param>
    void PendingOrdersUpdater(object param)
    {
      while (true)
      {
        try
        {
          IList<OrderSnapshot> queuedOrders = new List<OrderSnapshot>();
          IList<OrderSnapshot> completedOrders = new List<OrderSnapshot>();
          IList<OrderSnapshot> cancelledOrders = new List<OrderSnapshot>();
          //get the most recent 1k orders
          var result = Robinhood.rh.DownloadOrders().Result;
          int i = 1;
          do
          {
            ThreadedBindingList<OrderSnapshot> oList = new ThreadedBindingList<OrderSnapshot>(result.Items.ToList());

            if (oList != null && oList.Any(o => o.State == "queued") || oList.Any(o => o.State == "confirmed"))
            {
              //adding confirmed orders to pending order
              ((List<OrderSnapshot>)queuedOrders).AddRange(oList.Where(o => o.State == "confirmed"));
              //adding queueed order to pending orders
              ((List<OrderSnapshot>)queuedOrders).AddRange(oList.Where(o => o.State == "queued" && !queuedOrders.Any(or => or.OrderId == o.OrderId)).ToList());

            }
            //TODO: add support for "partially_filled" orders
            if (oList != null && oList.Any(o => o.State == "filled"))
            {
              //adding filled orders
              ((List<OrderSnapshot>)completedOrders).AddRange(oList.Where(o => o.State == "filled").ToList());
            }
            if (oList != null && oList.Any(o => o.State == "cancelled"))
            {
              //adding canceled orders
              ((List<OrderSnapshot>)cancelledOrders).AddRange(oList.Where(o => o.State == "cancelled").ToList());
            }

            //removing filled and cancelled orders from pending orders
            lock (AllPendingOrders)
            {
              for (int j = 0; j < AllPendingOrders.Count(); j++)
              {
                if (cancelledOrders.Any(or => or.OrderId == AllPendingOrders[j].OrderId)
                  || completedOrders.Any(or => or.OrderId == AllPendingOrders[j].OrderId))
                  AllPendingOrders.RemoveAt(j);
              }
            }

            //From the most recent 1k orders adding opened & filled order to recent orders ui grid
            if (i == 1)
            {
              RecentOrders = new ThreadedBindingList<OrderSnapshot>(oList.Where(o => o.State != "cancelled").ToList());
              this.dataGridViewRecentOrders.BeginInvoke((Action)delegate
              {
                this.UpdateRecentOrders();
              });
            }

            //after downloading recent  3 order break loop and start from most recent order again
            if (i >= 3)
              break;
            i++; // going back 1k orders
            result = Robinhood.rh.DownloadOrders(result.Next).Result;

          } while (result.Items != null);

          //from the most recent 3k order adding open order to a list
          foreach (OrderSnapshot order in queuedOrders)
          {
            if (!AllPendingOrders.Any(o => o.OrderId == order.OrderId))
            {
              lock (AllPendingOrders)
              {
                AllPendingOrders.Add(order);
              }
            }
          }

          foreach (Stock s in stocks)
          {
            s.PendingOrders = new ThreadedBindingList<OrderSnapshot>(AllPendingOrders.Where(o => o.InstrumentId == s.InstrumentURL).ToList());
          }
        }
        catch (Exception e)
        {
        }

        //sleep for 1 second before fetching recent 3k orders again
        Thread.Sleep(1000);
      }
    }

    /// <summary>
    /// Show the recent 1k open orders in the UI grid
    /// </summary>
    void UpdateRecentOrders()
    {
      if (RecentOrders.Count() > 0)
      {
        int scrollPosition = dataGridViewRecentOrders.FirstDisplayedScrollingRowIndex;
        int rowGrid = 0;
        if (dataGridViewRecentOrders.SelectedRows != null && dataGridViewRecentOrders.SelectedRows.Count > 0)
          rowGrid = dataGridViewRecentOrders.SelectedRows[0].Index;

        dataGridViewRecentOrders.DataSource = RecentOrders;


        if (scrollPosition > 0)
          dataGridViewRecentOrders.FirstDisplayedScrollingRowIndex = scrollPosition;

        if (rowGrid != 0 && rowGrid < dataGridViewRecentOrders.Rows.Count)
          dataGridViewRecentOrders.Rows[rowGrid].Selected = true;

        foreach (DataGridViewColumn col in dataGridViewRecentOrders.Columns)
        {
          if (col.HeaderText == "StopPrice" || col.HeaderText == "Price" || col.HeaderText == "AveragePrice"
            || col.HeaderText == "TotalNotional")
          {
            col.DefaultCellStyle.Format = "c";
            col.Visible = true;
          }
          else if (col.HeaderText == "Side" || col.HeaderText == "Quantity" || col.HeaderText == "Type"
              || col.HeaderText == "State" || col.HeaderText == "AveragePrice")
            col.Visible = true;
          else
            col.Visible = false;
        }
        dataGridViewRecentOrders.Refresh();
      }
      else
        dataGridViewRecentOrders.DataSource = null;

      if (tabControlMain.TabPages[1].Text.Contains("Loading"))
        tabControlMain.TabPages[1].Text = "Recent Orders";

    }



    /// <summary>
    /// this thread updates all the quotes on the grids also downloads
    /// also download latest portfolio from RH
    /// </summary>
    /// <param name="form"></param>
    void MarketPerfomanceUpdater(object form)
    {
      MainForm mainForm = (MainForm)form;
      int i = 0;
      decimal lastprice = 0;

      while (true)
      {
        if (mainForm.formClosing)
          break;

        try
        {
          Stock[] quotes = new Stock[] {
                                            new Stock() { Ticker = mainForm.market.Ticker},
                                            new Stock() { Ticker = mainForm.TVIX.Ticker },
                                            new Stock() { Ticker = mainForm.nugt.Ticker }};


          Account account = Robinhood.rh.DownloadAllAccounts().Result.First();
          IList<Position> positions = Robinhood.rh.DownloadPositions(account.PositionsUrl.Uri.ToString());

          foreach (Position position in positions)
          {
            Stock stock = mainForm.stocks.FirstOrDefault(s => s.InstrumentURL == position.InstrumentUrl.Uri.ToString());


            if (stock == null)
            {
              if ((int)position.Quantity == 0)
                continue; //skip stocks in RH watchlist
              var q = Robinhood.rh.DownloadInstrument(position.InstrumentUrl.Uri.ToString());
              stock = new Stock() { Ticker = q.Result.Symbol, NoOfShares = (int)position.Quantity, CostBasis = position.AverageBuyPrice };

              mainForm.stocks.Add(stock);
            }
            else
            {

              stock.NoOfShares = (int)position.Quantity;
              if (stock.NoOfShares > 0)
                stock.CostBasis = position.AverageBuyPrice;
              else
                stock.CostBasis = 0.00m;
            }
          }


          if (mainForm.stocks.Count() > 0)
            quotes = mainForm.stocks.Union(quotes).ToArray();

          List<Stock> marketStocks = Robinhood.GetQuote(quotes.ToList());
          foreach (Stock stock in marketStocks)
          {
            if (stock.Ticker == market.Ticker)
              mainForm.market = stock;
            else if (stock.Ticker == TVIX.Ticker)
              mainForm.TVIX = stock;
            else if (stock.Ticker == nugt.Ticker)
              mainForm.nugt = stock;

            //relying on update recent order to add pending orders
            ThreadedBindingList<OrderSnapshot> pendingOrders = null;
            if (stock.PendingOrders != null)
            {
              lock (stock.PendingOrders)
              {
                pendingOrders = new ThreadedBindingList<OrderSnapshot>(AllPendingOrders.Where(o => o.InstrumentId == stock.InstrumentURL).ToList());
              }
            }
            else
              pendingOrders = new ThreadedBindingList<OrderSnapshot>(AllPendingOrders.Where(o => o.InstrumentId == stock.InstrumentURL).ToList());

            if (pendingOrders.Count > 0)
              stock.PendingOrders = pendingOrders;
            else
              stock.PendingOrders = null;

            mainForm.PositionsGrid.BeginInvoke((Action)delegate
            {
              lock (mainForm.stocks)
              {
                int index = mainForm.stocks.ToList()
                                        .FindIndex(s => s.Ticker == stock.Ticker);
                if (index >= 0)
                  mainForm.stocks[index] = stock;
              }
            });

            if (stock.NoOfShares > 0)
            {
              if (!mainForm.tabControlMain.TabPages[1].Text.Contains("Loading")
                              && !trailStopping.Contains(stock.Ticker))
              {

                //implementing trailing loss
                if (stock.StopLoss != null
                              && stock.StopLoss.Execution == PricePointControl.Execution.Trailing
                              && stock.StopLoss.TrailPrcntg > 0 && stock.NoOfShares > 0)
                {
                  //PT reached sell
                  if (stock.StopLoss.Price >= stock.LastTradePrice)
                    stock.StopLoss.Price = 0; //reset stop loss
                  decimal newVal = (stock.StopLoss.Price +
                    (stock.StopLoss.Price * stock.StopLoss.TrailPrcntg / 100));

                  if (stock.LastTradePrice >
                    newVal
                    ) // within 2% of price target place limit sell
                  {
                    newVal = Math.Round(stock.LastTradePrice - (stock.LastTradePrice * stock.StopLoss.TrailPrcntg / 100), 2);

                    if (stock.PendingOrders == null || (stock.PendingOrders != null && !stock.PendingOrders.Any(o => o.StopPrice <= newVal + 0.2m && o.StopPrice >= newVal - 0.2m)))
                    {
                      if (stock.NoOfShares < stock.StopLoss.NoOfShares)
                        stock.NoOfShares = stock.StopLoss.NoOfShares;
                      stock.StopLoss.Trigger = TriggerType.Stop;
                      KeyValuePair<Stock, decimal> input = new KeyValuePair<Stock, decimal>(stock, newVal);
                      lock (trailStopping)
                      {
                        trailStopping.Add(stock.Ticker);
                      }
                      ThreadPool.QueueUserWorkItem(TrailStopper, input);
                    }
                  }
                }

                //no stop loss place it if manage trade selected
                if (stock.ManageTrade && stock.NoOfShares > 0 && stock.StopLoss.Execution != PricePointControl.Execution.Trailing)
                {
                  decimal newVal;
                  decimal? currentStop = null;

                  if (stock.PendingOrders != null && stock.PendingOrders.Count() > 0)
                  {
                    currentStop = (decimal)stock.PendingOrders.First().Price;
                  }

                  //if price has moved > 2% from cost
                  newVal = Math.Round(stock.CostBasis + (stock.CostBasis * 2.10m / 100), 2);
                  if (stock.LastTradePrice > newVal)
                  {
                    //price has moved 2% from cost move stop to break even
                    newVal = Math.Round(stock.CostBasis + (stock.CostBasis * 2.00m / 100), 2);
                  }
                  else
                  {
                    //place stop loss at max
                    newVal = Math.Round(stock.CostBasis - (stock.CostBasis * stock.MaxPrctgLoss / 100), 2);
                    stock.StopLoss.StopOffset = 0.02m;
                    stock.StopLoss.Trigger = TriggerType.Stop;
                    //price has already moved at or below stop loss liquidate immiediately
                    if (stock.LastTradePrice <= newVal)
                    {
                      newVal = stock.LastTradePrice;
                      stock.StopLoss.Trigger = TriggerType.Immediate;
                    }
                  }

                  if (currentStop == null || currentStop <= newVal - 0.03m || currentStop >= newVal + 0.03m)
                  {
                    stock.StopLoss.NoOfShares = stock.NoOfShares;
                    stock.StopLoss.Price = newVal;
                    //place adjusted stop loss
                    KeyValuePair<Stock, decimal> input = new KeyValuePair<Stock, decimal>(stock, newVal);
                    lock (trailStopping)
                    {
                      trailStopping.Add(stock.Ticker);
                    }
                    ThreadPool.QueueUserWorkItem(TrailStopper, input);
                  }
                }
              }

              //Checking if Price target reached
              if (stock.PriceTarget != null && stock.NoOfShares > 0
                              && stock.PriceTarget.Price > 0
                              && stock.PriceTarget.NoOfShares > 0
                              && !priceTargeting.Contains(stock.Ticker))
              {
                //PT reached sell
                if (stock.LastTradePrice + 0.02m >= stock.PriceTarget.Price) // within 2cent of price target place limit sell
                {
                  KeyValuePair<Stock, decimal> input = new KeyValuePair<Stock, decimal>(stock, stock.PriceTarget.Price);
                  lock (priceTargeting)
                  {
                    priceTargeting.Add(stock.Ticker);
                  }
                  ThreadPool.QueueUserWorkItem(PriceTargeter, input);
                }
              }
            }
            else
            {
              //don't own this stock
              stock.CostBasis = 0.00m;
            }
          }

          //Updating market tickers
          mainForm.BeginInvoke((MethodInvoker)(() =>
          {
            mainForm.marketLabel.Text = market.Quote;
            mainForm.marketLabel.ForeColor = market.Color;
          }));

          mainForm.labelTVIX.BeginInvoke((Action)delegate
          {
            mainForm.labelTVIX.Text = TVIX.Quote;
            mainForm.labelTVIX.ForeColor = TVIX.Color;
          });
          mainForm.nugtLabel.BeginInvoke((Action)delegate
          {
            mainForm.nugtLabel.Text = nugt.Quote;
            mainForm.nugtLabel.ForeColor = nugt.Color;
          });

          //Updating account information
          mainForm.buyPowerLabel.Invoke((Action)delegate
          {
            mainForm.buyPowerLabel.Text = "Buying Power: $" + Math.Round(account.CashBalance.BuyingPower, 2).ToString();
          });

          mainForm.labelUnsettled.Invoke((Action)delegate
          {
            mainForm.labelUnsettled.Text = "Unsettled: $" + Math.Round(account.CashBalance.UnsettledFunds, 2).ToString();
          });

          mainForm.labelCash.Invoke((Action)delegate
          {
            mainForm.labelCash.Text = "Cash: $" + Math.Round(account.CashBalance.BuyingPower + account.CashBalance.UnsettledFunds + account.CashBalance.CashHeldForOrders, 2).ToString();
          });

          mainForm.buttonAddStock.Invoke((Action)delegate
          {
            mainForm.buttonAddStock.Enabled = true;
          });

          if (mainForm.PositionsGrid.Enabled == false)
          {
            mainForm.PositionsGrid.Invoke((Action)delegate
            {
              mainForm.buttonAddStock.Enabled = true;
            });
          }

          mainForm.PositionsGrid.Invoke((Action)delegate
          {
            UpdateGrids();
          });

          Thread.Sleep(refreshTime);
        }
        catch (AggregateException)
        {
          mainForm.BeginInvoke((MethodInvoker)(() =>
          {
            mainForm.marketLabel.Text = "Could not reach Robinhood, Check network connection";
            mainForm.marketLabel.ForeColor = Color.IndianRed;
            mainForm.marketLabel.BringToFront();
          }));
        }
        catch (WebException e)
        {
          MessageBox.Show(e.ToString());
        }
        catch (HttpException e)
        {
          MessageBox.Show(e.ToString());
        }
        catch (Exception e)
        {
          MessageBox.Show(e.ToString());
          logger.Error(e);
        }
      }
    }
    #endregion

    #region Placing orders

    /// <summary>
    /// function to place trail stop order in a thread
    /// </summary>
    /// <param name="param"></param>
    void TrailStopper(object param)
    {
      KeyValuePair<Stock, decimal> input = (KeyValuePair<Stock, decimal>)param;
      Stock stock = input.Key;
      decimal newVal = input.Value;
      int attempt = 10;
      while (attempt > 0)
      {
        try
        {
          //cancel stoploss
          Robinhood.CancelThrdComp = new ManualResetEvent(false);
          ThreadPool.QueueUserWorkItem(Robinhood.CancelOrder,
            new KeyValuePair<Form, ThreadedBindingList<OrderSnapshot>>(this, stock.PendingOrders));
          if (stock.PendingOrders != null && stock.PendingOrders.Count() > 0)
            Robinhood.CancelThrdComp.WaitOne();

          while (AllPendingOrders.Any(o => o.InstrumentId == stock.InstrumentURL))
          {
            Thread.Sleep(200);
          }
          stock.StopLoss.Price = newVal;
          //place adjusted stop loss
          Robinhood.PlaceOrder(stock, TimeInForce.GoodTillCancel, Robinhood.TradeStep.StopLoss, this);
          lock (trailStopping)
          {
            trailStopping.Remove(stock.Ticker);
          }
          logger.Info("Order plaed at " + newVal.ToString());

          //when order is there exit
          while (stock.PendingOrders == null)
          {
            Thread.Sleep(200);
          }
          break;

        }
        catch (Exception e)
        {
          logger.Error(e);
        }

        Thread.Sleep(1000);
        attempt--;
      }
      lock (trailStopping)
      {
        trailStopping.Remove(stock.Ticker);
      }
    }

    /// <summary>
    /// function to place price target order in a thread
    /// </summary>
    /// <param name="param"></param>
    void PriceTargeter(object param)
    {
      KeyValuePair<Stock, decimal> input = (KeyValuePair<Stock, decimal>)param;
      Stock stock = input.Key;
      decimal newVal = input.Value;
      int attempt = 10;
      while (attempt > 0)
      {
        try
        {
          //cancel stoploss
          Robinhood.CancelThrdComp = new ManualResetEvent(false);
          ThreadPool.QueueUserWorkItem(Robinhood.CancelOrder,
            new KeyValuePair<Form, ThreadedBindingList<OrderSnapshot>>(this, stock.PendingOrders));
          Robinhood.CancelThrdComp.WaitOne();

          while (AllPendingOrders.Any(o => o.InstrumentId == stock.InstrumentURL))
          {
            Thread.Sleep(200);
          }

          //place adjusted stop loss
          Robinhood.PlaceOrder(stock, TimeInForce.GoodTillCancel, Robinhood.TradeStep.ProfitTarget, this);

          stock.PriceTarget.NoOfShares = 0;
          logger.Info("Order plaed at " + newVal.ToString());
          break;

        }
        catch (Exception e)
        {
          logger.Error(e);
        }

        Thread.Sleep(1000);
        attempt--;
      }
      lock (priceTargeting)
      {
        priceTargeting.Remove(stock.Ticker);
      }
    }
    #endregion

    #region Read & Save stocks to file
    public static void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
    {
      TextWriter writer = null;
      try
      {
        if (System.IO.File.Exists(filePath))
        {
          System.IO.Directory.CreateDirectory(
               System.IO.Path.GetDirectoryName(filePath));
        }

        var serializer = new XmlSerializer(typeof(T));
        writer = new StreamWriter(filePath, append);
        serializer.Serialize(writer, objectToWrite);
      }
      catch (Exception e)
      {
        Console.Write(e.ToString());
      }
      finally
      {
        if (writer != null)
          writer.Close();
      }
    }

    public static T ReadFromXmlFile<T>(string filePath) where T : new()
    {
      TextReader reader = null;
      try
      {
        var serializer = new XmlSerializer(typeof(T));
        reader = new StreamReader(filePath);
        return (T)serializer.Deserialize(reader);
      }
      finally
      {
        if (reader != null)
          reader.Close();
      }
    }

    #endregion

    #region UI Functions
    public MainForm()
    {
      InitializeComponent();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {

    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      formClosing = true;
      if (stocks != null && stocks.Count() > 0)
        WriteToXmlFile<BindingList<Stock>>(Robinhood.__stocksFile, stocks);

      Robinhood.SignOut();
    }

    private void dataGridViewPositions_Leave(object sender, EventArgs e)
    {
    }

    private void dataGridViewPositions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      try
      {
        if (PositionsGrid.SelectedRows[0].Cells["ticker"].Value != null)
        {
          string ticker = PositionsGrid.SelectedRows[0].Cells["ticker"].Value.ToString();
          Stock selectedStock = stocks.ToList().Find(s => s.Ticker == ticker);
          HoldingForm form = new HoldingForm(refreshTime, selectedStock);
          form.ShowDialog();
          if (form.stock != null)
          {
            stocks[stocks.ToList().FindIndex(s => s.Ticker == form.stock.Ticker)] = form.stock;
          }
          UpdateGrids();
        }
      }
      catch
      {
      }
    }

    private void saveToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (stocks.Count() > 0)
        WriteToXmlFile<BindingList<Stock>>(Robinhood.__stocksFile, stocks);
    }

    private void dataGridViewPositions_DataError(object sender, DataGridViewDataErrorEventArgs e)
    {
      // MessageBox.Show("DataGridView Error " + e.Exception.ToString());
    }

    private void addStockToolStripMenuItem_Click(object sender, EventArgs e)
    {
      HoldingForm form = new HoldingForm(refreshTime);
      form.ShowDialog();
      if (form.save)
      {
        stocks.Add(form.stock);
      }
      UpdateGrids();
    }

    private void dataGridViewPositions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      e.CellStyle.SelectionBackColor = e.CellStyle.BackColor;
      e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
    }

    private void dataGridViewPositions_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
    {
      pictureBoxBGImage.Visible = false;
      int saveRow = 0;
      if (PositionsGrid.Rows.Count > 0)
        saveRow = PositionsGrid.FirstDisplayedCell.RowIndex;


      foreach (DataGridViewRow Myrow in PositionsGrid.Rows)
      {            
        DataGridViewCell quoteCell = Myrow.Cells["Quote"];
        if (quoteCell.Value != null)
        {
          string cellVal = quoteCell.Value.ToString();
          if (cellVal.Contains("-"))// Or your condition 
          {
            Myrow.Cells["ChangePctng"].Style.ForeColor = quoteCell.Style.ForeColor = Color.IndianRed;
          }
          else if (cellVal.Contains("+"))
            Myrow.Cells["ChangePctng"].Style.ForeColor = quoteCell.Style.ForeColor = Color.SeaGreen;
          else
            Myrow.Cells["ChangePctng"].Style.ForeColor = quoteCell.Style.ForeColor = Color.Black;

        }

        DataGridViewCell gainCell = Myrow.Cells["GainQuote"];
        if (gainCell.Value != null)
        {
          string cellVal = gainCell.Value.ToString();
          if (cellVal.Contains("-"))// Or your condition 
          {
            gainCell.Style.BackColor = Color.FromArgb(221, 133, 86);  //Color.IndianRed;
            gainCell.Style.ForeColor = Color.White;
          }
          else if (cellVal.Contains("+"))
          {
            gainCell.Style.BackColor = Color.FromArgb(55, 181, 133); // Color.SeaGreen;
            gainCell.Style.ForeColor = Color.White;
          }
          else
          {
            gainCell.Style.BackColor = Color.White;
            gainCell.Style.ForeColor = Color.Black;
          }
        }

        if (!PositionsGrid.Enabled)
        {
          PositionsGrid.Enabled = true;

          PositionsGrid.Columns["LastTradePrice"].DefaultCellStyle.Format = "c";
          PositionsGrid.Columns["CostBasis"].DefaultCellStyle.Format = "c";
          PositionsGrid.Columns["MarketValue"].DefaultCellStyle.Format = "c";
          PositionsGrid.Columns["ChangePctng"].DefaultCellStyle.Format = "0.00\\%";

          foreach (DataGridViewColumn column in PositionsGrid.Columns)
          {
            if (column.ValueType == typeof(decimal) || column.ValueType == typeof(int))
              column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
          }
        }

        if (saveRow != 0 && saveRow < PositionsGrid.Rows.Count)
          PositionsGrid.FirstDisplayedScrollingRowIndex = saveRow;
      }
    }

    public void PlaceOrder(string ticker, PricePoint pricePoint, TimeInForce tif)
    {
      Account account = Robinhood.rh.DownloadAllAccounts().Result.First();

      Instrument instrument = null;
      while (instrument == null)
      {
        try
        {
          instrument = Robinhood.rh.FindInstrument(ticker.ToUpperInvariant()).Result.First(i => i.Symbol == ticker);
        }
        catch (Exception e)
        {
          MessageBox.Show("Problem. Try again. " + e.Message);
        }
      }

      var newOrderSingle = new NewOrderSingle(instrument);
      newOrderSingle.AccountUrl = account.AccountUrl;
      newOrderSingle.Quantity = Math.Abs(pricePoint.NoOfShares);
      newOrderSingle.Side = pricePoint.NoOfShares > 0 ? Side.Buy : Side.Sell;

      newOrderSingle.TimeInForce = tif;
      if (pricePoint.Price == 0)
      {
        newOrderSingle.OrderType = OrderType.Market;
      }
      else
      {
        newOrderSingle.OrderType = OrderType.Limit;
        newOrderSingle.Price = pricePoint.Price;
      }

      var order = Robinhood.rh.PlaceOrder(newOrderSingle).Result;
    }

    private void buttonAddStock_Click(object sender, EventArgs e)
    {
      AddStock();
    }

    private void dataGridViewPositions_Resize(object sender, EventArgs e)
    {
      pictureBoxBGImage.Left = (this.Width) / 2 - pictureBoxBGImage.Width;
      pictureBoxBGImage.Top = (this.Height) / 2 - pictureBoxBGImage.Height;
    }

    private void CenterPictureBox(PictureBox picBox, Bitmap picImage)
    {
      picBox.Location = new Point((picBox.Parent.ClientSize.Width / 2) - (picImage.Width / 2),
                                  (picBox.Parent.ClientSize.Height / 2) - (picImage.Height / 2));
      picBox.Refresh();
    }

    private void addStockToolStripMenuItemAddStock_Click(object sender, EventArgs e)
    {
      AddStock();
    }
    void AddStock()
    {
      HoldingForm form = new HoldingForm(refreshTime);
      form.ShowDialog();
      if (form.save)
      {
        if (!this.stocks.Any(s => s.Ticker == form.stock.Ticker))
          stocks.Add(form.stock);
        else
          MessageBox.Show("Stock already in watchlist or holdings");

      }
      UpdateGrids();
      if (stocks.Count() > 0)
        WriteToXmlFile<BindingList<Stock>>(Robinhood.__stocksFile, stocks);
    }
    void UpdateGrids()
    {
      try
      {
        int scrollPosition = 0;
        int rowPositionsGrid = 0, rowWatchList = 0;

        //Getting sort order
        var propertyInfo = typeof(Stock).GetProperty(stocks.sortProperty);
        List<Stock> sortedPositions = stocks.OrderBy(x => x.NoOfShares).ToList();
        List<Stock> sortedWatch = stocks.OrderBy(x => x.NoOfShares).ToList();

        //Saving selected row index
        if (PositionsGrid.Rows.Count > 0 && PositionsGrid.SelectedRows != null && PositionsGrid.SelectedRows.Count > 0)
          rowPositionsGrid = PositionsGrid.SelectedRows[0].Index;
        if (WatchListGrid.Rows.Count > 0 && WatchListGrid.SelectedRows != null && WatchListGrid.SelectedRows.Count > 0)
          rowWatchList = WatchListGrid.SelectedRows[0].Index;

        //if list is sorted ascending
        if (stocks.sortDirection == ListSortDirection.Ascending)
        {
          scrollPosition = PositionsGrid.FirstDisplayedScrollingRowIndex;
          PositionsGrid.DataSource = new ThreadedBindingList<Stock>(sortedPositions.Where(s => s.NoOfShares > 0)
                                                                    .OrderBy(x => propertyInfo.GetValue(x, null))
                                                                    .ToList());
          if (scrollPosition > 0)
            PositionsGrid.FirstDisplayedScrollingRowIndex = scrollPosition;

          scrollPosition = WatchListGrid.FirstDisplayedScrollingRowIndex;
          WatchListGrid.DataSource = new ThreadedBindingList<Stock>(sortedWatch.Where(s => s.NoOfShares == 0)
                                                                    .OrderBy(x => propertyInfo.GetValue(x, null))
                                                                    .ToList());
          if (scrollPosition > 0)
            WatchListGrid.FirstDisplayedScrollingRowIndex = scrollPosition;
        }
        else //if list is sorted desending 
        {

          scrollPosition = PositionsGrid.FirstDisplayedScrollingRowIndex;
          PositionsGrid.DataSource = new ThreadedBindingList<Stock>(sortedPositions.Where(s => s.NoOfShares > 0)
                                                                    .OrderByDescending(x => propertyInfo.GetValue(x, null))
                                                                    .ToList());
          if (scrollPosition > 0)
            PositionsGrid.FirstDisplayedScrollingRowIndex = scrollPosition;

          scrollPosition = WatchListGrid.FirstDisplayedScrollingRowIndex;
          WatchListGrid.DataSource = new ThreadedBindingList<Stock>(sortedWatch.Where(s => s.NoOfShares == 0)
                                                                    .OrderByDescending(x => propertyInfo.GetValue(x, null))
                                                                    .ToList());
          WatchListGrid.DataSource = new ThreadedBindingList<Stock>(sortedWatch.Where(s => s.NoOfShares == 0)
                                                                    .OrderBy(x => propertyInfo.GetValue(x, null))
                                                                    .ToList());
          if (scrollPosition > 0)
            WatchListGrid.FirstDisplayedScrollingRowIndex = scrollPosition;
        }

        if (rowPositionsGrid != 0 && rowPositionsGrid < PositionsGrid.Rows.Count)
          PositionsGrid.Rows[rowPositionsGrid].Selected = true;

        if (rowWatchList != 0 && rowWatchList < WatchListGrid.Rows.Count)
          WatchListGrid.Rows[rowWatchList].Selected = true;
      }
      catch { }
    }

    private void saveStocksToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (stocks.Count() > 0)
        WriteToXmlFile<BindingList<Stock>>(Robinhood.__stocksFile, stocks);
    }

    private void WatchListGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      e.CellStyle.SelectionBackColor = e.CellStyle.BackColor;
      e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
    }

    private void WatchListGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      try
      {
        if (WatchListGrid.SelectedRows[0].Cells["ticker"].Value != null)
        {
          string ticker = WatchListGrid.SelectedRows[0].Cells["ticker"].Value.ToString();
          Stock selectedStock = stocks.ToList().Find(s => s.Ticker == ticker);
          HoldingForm form = new HoldingForm(refreshTime, selectedStock);
          form.ShowDialog();
          if (form.stock != null)
          {
            stocks[stocks.ToList().FindIndex(s => s.Ticker == form.stock.Ticker)] = form.stock;
          }
          UpdateGrids();
        }
      }
      catch
      {
      }
    }

    private void WatchListGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
    {
      foreach (DataGridViewRow Myrow in WatchListGrid.Rows)
      {      
        DataGridViewCell quoteCell = Myrow.Cells["Quote"];
        if (quoteCell.Value != null)
        {
          string cellVal = quoteCell.Value.ToString();
          if (cellVal.Contains("-"))// Or your condition 
          {
            Myrow.Cells["ChangePctng"].Style.ForeColor = quoteCell.Style.ForeColor = Color.IndianRed;
          }
          else if (cellVal.Contains("+"))
            Myrow.Cells["ChangePctng"].Style.ForeColor = quoteCell.Style.ForeColor = Color.SeaGreen;
          else
            Myrow.Cells["ChangePctng"].Style.ForeColor = quoteCell.Style.ForeColor = Color.Black;
        }

        DataGridViewCell gainCell = Myrow.Cells["GainQuote"];
        if (gainCell.Value != null)
        {
          string cellVal = gainCell.Value.ToString();
          if (cellVal.Contains("-"))// Or your condition 
          {
            gainCell.Style.BackColor = Color.FromArgb(221, 133, 86);  //Color.IndianRed;
            gainCell.Style.ForeColor = Color.White;
          }
          else if (cellVal.Contains("+"))
          {
            gainCell.Style.BackColor = Color.FromArgb(55, 181, 133); // Color.SeaGreen;
            gainCell.Style.ForeColor = Color.White;
          }
          else
          {
            gainCell.Style.BackColor = Color.White;
            gainCell.Style.ForeColor = Color.Black;
          }
        }

        if (!WatchListGrid.Enabled)
        {
          WatchListGrid.Enabled = true;

          WatchListGrid.Columns["LastTradePrice"].DefaultCellStyle.Format = "c";
          WatchListGrid.Columns["CostBasis"].DefaultCellStyle.Format = "c";
          WatchListGrid.Columns["MarketValue"].DefaultCellStyle.Format = "c";
          WatchListGrid.Columns["ChangePctng"].DefaultCellStyle.Format = "0.00\\%";

          foreach (DataGridViewColumn column in PositionsGrid.Columns)
          {
            if (column.ValueType == typeof(decimal) || column.ValueType == typeof(int))
              column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
          }
        }
      }
    }


    private void WatchListGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
    {

    }

    private void WatchListGrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      stocks.sortProperty = WatchListGrid.Columns[e.ColumnIndex].DataPropertyName;
      if (stocks.sortDirection == ListSortDirection.Ascending)
        stocks.sortDirection = ListSortDirection.Descending;
      else
        stocks.sortDirection = ListSortDirection.Ascending;

      UpdateGrids();

    }

    private void PositionsGrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      stocks.sortProperty = PositionsGrid.Columns[e.ColumnIndex].DataPropertyName;
      if (stocks.sortDirection == ListSortDirection.Ascending)
        stocks.sortDirection = ListSortDirection.Descending;
      else
        stocks.sortDirection = ListSortDirection.Ascending;

      UpdateGrids();
    }

    private void MainForm_Shown(object sender, EventArgs e)
    {

      LoginForm loginForm = new LoginForm();
      loginForm.ShowDialog();
      if (loginForm.cancelFlag)
        Application.Exit();

      dataGridViewPositions_Resize(null, null);
      pictureBoxBGImage.BringToFront();

      market = new Stock("SPY");
      TVIX = new Stock("TVIX");
      nugt = new Stock("NUGT");
      if (File.Exists(Robinhood.__stocksFile))
        stocks = ReadFromXmlFile<ThreadedBindingList<Stock>>(Robinhood.__stocksFile);
      else
        stocks = new ThreadedBindingList<Stock>();
      AllPendingOrders = new ThreadedBindingList<OrderSnapshot>();
      RecentOrders = new ThreadedBindingList<OrderSnapshot>();
      stocks.sortProperty = "Ticker";
      ThreadPool.QueueUserWorkItem(PendingOrdersUpdater, null);
      ThreadPool.QueueUserWorkItem(MarketPerfomanceUpdater, this);
      ready = true;
    }

    private void reportBugToolStripMenuItem_Click(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://meethassan.net/contact-me/");
    }

    private void WatchListGrid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
    {
      try
      {
        string test = e.Row.Cells[0].Value.ToString();
        Stock stock = this.stocks.First(s => s.Ticker == test);
        lock (this.stocks)
        {
          this.stocks.Remove(stock);
        }
      }
      catch
      {
      }
    }

    private void contactSupportToolStripMenuItem_Click(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://meethassan.net/contact-me/");
    }

#endregion
  }

}
