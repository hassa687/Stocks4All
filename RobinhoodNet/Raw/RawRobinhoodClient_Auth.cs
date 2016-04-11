using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace BasicallyMe.RobinhoodNet.Raw
{
    public partial class RawRobinhoodClient
    {
        string _authToken;
        public string AuthToken
        {
            get { return _authToken; }
            private set
            {
                _authToken = value;
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Token",
                    _authToken);
            }
        }

        public async Task 
        Authenticate (string userName, string password)
        {
            var auth = await doPost(LOGIN_URL, new Dictionary<string, string>
                {
                    { "username", userName },
                    { "password", password }
                });

            this.AuthToken = auth["token"].ToString();
        }

        public Task Authenticate (string token)
        {
            this.AuthToken = token;

            var tcs = new TaskCompletionSource<bool>();
            tcs.SetResult(true);
            return tcs.Task;
        }
    }
}
