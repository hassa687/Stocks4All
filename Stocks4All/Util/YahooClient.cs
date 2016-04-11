using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Stocks4All.Util
{
  public class YahooClient
  {
    public string YahooStockRequest(string Symbols, bool UseYahoo = true)
    {
      {
        string StockQuoteUrl = string.Empty;

        try
        {
          // Use Yahoo finance service to download stock data from Yahoo
          if (UseYahoo)
          {
            string YahooSymbolString = Symbols.Replace(",", "+");
            StockQuoteUrl = @"http://finance.yahoo.com/q?s=" + YahooSymbolString + "&ql=1";
          }
          else
          {
            //Going to Put Google Finance here when I Figure it out.
          }

          // Initialize a new WebRequest.
          HttpWebRequest webreq = (HttpWebRequest)WebRequest.Create(StockQuoteUrl);
          // Get the response from the Internet resource.
          HttpWebResponse webresp = (HttpWebResponse)webreq.GetResponse();
          // Read the body of the response from the server.

          HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
          string pageSource;
          using (StreamReader sr = new StreamReader(webresp.GetResponseStream()))
          {
            pageSource = sr.ReadToEnd();
          }
          doc.LoadHtml(pageSource.ToString());
          if (UseYahoo)
          {
            string Results = string.Empty;
            //loop through each Symbol that you provided with a "," delimiter
            foreach (string SplitSymbol in Symbols.Split(new char[] { ',' }))
            {
              Results += SplitSymbol + " : " + doc.GetElementbyId("yfs_l10_" + SplitSymbol).InnerText + Environment.NewLine;
            }
            return (Results);
          }
          else
          {
            return (doc.GetElementbyId("ref_14135_l").InnerText);
          }

        }
        catch (WebException Webex)
        {
          return ("SYSTEM ERROR DOWNLOADING SYMBOL: " + Webex.ToString());

        }

      }
    }
  }
}
