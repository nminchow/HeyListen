using System;
using System.Collections.Generic;
using System.Text;

namespace HeyListen.Controllers.Admin
{
    public class AdminOrchestrator
    {
        public string secret;

        public AdminOrchestrator(string spotifySecret)
        {
            secret = spotifySecret;
        }


    }
}
