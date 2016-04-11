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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicallyMe.RobinhoodNet
{
    public class Url<T>
    {
        public Url(string url)
        {
            this.Uri = new Uri(url);
        }

        public Url(Uri uri)
        {
            //if (uri == null) { throw new ArgumentNullException(nameof(uri)); }
            this.Uri = uri;
        }


        public Uri Uri { get; private set; }

        public override int GetHashCode()
        {
            return this.Uri.GetHashCode();
        }

        public override string ToString()
        {
            return this.Uri.ToString();
        }

        public override bool Equals(object obj)
        {
            Url<T> rhs = obj as Url<T>;
            if (rhs != null)
            {
                return this.Uri.Equals(rhs.Uri);
            }
            else
            {
                Uri uri = obj as Uri;
                if (uri != null)
                {
                    return this.Uri.Equals(uri);
                }
            }
            return false;
        }
    }

    public class OrderCancellation
    {
    }

    public class AccountPositions
    {
    }
}
