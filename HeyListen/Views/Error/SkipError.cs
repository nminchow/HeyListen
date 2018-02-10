using System;
using System.Collections.Generic;
using System.Text;

namespace HeyListen.Views.Error
{
    class SkipError
    {
        public static string Response()
        {
            return "There was an error skipping the track. This can often be " +
                "resolved by having the DJ switch to a different track in " +
                "spotify, then switching back to the desired source. If " +
                "this problem persists, please let us know in the official " +
                "discord: https://discord.gg/RdXVwZs";
        }
    }
}
