using BasicallyMe.RobinhoodNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Stocks4All.CustomControls;
using Stocks4All.Model;
using Stocks4All.Util;

namespace Stocks4All
{
  public partial class HoldingForm : BaseForm
  {
    //RobinhoodClient rh;
    public Stock stock = null;

    int refreshTime = 2000;
    volatile bool stopThreads = false;
    public bool save = false;
    bool shown = false;
    public HoldingForm(int refreshTime, Stock stock = null)
    {
      try
      {
        InitializeComponent();
        this.Opacity = 0.99;
        timer1.Interval = 10;
        timer1.Enabled = true;
        timer1.Tick += timer1_Tick;

        Entry.ExecutionSpreadSelected += pricePointControlEntry_ExecutionSpreadSelected;
        Entry.numericUpDownNoOfShares.ValueChanged += numericUpDownNoOfShares_ValueChanged;
        Entry.PlaceOrder += Entry_PlaceOrder;
        Entry.TriggerSelected += Entry_TriggerSelected;

        PriceTarget.ExecutionSpreadSelected += pricePointControlPriceTarget_ExecutionSpreadSelected;
        PriceTarget.PlaceOrder += PriceTarget_PlaceOrder;
        PriceTarget.TriggerSelected += PriceTarget_TriggerSelected;

        Stop.ExecutionSpreadSelected += pricePointControlStop_ExecutionSpreadSelected;
        Stop.PlaceOrder += Stop_PlaceOrder;
        Stop.TriggerSelected += Stop_TriggerSelected;

        Entry.FollowSelected += Entry_FollowSelected;
        PriceTarget.FollowSelected += PriceTarget_FollowSelected;
        Stop.FollowSelected += Stop_FollowSelected;
        this.refreshTime = refreshTime;
        textBoxTicker.DelayedTextChangedTimeout = 1000;
        textBoxTicker.DelayedTextChanged += textBoxTicker_DelayedTextChanged;

        if (stock == null)
        {
          textBoxTicker.Enabled = true;
          this.stock = new Stock("");

        }
        else
        {
          textBoxTicker.Enabled = false;
          this.stock = stock;
        }

        textBoxTicker.Text = this.stock.Ticker;
        //Temp remove
        if (this.stock.PriceTarget == null)
          this.stock.PriceTarget = new PricePoint();
        PriceTarget.Set(this.stock.PriceTarget);

        //Temp
        if (this.stock.Entry == null)
          this.stock.Entry = new PricePoint();
        Entry.Set(this.stock.Entry);

        if (this.stock.StopLoss == null)
          this.stock.StopLoss = new PricePoint();
        Stop.Set(this.stock.StopLoss);

        if (this.stock.StopLoss.NoOfShares < 0)
          this.stock.StopLoss.NoOfShares *= -1;
        Stop.numericUpDownNoOfShares.Value = this.stock.StopLoss.NoOfShares;

        checkBoxDayTrade.Checked = this.stock.DayTrade;
        CostBasis.Value = this.stock.CostBasis;
        NoOfShares.Value = this.stock.NoOfShares;
        Volatility.Value = this.stock.Volatility;
        MaxLossAmnt.Value = this.stock.MaxLoss;
        MaxLossPrcntg.Value = this.stock.MaxPrctgLoss;
        bidAskRationLabel.Text = this.stock.BidAskSpread.ToString();
        checkManageTrade.Checked = this.stock.ManageTrade;
        UpdatePendingOrder();
        if (MaxLossPrcntg.Value == 0)
          MaxLossPrcntg.Value = 1.96m;
        shown = true;
      }
      catch (Exception e)
      {

      }
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      this.Opacity = this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)) ? 0.99 : 0.25;
    }

    void Entry_TriggerSelected(object sender, EventArgs e)
    {
      if (Entry.triggerComboBox.SelectedValue.ToString() == TriggerType.Stop.ToString())
      {
      }
      else
      {
      }
    }

    void Stop_TriggerSelected(object sender, EventArgs e)
    {
    }

    void PriceTarget_TriggerSelected(object sender, EventArgs e)
    {
    }

    void Stop_PlaceOrder(object sender, EventArgs e)
    {
      stock.StopLoss.Price = Stop.numericUpDownValue.Value;
      stock.StopLoss.StopOffset = Stop.stopPriceCustomNumericUpDown.Value;
      stock.StopLoss.TrailPrcntg = Stop.trailPrcntgNumericUpDown.Value;
      stock.StopLoss.Type = (PricePointControl.OrderType)Stop.comboBoxOrderType.SelectedValue;
      stock.StopLoss.Execution = (PricePointControl.Execution)Stop.comboBoxExecution.SelectedValue;
      stock.StopLoss.Trigger = (TriggerType)Stop.triggerComboBox.SelectedValue;
      stock.StopLoss.NoOfShares = (int)Stop.numericUpDownNoOfShares.Value;
      if (stock.StopLoss.Price > 0)
      {
        if (stock.StopLoss.NoOfShares > 0)
        {
          ThreadPool.QueueUserWorkItem(PlaceStop, stock);
        }
      }
    }

    void PriceTarget_PlaceOrder(object sender, EventArgs e)
    {
      stock.PriceTarget.Price = PriceTarget.numericUpDownValue.Value;
      stock.PriceTarget.StopOffset = PriceTarget.stopPriceCustomNumericUpDown.Value;
      stock.PriceTarget.TrailPrcntg = PriceTarget.trailPrcntgNumericUpDown.Value;
      stock.PriceTarget.Type = (PricePointControl.OrderType)PriceTarget.comboBoxOrderType.SelectedValue;
      stock.PriceTarget.Execution = (PricePointControl.Execution)PriceTarget.comboBoxExecution.SelectedValue;
      stock.PriceTarget.Trigger = (TriggerType)PriceTarget.triggerComboBox.SelectedValue;
      stock.PriceTarget.NoOfShares = (int)PriceTarget.numericUpDownNoOfShares.Value;
      if (stock.PriceTarget.NoOfShares > 0)
      {
        checkManageTrade.Checked = false;
        buttonCancelPendingOrders_Click(null, null);
        if (stock.PriceTarget.Price > 0)
        {
          ThreadPool.QueueUserWorkItem(PlacePT, stock);
        }
      }
      else
        MessageBox.Show("Enter No of Shares to sell when Price reaches Price Target.");
    }

    void Entry_PlaceOrder(object sender, EventArgs e)
    {
      stock.Entry.Price = Entry.numericUpDownValue.Value;
      stock.Entry.StopOffset = Entry.stopPriceCustomNumericUpDown.Value;
      stock.Entry.TrailPrcntg = Entry.trailPrcntgNumericUpDown.Value;
      stock.Entry.Type = (PricePointControl.OrderType)Entry.comboBoxOrderType.SelectedValue;
      stock.Entry.Execution = (PricePointControl.Execution)Entry.comboBoxExecution.SelectedValue;
      stock.Entry.FollowPrice = (PricePointControl.FollowPrice)Entry.toFollowComboBox.SelectedValue;
      stock.Entry.Trigger = (TriggerType)Entry.triggerComboBox.SelectedValue;
      stock.Entry.NoOfShares = (int)Entry.numericUpDownNoOfShares.Value;
      if (stock.Entry.Price > 0)
      {
        if (stock.Entry.NoOfShares > 0)
        {
          ThreadPool.QueueUserWorkItem(PlaceEntry, stock);
        }
      }
    }

    void Stop_FollowSelected(object sender, EventArgs e)
    {
      stock.StopLoss.FollowPrice = (PricePointControl.FollowPrice)Stop.toFollowComboBox.SelectedValue;
    }

    void PriceTarget_FollowSelected(object sender, EventArgs e)
    {
      stock.PriceTarget.FollowPrice = (PricePointControl.FollowPrice)PriceTarget.toFollowComboBox.SelectedValue;
    }

    void Entry_FollowSelected(object sender, EventArgs e)
    {
      stock.Entry.FollowPrice = (PricePointControl.FollowPrice)Entry.toFollowComboBox.SelectedValue;
    }

    void UpdatePendingOrder()
    {
      try
      {
        if (stock != null && stock.PendingOrders != null)
        {
          DataTable table = new DataTable();
          table.Columns.Add("Shares", typeof(int));
          table.Columns.Add("Price", typeof(decimal));

          foreach (OrderSnapshot o in stock.PendingOrders)
          {
            table.Rows.Add(o.Quantity, Math.Round((decimal)o.Price, 2));
          }
          int scrollPosition = dataGridViewPendingOrders.FirstDisplayedScrollingRowIndex;
          
          dataGridViewPendingOrders.DataSource = table;
          dataGridViewPendingOrders.FirstDisplayedScrollingRowIndex = scrollPosition;
          dataGridViewPendingOrders.Refresh();
        }
        else
        {
          dataGridViewPendingOrders.Invoke((Action)delegate
          {
            dataGridViewPendingOrders.DataSource = new List<OrderSnapshot>();
          });
        }
      }
      catch
      {
      }
    }

    void pricePointControlStop_ExecutionSpreadSelected(object sender, EventArgs e)
    {
      List<decimal> spread = null;
      if (Stop.numericUpDownValue.Value > 0 && Stop.numericUpDownNoOfShares.Value > 0)
      {
        spread =
          stock.GetSpread(false, Stop.numericUpDownValue.Value,
          (int)Stop.numericUpDownNoOfShares.Value, Volatility.Value);
      }
      stock.StopLoss.ExecutionSpread = spread;
      Stop.comboBoxSpreadValues.DataSource = spread;
    }

    void pricePointControlPriceTarget_ExecutionSpreadSelected(object sender, EventArgs e)
    {
      List<decimal> spread = null;
      if (PriceTarget.numericUpDownValue.Value > 0 && PriceTarget.numericUpDownNoOfShares.Value > 0)
      {
        spread =
          stock.GetSpread(true, PriceTarget.numericUpDownValue.Value,
          (int)PriceTarget.numericUpDownNoOfShares.Value, Volatility.Value);
      }
      stock.PriceTarget.ExecutionSpread = spread;
      PriceTarget.comboBoxSpreadValues.DataSource = spread;
    }

    void pricePointControlEntry_ExecutionSpreadSelected(object sender, EventArgs e)
    {
      List<decimal> spread = null;
      if (Entry.numericUpDownValue.Value > 0 && Entry.numericUpDownNoOfShares.Value > 0)
      {
        spread =
          stock.GetSpread(true, Entry.numericUpDownValue.Value,
          (int)Entry.numericUpDownNoOfShares.Value, Volatility.Value);

        stock.Entry.ExecutionSpread = spread;
        Entry.comboBoxSpreadValues.DataSource = spread;
      }
    }

    void textBoxTicker_DelayedTextChanged(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(textBoxTicker.Text))
      {
        if (textBoxTicker.Enabled)
          stock = new Stock(textBoxTicker.Text);
        ThreadPool.QueueUserWorkItem(StockUpdater, this);
      }
    }

    void StockUpdater(object parm)
    {
      HoldingForm form = (HoldingForm)parm;
      while (true)
      {
        try
        {
          if (stopThreads)
            break;
          stock = Robinhood.GetQuote(new List<Stock>() { stock }).First();
          string quote = stock.Quote;

          form.labelQuote.Invoke((Action)delegate
          {
            form.labelQuote.Text = quote;
            form.labelQuote.ForeColor = form.stock.Color;
          });

          form.labelAskPrice.Invoke((Action)delegate
          {
            form.labelAskPrice.Text = "$" + stock.AskPrice.ToString();
            form.labelBidPrice.Text = "$" + stock.BidPrice.ToString();
          });

          ///////////////////////////
          if (stock.Entry.FollowPrice == PricePointControl.FollowPrice.AtAsk)
          {
            form.Entry.numericUpDownValue.Invoke((Action)delegate
            {
              form.Entry.numericUpDownValue.Value = stock.AskPrice;
            });
          }

          if (stock.Entry.FollowPrice == PricePointControl.FollowPrice.AtLastTradedPrice)
          {
            form.Entry.numericUpDownValue.Invoke((Action)delegate
            {
              form.Entry.numericUpDownValue.Value = stock.LastTradePrice + 0.01m;
            });
          }

          if (stock.Entry.FollowPrice == PricePointControl.FollowPrice.AtBid)
          {
            form.Entry.numericUpDownValue.Invoke((Action)delegate
            {
              form.Entry.numericUpDownValue.Value = stock.BidPrice;
            });
          }
          /////////////////////////////////////////////
          if (stock.StopLoss.FollowPrice == PricePointControl.FollowPrice.AtAsk)
          {
            form.Stop.numericUpDownValue.Invoke((Action)delegate
            {
              form.Stop.numericUpDownValue.Value = stock.AskPrice;
            });
          }

          if (stock.StopLoss.FollowPrice == PricePointControl.FollowPrice.AtBid)
          {
            form.Stop.numericUpDownValue.Invoke((Action)delegate
            {
              form.Stop.numericUpDownValue.Value = stock.BidPrice;
            });
          }

          if (stock.StopLoss.FollowPrice == PricePointControl.FollowPrice.AtLastTradedPrice)
          {
            form.Stop.numericUpDownValue.Invoke((Action)delegate
            {
              form.Stop.numericUpDownValue.Value = stock.LastTradePrice;
            });
          }

          ///////////////
          if (stock.PriceTarget.FollowPrice == PricePointControl.FollowPrice.AtAsk)
          {
            form.PriceTarget.numericUpDownValue.Invoke((Action)delegate
            {
              form.PriceTarget.numericUpDownValue.Value = stock.AskPrice;
            });
          }

          if (stock.PriceTarget.FollowPrice == PricePointControl.FollowPrice.AtLastTradedPrice)
          {
            form.PriceTarget.numericUpDownValue.Invoke((Action)delegate
            {
              form.PriceTarget.numericUpDownValue.Value = stock.LastTradePrice;
            });
          }

          if (stock.PriceTarget.FollowPrice == PricePointControl.FollowPrice.AtBid)
          {
            form.PriceTarget.numericUpDownValue.Invoke((Action)delegate
            {
              form.PriceTarget.numericUpDownValue.Value = stock.BidPrice;
            });
          }

          form.labelMarketValue.Invoke((Action)delegate
          {
            form.labelMarketValue.Text = "Market Value: $" + Math.Round(stock.LastTradePrice * form.NoOfShares.Value, 2).ToString();
          });

          form.bidAskRationLabel.Invoke((Action)delegate
          {
            bidAskRationLabel.Text = stock.BidAskSpread.ToString();
          });

          if (stock.NoOfShares > 0 && (stock.PendingOrders == null || !stock.PendingOrders.Any(o => o.Side == Side.Sell && o.Trigger == "stop")))
          {
            Stop.Invoke((Action)delegate
            {
              if (Stop.placeOrderButton.BackColor == SystemColors.ControlLightLight)
              {
                Stop.placeOrderButton.BackColor = Color.IndianRed;
                Stop.placeOrderButton.ForeColor = Color.White;
                Stop.placeOrderButton.Text = "No Stop Order";
              }
              else
              {
                Stop.placeOrderButton.BackColor = SystemColors.ControlLightLight;
                Stop.placeOrderButton.ForeColor = SystemColors.WindowText;
                Stop.placeOrderButton.Text = "Place Order";
              }
            });
          }
          else
          {
            Stop.Invoke((Action)delegate
            {
              Stop.placeOrderButton.BackColor = SystemColors.ButtonFace;
              Stop.placeOrderButton.ForeColor = SystemColors.WindowText;
              Stop.placeOrderButton.Text = "Place Order";
            });
          }

          this.BeginInvoke((Action)delegate
          {
            this.UpdatePendingOrder();
          });
        }
        catch (Exception e)
        {
          if (shown && form.IsHandleCreated)
          {
            form.labelQuote.Invoke((Action)delegate
            {
              form.labelQuote.Text = e.Message;
              form.labelQuote.ForeColor = Color.IndianRed;
            });
          }
        }
        Thread.Sleep(form.refreshTime);
      }
    }

    private void HoldingForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      stopThreads = true;
      if (!save)
        stock = null;
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      CloseForm();
    }

    void CloseForm()
    {
      this.Close();
    }
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (keyData == Keys.Escape)
      {
        CloseForm();
        return true;
      }
      return base.ProcessCmdKey(ref msg, keyData);
    }


    private void buttonSave_Click(object sender, EventArgs e)
    {
      stock.Ticker = textBoxTicker.Text;
      if (string.IsNullOrEmpty(stock.Ticker))
      {
        MessageBox.Show("Must Enter a Valid Ticker.");
        return;
      }

      //stock.PriceTarget.Value = PriceTarget.numericUpDownValue.Value;
      //stock.PriceTarget.TrailPrcntg = PriceTarget.trailPrcntgNumericUpDown.Value;
      //stock.PriceTarget.Type = (PricePointControl.OrderType)PriceTarget.comboBoxOrderType.SelectedValue;
      //stock.PriceTarget.Execution = (PricePointControl.Execution)PriceTarget.comboBoxExecution.SelectedValue;
      //stock.PriceTarget.Trigger = (TriggerType)PriceTarget.triggerComboBox.SelectedValue;
      //stock.PriceTarget.FollowPrice = (PricePointControl.FollowPrice)PriceTarget.toFollowComboBox.SelectedValue;
      //stock.PriceTarget.NoOfShares = (int)PriceTarget.numericUpDownNoOfShares.Value;
      stock.PriceTarget = PriceTarget.Get();
      //if (stock.PriceTarget.Value > 0 && stock.PriceTarget.NoOfShares == 0)
      //{
      //  MessageBox.Show("Enter No of Shares to sell when Price reaches Price Target.");
      //  return;
      //}


      //stock.Entry.Type = (PricePointControl.OrderType)Entry.comboBoxOrderType.SelectedValue;
      //stock.Entry.Execution = (PricePointControl.Execution)Entry.comboBoxExecution.SelectedValue;
      //stock.Entry.Trigger = (TriggerType)Entry.triggerComboBox.SelectedValue;
      //stock.Entry.FollowPrice = (PricePointControl.FollowPrice)Entry.toFollowComboBox.SelectedValue;
      //stock.Entry.NoOfShares = (int)Entry.numericUpDownNoOfShares.Value;
      //stock.Entry.Value = Entry.numericUpDownValue.Value;
      //stock.Entry.TrailPrcntg = Entry.trailPrcntgNumericUpDown.Value;
      stock.Entry = Entry.Get();
      //if (stock.Entry.Value > 0 && stock.Entry.NoOfShares == 0)
      //{
      //  MessageBox.Show("Enter No of Shares to but when Price reaches Entry Price.");
      //  return;
      //}

      //stock.StopLoss.Value = Stop.numericUpDownValue.Value;
      //stock.StopLoss.TrailPrcntg = Stop.trailPrcntgNumericUpDown.Value;
      //stock.StopLoss.Type = (PricePointControl.OrderType)Stop.comboBoxOrderType.SelectedValue;
      //stock.StopLoss.Execution = (PricePointControl.Execution)Stop.comboBoxExecution.SelectedValue;
      //stock.StopLoss.Trigger = (TriggerType)Stop.triggerComboBox.SelectedValue;
      //stock.StopLoss.FollowPrice = (PricePointControl.FollowPrice)Stop.toFollowComboBox.SelectedValue;
      //stock.StopLoss.NoOfShares = (int)Stop.numericUpDownNoOfShares.Value;
      stock.StopLoss = Stop.Get();
      //if (stock.StopLoss.Value > 0)
      //{
      //  if (stock.StopLoss.NoOfShares == 0)
      //  {
      //    MessageBox.Show("Enter No of Shares to sell when Price reaches Stop Loss Price.");
      //    return;
      //  }

      //  ////Set Stop loss if not already set
      //  //if (setStopLoss)
      //  //{
      //  //  //Robinhood.Init();
      //  //  //Robinhood.PlaceOrder(stock.Ticker, stock.StopLoss, TimeInForce.GoodTillCancel);
      //  //  stock.StopLoss.NoOfShares *= -1;
      //  //  if(stock.Trade)
      //  //    ThreadPool.QueueUserWorkItem(PlaceStop,stock);
      //  //  //MainForm.PlaceOrder(stock.Ticker, stock.StopLoss, TimeInForce.GoodTillCancel);
      //  //}
      //}

      stock.DayTrade = checkBoxDayTrade.Checked;
      stock.ManageTrade = checkManageTrade.Checked;
      stock.NoOfShares = (int)NoOfShares.Value;
      stock.CostBasis = CostBasis.Value;
      stock.Volatility = Volatility.Value;
      stock.MaxLoss = MaxLossAmnt.Value;
      stock.MaxPrctgLoss = Math.Round(MaxLossPrcntg.Value, 2);
      save = true;
      this.Close();
    }

    void PlacePT(object param)
    {
      Stock stock = (Stock)param;
      Robinhood.PlaceOrder(stock, TimeInForce.GoodTillCancel, Robinhood.TradeStep.ProfitTarget, this);
    }

    void PlaceStop(object param)
    {

      Robinhood.PlaceOrder(stock, TimeInForce.GoodTillCancel, Robinhood.TradeStep.StopLoss, this);
    }

    void PlaceEntry(object param)
    {
      Stock stock = (Stock)param;
      Robinhood.PlaceOrder(stock, TimeInForce.GoodTillCancel, Robinhood.TradeStep.Entry, this);
    }

    private void HoldingForm_Load(object sender, EventArgs e)
    {
    }

    private void numericUpDownVolatility_ValueChanged(object sender, EventArgs e)
    {
      if (Stop.comboBoxExecution.SelectedValue.ToString() == PricePointControl.Execution.Spread.ToString())
        pricePointControlStop_ExecutionSpreadSelected(null, null);
      if (PriceTarget.comboBoxExecution.SelectedValue.ToString() == PricePointControl.Execution.Spread.ToString())
        pricePointControlPriceTarget_ExecutionSpreadSelected(null, null);
      if (Entry.comboBoxExecution.SelectedValue.ToString() == PricePointControl.Execution.Spread.ToString())
        pricePointControlEntry_ExecutionSpreadSelected(null, null);
    }

    private void numericUpDownVolatility_Leave(object sender, EventArgs e)
    {

    }

    private void buttonCancelPendingOrders_Click(object sender, EventArgs e)
    {
      if (stock.PendingOrders != null && stock.PendingOrders.Count() > 0)
      {

        //TODO POTENTIAL THREADING BUG
        Robinhood.CancelThrdComp = new ManualResetEvent(false);
        ThreadPool.QueueUserWorkItem(Robinhood.CancelOrder,
          new KeyValuePair<Form, ThreadedBindingList<OrderSnapshot>>(this, stock.PendingOrders));
        //Robinhood.CancelThrdComp.WaitOne();

      }

    }

    void RecalculateLoss()
    {
      decimal relativeToo;
      int noOfShares = 0;
      if (CostBasis.Value > 0 && NoOfShares.Value > 0)
      {
        relativeToo = CostBasis.Value;
        noOfShares = (int)NoOfShares.Value;
      }
      else if (Entry.numericUpDownValue.Value > 0 && Entry.numericUpDownNoOfShares.Value > 0)
      {
        relativeToo = Entry.numericUpDownValue.Value;
        noOfShares = (int)Entry.numericUpDownNoOfShares.Value;
      }
      else
      {
        MessageBox.Show("Cost Basis & no shares or Entry should be set to calculate Max Los");
        return;
      }

      Stop.numericUpDownValue.Value = Math.Round(relativeToo - MaxLossAmnt.Value / noOfShares, 2);
      decimal tempValue = ((MaxLossAmnt.Value / noOfShares) / relativeToo) * 100;
      if (tempValue != MaxLossPrcntg.Value)
        MaxLossPrcntg.Value = tempValue;
    }

    void numericUpDownNoOfShares_ValueChanged(object sender, EventArgs e)
    {
      if (shown)
      {
        //decimal relativeToo;
        //int noOfShares = 0;
        //if (CostBasis.Value > 0 && NoOfShares.Value > 0)
        //{
        //  relativeToo = CostBasis.Value;
        //  noOfShares = (int)NoOfShares.Value;
        //}
        //else if (Entry.numericUpDownValue.Value > 0 && Entry.numericUpDownNoOfShares.Value > 0)
        //{
        //  relativeToo = Entry.numericUpDownValue.Value;
        //  noOfShares = (int)Entry.numericUpDownNoOfShares.Value;
        //}
        //else
        //{
        //  //MessageBox.Show("Cost Basis & no shares or Entry should be set to calculate Max Los");
        //  return;
        //}
        //decimal stopvalue = Math.Round(relativeToo - MaxLossAmnt.Value / noOfShares, 2);
        //Stop.numericUpDownValue.Value = stopvalue > 0 ? stopvalue : 0;
        //Stop.numericUpDownNoOfShares.Value = Entry.numericUpDownNoOfShares.Value;
        //decimal tempValue = ((MaxLossAmnt.Value / noOfShares) / relativeToo) * 100;
        //if (tempValue != MaxLossPrcntg.Value)
        //  MaxLossPrcntg.Value = tempValue;
      }
    }

    private void customNumericUpDownMaxLossAmnt_ValueChanged(object sender, EventArgs e)
    {
      if (shown)
      {
        decimal relativeToo;
        int noOfShares = 0;
        if (CostBasis.Value > 0 && NoOfShares.Value > 0)
        {
          relativeToo = CostBasis.Value;
          noOfShares = (int)NoOfShares.Value;
        }
        else if (Entry.numericUpDownValue.Value > 0 && Entry.numericUpDownNoOfShares.Value > 0)
        {
          relativeToo = Entry.numericUpDownValue.Value;
          noOfShares = (int)Entry.numericUpDownNoOfShares.Value;
        }
        else
        {
          //MessageBox.Show("Cost Basis & no shares or Entry should be set to calculate Max Los");
          return;
        }

        decimal stopvalue = Math.Round(relativeToo - MaxLossAmnt.Value / noOfShares, 2);
        Stop.numericUpDownValue.Value = stopvalue > 0 ? stopvalue : 0;
        decimal tempValue = ((MaxLossAmnt.Value / noOfShares) / relativeToo) * 100;
        if (tempValue != MaxLossPrcntg.Value)
          MaxLossPrcntg.Value = tempValue;
      }
    }

    private void MaxLossPrcntg_ValueChanged(object sender, EventArgs e)
    {
      if (shown)
      {
        stock.MaxPrctgLoss = Math.Round(MaxLossPrcntg.Value, 2);
        decimal relativeToo;
        int noOfShares = 0;
        if (CostBasis.Value > 0 && NoOfShares.Value > 0)
        {
          relativeToo = CostBasis.Value;
          noOfShares = (int)NoOfShares.Value;
        }
        else if (Entry.numericUpDownValue.Value > 0 && Entry.numericUpDownNoOfShares.Value > 0)
        {
          relativeToo = Entry.numericUpDownValue.Value;
          noOfShares = (int)Entry.numericUpDownNoOfShares.Value;
        }
        else
        {
          //MessageBox.Show("Cost Basis & no shares or Entry should be set to calculate Max Los");
          return;
        }

        decimal stopvalue = Math.Round(relativeToo - (relativeToo * MaxLossPrcntg.Value / 100), 2);
        Stop.numericUpDownValue.Value = stopvalue > 0 ? stopvalue : 0;
        decimal tempValue = Math.Round((relativeToo * MaxLossPrcntg.Value / 100) * noOfShares, 2);
        if (tempValue != MaxLossAmnt.Value)
          MaxLossAmnt.Value = tempValue;
      }
    }

    private void ProfitTargAmnt_ValueChanged(object sender, EventArgs e)
    {
      if (shown)
      {
        decimal relativeToo;
        int noOfShares = 0;
        if (CostBasis.Value > 0 && NoOfShares.Value > 0)
        {
          relativeToo = CostBasis.Value;
          noOfShares = (int)NoOfShares.Value;
        }
        else if (Entry.numericUpDownValue.Value > 0 && Entry.numericUpDownNoOfShares.Value > 0)
        {
          relativeToo = Entry.numericUpDownValue.Value;
          noOfShares = (int)Entry.numericUpDownNoOfShares.Value;
        }
        else
        {
          //MessageBox.Show("Cost Basis & no shares or Entry should be set to calculate Profit Target");
          return;
        }

        PriceTarget.numericUpDownValue.Value = Math.Round(relativeToo + ProfitTargAmnt.Value / noOfShares, 2);
        decimal tempValue = Math.Round(((ProfitTargAmnt.Value / noOfShares) / relativeToo) * 100, 2);
        if (tempValue != ProfitTargPrctg.Value)
          ProfitTargPrctg.Value = tempValue;
      }
    }

    private void ProfitTargPrctg_ValueChanged(object sender, EventArgs e)
    {
      if (shown)
      {
        decimal relativeToo;
        int noOfShares = 0;
        if (CostBasis.Value > 0 && NoOfShares.Value > 0)
        {
          relativeToo = CostBasis.Value;
          noOfShares = (int)NoOfShares.Value;
        }
        else if (Entry.numericUpDownValue.Value > 0 && Entry.numericUpDownNoOfShares.Value > 0)
        {
          relativeToo = Entry.numericUpDownValue.Value;
          noOfShares = (int)Entry.numericUpDownNoOfShares.Value;
        }
        else
        {
          //MessageBox.Show("Cost Basis & no shares or Entry should be set to calculate");
          return;
        }

        PriceTarget.numericUpDownValue.Value = Math.Round(relativeToo + (relativeToo * ProfitTargPrctg.Value / 100), 2);
        decimal tempValue = Math.Round((relativeToo * ProfitTargPrctg.Value / 100) * noOfShares, 2);
        if (tempValue != ProfitTargAmnt.Value)
          ProfitTargAmnt.Value = tempValue;
      }
    }

    private void buttonSetStopLoss_Click(object sender, EventArgs e)
    {
      // buttonSetStopLoss.Focus();
      //bool setStopLoss = false;
      //if (stock.NoOfShares > 0 && stock.StopLoss.Value != Stop.numericUpDownValue.Value
      //  || stock.StopLoss.NoOfShares != (int)Stop.numericUpDownNoOfShares.Value
      //  || (!stock.Trade && checkBoxDayTrade.Checked))//trade check flipped
      //{
      //  setStopLoss = true;
      //buttonCancelPendingOrders_Click(null, null);
      //}


      //stock.StopLoss.Value = Stop.numericUpDownValue.Value;
      //stock.StopLoss.Type = (PricePointControl.OrderType)Stop.comboBoxOrderType.SelectedValue;
      //stock.StopLoss.Execution = (PricePointControl.Execution)Stop.comboBoxExecution.SelectedValue;
      //stock.StopLoss.FollowPrice = (PricePointControl.FollowPrice)Stop.toFollowComboBox.SelectedValue;
      //stock.StopLoss.Trigger = (TriggerType)Stop.triggerComboBox.SelectedValue;
      //stock.StopLoss.NoOfShares = (int)Stop.numericUpDownNoOfShares.Value;
      stock.StopLoss = Stop.Get();
      if (stock.StopLoss.Price > 0)
      {
        if (stock.StopLoss.NoOfShares > 0)
        {
          //  MessageBox.Show("Enter No of Shares to sell when Price reaches Stop Loss Price.");
          //  return;
          //}

          //Set Stop loss if not already set
          //if (setStopLoss)
          //{
          //Robinhood.Init();
          //Robinhood.PlaceOrder(stock.Ticker, stock.StopLoss, TimeInForce.GoodTillCancel);
          ThreadPool.QueueUserWorkItem(PlaceStop, stock);
          //MainForm.PlaceOrder(stock.Ticker, stock.StopLoss, TimeInForce.GoodTillCancel);
          //}
        }
      }
    }

    private void buttonPlaceEntry_Click(object sender, EventArgs e)
    {
      //buttonPlaceEntry.Focus();
      //bool setEntry = false;
      //if (stock.Entry.Value != Entry.numericUpDownValue.Value
      //  || stock.Entry.NoOfShares != (int)Entry.numericUpDownNoOfShares.Value
      //  || (!stock.Trade && checkBoxDayTrade.Checked))//trade check flipped
      //{
      //  setEntry = true;
      //buttonCancelPendingOrders_Click(null, null);
      //}
      //stock.Entry.Value = Entry.numericUpDownValue.Value;
      //stock.Entry.TrailPrcntg = Entry.trailPrcntgNumericUpDown.Value;
      //stock.Entry.Type = (PricePointControl.OrderType)Entry.comboBoxOrderType.SelectedValue;
      //stock.Entry.Execution = (PricePointControl.Execution)Entry.comboBoxExecution.SelectedValue;
      //stock.Entry.Trigger = (TriggerType)Entry.triggerComboBox.SelectedValue;
      //stock.Entry.NoOfShares = (int)Entry.numericUpDownNoOfShares.Value;
      stock.Entry = Entry.Get();
      if (stock.Entry.Price > 0)
      {
        if (stock.Entry.NoOfShares > 0)
        {
          //  MessageBox.Show("Enter No of Shares to sell when Price reaches Stop Loss Price.");
          //  return;
          //}

          //Set Stop loss if not already set
          //if (setEntry)
          //{
          //Robinhood.Init();
          //Robinhood.PlaceOrder(stock.Ticker, stock.StopLoss, TimeInForce.GoodTillCancel);
          ThreadPool.QueueUserWorkItem(PlaceEntry, stock);
          //MainForm.PlaceOrder(stock.Ticker, stock.StopLoss, TimeInForce.GoodTillCancel);
        }

        //}
      }
    }

    private void Entry_VisibleChanged(object sender, EventArgs e)
    {

    }

    private void HoldingForm_Shown(object sender, EventArgs e)
    {
      shown = true;
    }

    private void Entry_Load(object sender, EventArgs e)
    {

    }

    private void PriceTarget_Load(object sender, EventArgs e)
    {

    }

    private void checkManageTrade_CheckedChanged(object sender, EventArgs e)
    {
      stock.ManageTrade = checkManageTrade.Checked;
    }

  }
}
