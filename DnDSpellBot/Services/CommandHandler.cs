using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace DnDSpellBot.Services
{
    class CommandHandler
    {
        private readonly DiscordSocketClient DiscordClient;
        private readonly CommandService Commands;
        private readonly IConfiguration config;
        private readonly IServiceProvider Services;

        public CommandHandler(IServiceProvider services)
        {
            config = services.GetRequiredService<IConfiguration>();
            Commands = services.GetRequiredService<CommandService>();
            DiscordClient = services.GetRequiredService<DiscordSocketClient>();
            Services = services;

            Commands.CommandExecuted += CommandExecuteAsync;

            DiscordClient.MessageReceived += MessageReceivedAsync;
        }

        public async Task InitializeAsync()
        {
            await Commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
                                          Services);
        }

        public async Task MessageReceivedAsync(SocketMessage rawMessage)
        {
            if (!(rawMessage is SocketUserMessage message)) return;

            if (message.Source != MessageSource.User) return;

            var argPos = 0;

            char prefix = Char.Parse(config["Prefix"]);

            if (!(message.HasMentionPrefix(DiscordClient.CurrentUser, ref argPos) || message.HasCharPrefix(prefix, ref argPos))) return;

            var context = new SocketCommandContext(DiscordClient, message);

            await Commands.ExecuteAsync(context, argPos, Services);
        }

        public async Task CommandExecuteAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            if(!command.IsSpecified)
            {
                Console.WriteLine($"Command failed to execute for [{context.User.Username}] <-> [{result.ErrorReason}]!");
                return;
            }

            if(result.IsSuccess)
            {
                Console.WriteLine($"Command [{command.Value.Name}] executed for -> [{context.User.Username}]");
                return;
            }

            await context.Channel.SendMessageAsync($"Sorry... something went wrong -> [{result}]!");

        }
    }

}
