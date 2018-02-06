using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HeyListen.Preconditions
{
    class CurrentlyDisabled : PreconditionAttribute
    {
        public async override Task<PreconditionResult> CheckPermissions(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            return PreconditionResult.FromError("This command is not implemented.");
        }
    }
}
