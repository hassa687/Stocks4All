using BasicallyMe.RobinhoodNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Stocks4All.CustomControls;

namespace Stocks4All.Model
{
  [Serializable]
  public class Stock : INotifyPropertyChanged
  {
    private string ticker;
    private int noOfShares;
    private decimal costBasis;
    private decimal lastTradePrice;
    private decimal askPrice;
    private decimal bidPrice;
    private decimal changePctng;
    private Color color;
    private string chngSym;
    private PricePoint priceTarget;
    private PricePoint entry;
    private bool dayTrade;
    private PricePoint stopLoss;
    private decimal volatility;
    private string instrumentURL;
    private decimal maxLoss;
    private decimal maxPrctgLoss;
    bool manageTrade;
    //bool sellAtAsk;
    //bool buyAtAsk;
    //bool sellAtBid;

    


    [DisplayName("Ticker")]
    public string Ticker
    {
      get { return this.ticker; }
      set
      {
        if (value != this.ticker)
        {
          this.ticker = value;
          NotifyPropertyChanged();
        }
      }
    }
    [XmlIgnore]
    [Browsable(false)]
    [DisplayName("Change Symbol")]
    public string ChngSym
    {
      get { return this.chngSym; }
      set
      {
        if (value != this.chngSym)
        {
          this.chngSym = value;
          NotifyPropertyChanged();
        }
      }
    }
    [XmlIgnore]
    [DisplayName("Quote")]
    public String Quote
    {
      get
      {
        //return string.Format("${0} ({1}{2}%)",
        //    LastTradePrice.ToString(),
        //    ChngSym,
        //    ChangePctng.ToString());
        if (ChangePctng != 0)
        {
          decimal test = Math.Round(ChangePctng, 2);
        }
        return string.Format("${0} ({1}{2}%)",
            LastTradePrice.ToString(),
            ChngSym,
            Math.Round(ChangePctng, 2));
      }
    }
    [XmlIgnore]
    [DisplayName("Change %")]
    public decimal ChangePctng
    {
      get { return this.changePctng; }
      set
      {
        if (value != this.changePctng)
        {
          this.changePctng = value;
          NotifyPropertyChanged();
        }
      }
    }
    [DisplayName("Shares")]
    public int NoOfShares
    {
      get { return this.noOfShares; }
      set
      {
        if (value != this.noOfShares)
        {
          this.noOfShares = value;
          NotifyPropertyChanged();
        }
      }
    }
    [DisplayName("Cost Basis")]
    public decimal CostBasis
    {
      get { return this.costBasis; }
      set
      {
        if (value != this.costBasis)
        {
          this.costBasis = value;
          NotifyPropertyChanged();
        }
      }
    }
    [XmlIgnore]
    [Browsable(false)]
    [DisplayName("Gain %")]
    public decimal GainPrcntg
    {
      get
      {
        if (this.costBasis <= 0)
          return 0;
        else
          return (Math.Round((this.lastTradePrice - this.costBasis) / this.costBasis, 2) * 100);
      }
    }
    [XmlIgnore]
    [Browsable(false)]
    [DisplayName("Gain")]
    public decimal Gain
    {
      get
      {
        return ((this.lastTradePrice * this.noOfShares) - (this.costBasis * this.noOfShares));
      }
    }
    [XmlIgnore]
    [DisplayName("Total Gain")]
    public string GainQuote
    {
      get
      {
        string sym = string.Empty;
        if (Gain < 0)
          sym = "-";
        else if (Gain > 0)
          sym = "+";
        return string.Format("${2}{0}({2}{1}%)", Gain, GainPrcntg, sym);
      }
    }
    [XmlIgnore]
    [DisplayName("Market Value")]
    public decimal MarketValue
    {
      get
      {
        return (this.lastTradePrice * this.noOfShares);
      }
    }
    [XmlIgnore]
    [DisplayName("Last Trade Price")]
    public decimal LastTradePrice
    {
      get { return this.lastTradePrice; }
      set
      {
        if (value != this.lastTradePrice)
        {
          this.lastTradePrice = value;
          NotifyPropertyChanged();
        }
      }
    }
    [XmlIgnore]
    [Browsable(false)]
    [DisplayName("Ask Price")]
    public decimal AskPrice
    {
      get { return this.askPrice; }
      set
      {
        if (value != this.askPrice)
        {
          this.askPrice = value;
          NotifyPropertyChanged();
        }
      }
    }
    [XmlIgnore]
    [Browsable(false)]
    [DisplayName("Bid Price")]
    public decimal BidPrice
    {
      get { return this.bidPrice; }
      set
      {
        if (value != this.bidPrice)
        {
          this.bidPrice = value;
          NotifyPropertyChanged();
        }
      }
    }
    [XmlIgnore]
    [Browsable(false)]
    public decimal BidAskSpread
    {
      //get { return this.askPrice > 0 ? Math.Round(this.bidPrice / this.askPrice, 2) : 0; }
      get { return Math.Round(this.askPrice - this.bidPrice, 2); }
    }
  
    [XmlIgnore]
    [Browsable(false)]
    [DisplayName("Color")]
    public Color Color
    {
      get { return this.color; }
      set
      {
        if (value != this.color)
        {
          this.color = value;
          NotifyPropertyChanged();
        }
      }
    }
    [DisplayName("Manage Trade")]
    public bool ManageTrade
    {
      get { return this.manageTrade; }
      set
      {
        if (value != this.manageTrade)
        {
          this.manageTrade = value;
          NotifyPropertyChanged();
        }
      }
    }

    //[Browsable(false)]
    //public bool SellAtAsk
    //{
    //  get { return this.sellAtAsk; }
    //  set
    //  {
    //    if (value != this.sellAtAsk)
    //    {
    //      this.sellAtAsk = value;
    //      NotifyPropertyChanged();
    //    }
    //  }
    //}
    //[Browsable(false)]
    //public bool SellAtBid
    //{
    //  get { return this.sellAtBid; }
    //  set
    //  {
    //    if (value != this.sellAtBid)
    //    {
    //      this.sellAtBid = value;
    //      NotifyPropertyChanged();
    //    }
    //  }
    //}

    [DisplayName("Volatility %")]
    public decimal Volatility
    {
      get { return this.volatility; }
      set
      {
        if (value != this.volatility)
        {
          this.volatility = value;
          NotifyPropertyChanged();
        }
      }
    }
    [DisplayName("Stop Loss")]
    public PricePoint StopLoss
    {
      get { return this.stopLoss; }
      set
      {
        if (value != this.stopLoss)
        {
          this.stopLoss = value;
          NotifyPropertyChanged();
        }
      }
    }
    [DisplayName("Entry")]
    public PricePoint Entry
    {
      get { return this.entry; }
      set
      {
        if (value != this.entry)
        {
          this.entry = value;
          NotifyPropertyChanged();
        }
      }
    }
    [DisplayName("Price Target")]
    public PricePoint PriceTarget
    {
      get { return this.priceTarget; }
      set
      {
        if (value != this.priceTarget)
        {
          this.priceTarget = value;
          NotifyPropertyChanged();
        }
      }
    }
    
    [DisplayName("Day Trade")]
    public bool DayTrade
    {
      get { return this.dayTrade; }
      set
      {
        if (value != this.dayTrade)
        {
          this.dayTrade = value;
          NotifyPropertyChanged();
        }
      }
    }
    [Browsable(false)]
    public String InstrumentURL
    {
      get { return this.instrumentURL; }
      set
      {
        if (value != this.instrumentURL)
        {
          this.instrumentURL = value;
          NotifyPropertyChanged();
        }
      }
    }
    [XmlIgnore]
    [Browsable(false)]
    public ThreadedBindingList<OrderSnapshot> PendingOrders;
    
    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }
    [Browsable(false)]
    public decimal MaxLoss
    {
      get { return this.maxLoss; }
      set
      {
        if (value != this.maxLoss)
        {
          this.maxLoss = value;
          NotifyPropertyChanged();
        }
      }
    }
    [Browsable(false)]
    public decimal MaxPrctgLoss
    {
      get { return this.maxPrctgLoss; }
      set
      {
        if (value != this.maxPrctgLoss)
        {
          this.maxPrctgLoss = value;
          NotifyPropertyChanged();
        }
      }
    }
    public Stock()
    {
    }

    public Stock(string ticker)
    {
      Ticker = ticker;
      PriceTarget = new PricePoint() { Price = 0.00m, StopOffset = -0.05m, Trigger = TriggerType.Immediate,FollowPrice = PricePointControl.FollowPrice.AtValue, Type = PricePointControl.OrderType.Limit, Execution = PricePointControl.Execution.Limit, NoOfShares = 0 };
      Entry = new PricePoint() { Price = 0.00m, StopOffset = -0.05m, Trigger = TriggerType.Immediate, Type = PricePointControl.OrderType.Limit, Execution = PricePointControl.Execution.Limit, NoOfShares = 0 };
      StopLoss = new PricePoint() { StopOffset = -0.05m, Trigger = TriggerType.Stop, FollowPrice = PricePointControl.FollowPrice.AtValue, Price = 0.00m, Type = PricePointControl.OrderType.Limit, Execution = PricePointControl.Execution.Limit, NoOfShares = 0 };
      Volatility = 2;
      DayTrade = false;
      //MaxLoss = 5m;
      MaxPrctgLoss = 1.35m;
      ManageTrade = true;
    }

    public List<decimal> GetSpread(bool buying, decimal price, int NoOfShares, decimal volatility)
    {
      List<decimal> result = new List<decimal>();
      if (NoOfShares == 1)
      {
        result.Add(price);
        return result;
      }

      decimal margin = (price * (volatility/100)) * 2;
      decimal offset = Math.Round(margin / NoOfShares,2);
      decimal runningOffset = offset;
      int count = 0;
      if (margin % NoOfShares > 0)
        count = NoOfShares - 1;
      else
        count = NoOfShares;

      bool add = true;
      //bool priceReachedZero = false;
      for (int i = 1; i <= count; i++)
      {
        if (buying && i == 1) // if buying first offset price is < target
          add = false; // if selling first offset price is > target

        if (add)
        {
          result.Add(price + runningOffset);
          //if (!priceReachedZero)
            add = false;
        }
        //else if (!priceReachedZero)
        else
        {
          decimal offsetPrice = price - runningOffset;
          //if (offsetPrice <= 0)
          //{
          //  priceReachedZero = true;
          //  i--;
          //}
          
          //if(!priceReachedZero)
            result.Add(offsetPrice);
          add = true;
        }

        if (i % 2 == 0)
          runningOffset = runningOffset + offset;
      }

      if (margin % NoOfShares > 0)
        result.Add(price);
      
      return result;
    }
  }
}
