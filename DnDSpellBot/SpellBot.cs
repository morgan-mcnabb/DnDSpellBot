using System;
using Discord;
using Discord.Net;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DnDSpellBot.Services;


namespace DnDSpellBot
{
    class SpellBot
    {
        //setup our fields we assign later
        private readonly IConfiguration Config;
        private DiscordSocketClient DiscordClient;

        public SpellBot()
        {
            //create the configuration file
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(path: "config.json");
            //build the configuration and assign it to Config
            Config = builder.Build();
        }
        public async Task MainAsync()
        {
            //call ConfigureServices to create a ServiceCollection/Provider for passing around the services
            using (var services = ConfigureServices())
            {
                //get the discord client and assign it
                var client = services.GetRequiredService<DiscordSocketClient>();
                DiscordClient = client;

                //setup the logging and ready events
                client.Log += LogAsync;
                client.Ready += ReadyAsync;
                services.GetRequiredService<CommandService>().Log += LogAsync;

                //this is where we get the Token value from the configuration file and start the bot
                await client.LoginAsync(TokenType.Bot, Config["Token"]);
                await client.StartAsync();

                //get the CommandHandler service and initialize(Start) it
                await services.GetRequiredService<CommandHandler>().InitializeAsync();

                await Task.Delay(-1);
            }
        }

        private Task LogAsync(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
        
        private Task ReadyAsync()
        {
            Console.WriteLine($"Connected as ->  [{DiscordClient.CurrentUser}]");
            return Task.CompletedTask;
        }

        //handles ServiceCollection creation/configuration and builds out the service provider
        private ServiceProvider ConfigureServices()
        {
            //returns the ServiceProvider for use
            return new ServiceCollection()
                .AddSingleton(Config)
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandler>()
                .BuildServiceProvider();
        }
    }
}
