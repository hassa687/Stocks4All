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
using BasicallyMe.RobinhoodNet;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

namespace BasicallyMe.RobinhoodNet.RhPlaceOrder
{
    class MainClass
    {
        public static void Main (string [] args)
        {
            var rh = new RobinhoodClient();

            authenticate(rh).Wait();

            Account account = rh.DownloadAllAccounts().Result.First();

            Instrument instrument = null;
            while (instrument == null)
            {
                try
                {
                    Console.Write("Symbol: ");
                    var symbol = Console.ReadLine().ToUpperInvariant();
                    instrument = rh.FindInstrument(symbol).Result.First(i => i.Symbol == symbol);
                    Console.WriteLine(instrument.Name);
                }
                catch
                {
                    Console.WriteLine("Problem. Try again.");
                }
            }

            int qty = 0;
            while (true)
            {
                
                Console.Write("Quantity (positive for buy, negative for sell): ");
                string q = Console.ReadLine();
                if (Int32.TryParse(q, out qty))
                {
                    break;
                }
            }

            decimal price = 0m;
            while (true)
            {
                Console.Write("Limit price (or 0 for Market order): ");
                string p = Console.ReadLine();
                if (Decimal.TryParse(p, out price))
                {
                    break;
                }
            }

            TimeInForce tif = TimeInForce.Unknown;
            while (true)
            {
                Console.Write("Time in Force (GFD or GTC): ");
                string t = Console.ReadLine();
                if (t.Equals("GFD", StringComparison.InvariantCultureIgnoreCase))
                {
                    tif = TimeInForce.GoodForDay;
                    break;
                }
                else if (t.Equals("GTC", StringComparison.InvariantCultureIgnoreCase))
                {
                    tif = TimeInForce.GoodTillCancel;
                    break;
                }
            }


            var newOrderSingle = new NewOrderSingle(instrument);
            newOrderSingle.AccountUrl = account.AccountUrl;
            newOrderSingle.Quantity   = Math.Abs(qty);
            newOrderSingle.Side       = qty > 0 ? Side.Buy : Side.Sell;

            newOrderSingle.TimeInForce = tif;
            if (price == 0)
            {
                newOrderSingle.OrderType = OrderType.Market;
            }
            else
            {
                newOrderSingle.OrderType = OrderType.Limit;
                newOrderSingle.Price = price;
            }


            var order = rh.PlaceOrder(newOrderSingle).Result;
            Console.WriteLine("{0}\t{1}\t{2} x {3}\t{4}",
                                order.Side,
                                instrument.Symbol,
                                order.Quantity,
                                order.Price.HasValue ? order.Price.ToString() : "mkt",
                                order.State);
            if (!String.IsNullOrEmpty(order.RejectReason))
            {
                Console.WriteLine(order.RejectReason);
            }
            else
            {
                Console.WriteLine("Press C to cancel this order, or anything else to quit");
                var x = Console.ReadKey();
                if (x.KeyChar == 'c' || x.KeyChar == 'C')
                {
                    rh.CancelOrder(order.CancelUrl).Wait();
                    Console.WriteLine("Cancelled");
                }
            }
        }




        static readonly string __tokenFile = System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "RobinhoodNet",
            "token");
        
        static string getConsolePassword ()
        {
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }

                if (cki.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length > 0)
                    {
                        Console.Write("\b\0\b");
                        sb.Length--;
                    }

                    continue;
                }

                Console.Write('*');
                sb.Append(cki.KeyChar);
            }

            return sb.ToString();
        }

        static async Task authenticate (RobinhoodClient client)
        {
            if (System.IO.File.Exists(__tokenFile))
            {
                var token = System.IO.File.ReadAllText(__tokenFile);
                await client.Authenticate(token);
            }
            else
            {
                Console.Write("username: ");
                string userName = Console.ReadLine();

                Console.Write("password: ");
                string password = getConsolePassword();

                await client.Authenticate(userName, password);

                System.IO.Directory.CreateDirectory(
                    System.IO.Path.GetDirectoryName(__tokenFile));

                System.IO.File.WriteAllText(__tokenFile, client.AuthToken);
            }            
        }
    }
}
