using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace chockboibot
{
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        [Command("say")]
        public async Task SayAsync(string msg)
        {
            await Context.Channel.SendMessageAsync(msg);
        }
    }
}
