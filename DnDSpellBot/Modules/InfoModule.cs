using Discord;
using Discord.Net;
using Discord.WebSocket;
using Discord.Commands;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace DnDSpellBot.Modules
{
    public class InfoModule : ModuleBase
    {
        [Command("hello")]
        [Summary("Echoes a message")]
        public async Task HelloCommand()
        {
            var sb = new StringBuilder();

            var user = Context.User;

            sb.AppendLine($"You are -> [{user.Username}]");

            sb.AppendLine("Hello World");

            await ReplyAsync(sb.ToString());
        }
    }
}
