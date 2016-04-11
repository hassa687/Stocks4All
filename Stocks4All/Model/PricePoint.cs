using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Stocks4All.CustomControls;
using BasicallyMe.RobinhoodNet;

namespace Stocks4All.Model
{
  [Serializable]
  public class PricePoint : INotifyPropertyChanged
  {
    PricePointControl.OrderType type;
    PricePointControl.Execution execution;
    PricePointControl.FollowPrice followPrice;
    TriggerType trigger;
    decimal price;
    decimal stopOffset;
    decimal trailPrcntg;
    List<decimal> executionSpread;
    int noOfShares;
    //Dictionary<decimal, String> pendingOrders;
    //public TimeInForce tif;


    //public TimeInForce TIF
    //{
    //  get { return this.tif; }
    //  set
    //  {
    //    if (value != this.tif)
    //    {
    //      this.tif = value;
    //      NotifyPropertyChanged();
    //    }
    //  }
    //}

    //public Dictionary<decimal, String> PendingOrders
    //{
    //  get { return this.pendingOrders; }
    //  set
    //  {
    //    if (value != this.pendingOrders)
    //    {
    //      this.pendingOrders = value;
    //      NotifyPropertyChanged();
    //    }
    //  }
    //}

    public PricePointControl.OrderType Type
    {
      get { return this.type; }
      set
      {
        if (value != this.type)
        {
          this.type = value;
          NotifyPropertyChanged();
        }
      }
    }

    public TriggerType Trigger
    {
      get { return this.trigger; }
      set
      {
        if (value != this.trigger)
        {
          this.trigger = value;
          NotifyPropertyChanged();
        }
      }
    }
    public PricePointControl.Execution Execution
    {
      get { return this.execution; }
      set
      {
        if (value != this.execution)
        {
          this.execution = value;
          NotifyPropertyChanged();
        }
      }
    }

    public PricePointControl.FollowPrice FollowPrice
    {
      get { return this.followPrice; }
      set
      {
        if (value != this.followPrice)
        {
          this.followPrice = value;
          NotifyPropertyChanged();
        }
      }
    }


    public decimal Price
    {
      get { return this.price; }
      set
      {
        if (value != this.price)
        {
          this.price = value;
          NotifyPropertyChanged();
        }
      }
    }

    public decimal StopOffset
    {
      get { return this.stopOffset; }
      set
      {
        if (value != this.stopOffset)
        {
          this.stopOffset = value;
          NotifyPropertyChanged();
        }
      }
    }

    public decimal TrailPrcntg
    {
      get { return this.trailPrcntg; }
      set
      {
        if (value != this.trailPrcntg)
        {
          this.trailPrcntg = value;
          NotifyPropertyChanged();
        }
      }
    }

    public List<decimal> ExecutionSpread
    {
      get { return this.executionSpread; }
      set
      {
        if (value != this.executionSpread)
        {
          this.executionSpread = value;
          NotifyPropertyChanged();
        }
      }
    }

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

    public PricePoint()
    {
      Type = PricePointControl.OrderType.Limit;
      Execution = PricePointControl.Execution.Limit;
      FollowPrice = PricePointControl.FollowPrice.AtLastTradedPrice;
      Trigger = TriggerType.Immediate;
      Price = 0;
      NoOfShares = 1;
      //pendingOrders = new Dictionary<decimal, String>();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    public override string ToString()
    {
      if (Price <= 0)
        return string.Empty;
      else
        return String.Format("{0}({1})", Price, Enum.GetName(typeof(PricePointControl.OrderType), Type));
    }
  }
}
