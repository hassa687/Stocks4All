using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stocks4All.Model;
using System.Collections.Generic;

namespace Stocks4AllTest
{
  [TestClass]
  public class StockTest
  {
    [TestMethod]
    public void GetSpreadTest()
    {
      Stock stock = new Stock();
      decimal Volatility = 0.0210m;
      List<decimal> spread = stock.GetSpread(true,100, 10, Volatility);
     // spread = stock.GetSpread(false, 100, 3);
     // spread = stock.GetSpread(true, 100, 3);
     //// spread = stock.GetSpread(true, 10.06m, 11);
      spread = stock.GetSpread(true, 2, 100, Volatility);
      spread = stock.GetSpread(true, 106, 5, Volatility);
    }
  }
}
