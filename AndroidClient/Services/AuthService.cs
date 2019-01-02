using Android.App;
using Client.Models;
using Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Services
{
    public class AuthService : Service
    {
        public AuthService(
            ) : base("/api/auth")
        {
        }
        
        public async Task<HttpResult<string>> Login(Login model, CancellationToken token = default(CancellationToken))
        {
            return await PostString(model,"/login", token);
        }
    }
}