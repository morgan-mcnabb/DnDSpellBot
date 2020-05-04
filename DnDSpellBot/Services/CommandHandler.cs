using System;
using System.Net.Http;
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
        //fields for constructor
        private readonly DiscordSocketClient DiscordClient;
        private readonly CommandService Commands;
        private readonly IConfiguration config;
        private readonly IServiceProvider Services;
        private readonly HttpClient ApiClient;

        public CommandHandler(IServiceProvider services)
        {
            //getRequiredService to pass the services into the fields
            config = services.GetRequiredService<IConfiguration>();
            Commands = services.GetRequiredService<CommandService>();
            DiscordClient = services.GetRequiredService<DiscordSocketClient>();
            ApiClient = services.GetRequiredService<HttpClient>();
            Services = services;

            //do the command when a command is executed
            Commands.CommandExecuted += CommandExecuteAsync;

            //take action when a message is received
            DiscordClient.MessageReceived += MessageReceivedAsync;
        }


        public async Task InitializeAsync()
        {
            //register the public modules(commands)
            await Commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
                                          Services);
        }

        public async Task MessageReceivedAsync(SocketMessage rawMessage)
        {
            //ensures we don't process system/other messages entered by bots
            if (!(rawMessage is SocketUserMessage message)) return;
            if (message.Source != MessageSource.User) return;

            //sets the argument position away from the prefix we set
            var argPos = 0;

            //get prefix from config file(!)
            char prefix = Char.Parse(config["Prefix"]);

            //determine if the message has a valid prefic, and adjust argPos based on prefix
            if (!(message.HasMentionPrefix(DiscordClient.CurrentUser, ref argPos) || message.HasCharPrefix(prefix, ref argPos))) return;

            var context = new SocketCommandContext(DiscordClient, message);

            //execute command if one is found that matches
            await Commands.ExecuteAsync(context, argPos, Services);
        }

        public async Task CommandExecuteAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            //if command is not found, log the info to the console and exit
            if(!command.IsSpecified)
            {
                Console.WriteLine($"Command failed to execute for [{context.User.Username}] <-> [{result.ErrorReason}]!");
                return;
            }

            //if command is successful, print success to the console and exit
            if(result.IsSuccess)
            {
                Console.WriteLine($"Command [{command.Value.Name}] executed for -> [{context.User.Username}]");
                return;
            }

            //if it fails, let them know it failed and why
            await context.Channel.SendMessageAsync($"Sorry... something went wrong -> [{result}]!");

        }
    }

}
