using System;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HeyListen.Preconditions
{
    public class RequireDj : PreconditionAttribute
    {
        public async override Task<PreconditionResult> CheckPermissions(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            // possible to pass the context in?
            var db = (DataBase)services.GetService(typeof(DataBase));
            var currentDj = db.Channels.Include(c => c.CurrentDj).FirstOrDefault(c => c.DiscordId == context.Channel.Id.ToString())?.CurrentDj;
            if (currentDj.DiscordId == context.Message.Author.Id.ToString())
                return PreconditionResult.FromSuccess();
            
            return PreconditionResult.FromError("Only the DJ can perform this action.");
        }
    }
}
