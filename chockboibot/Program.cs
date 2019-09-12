using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using DotNetEnv;
using Discord.Commands;

namespace chockboibot
{
    class Program
    {
        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandHandler _handler;

        public async Task MainAsync()
        {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Env.Load(appdata + @"\config.env");
            var token = Environment.GetEnvironmentVariable("discordToken");
            if (String.IsNullOrEmpty(token)) return;

            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });

            _client.Log += Log;
            Initialize _in = new Initialize(client: _client);

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            _handler = new CommandHandler(services: _in.BuildServiceProvider(), client: _client, commands: new CommandService());
            await _handler.InstallCommandsAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
