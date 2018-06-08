using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace Pillowcat
{
        public class Program
        {
            private DiscordSocketClient _client;
            private CommandHandler _handler;
            private string[] _args;

            private static void Main(string[] args)
                => new Program().StartAsync().GetAwaiter().GetResult();

            public async Task StartAsync()
            {
                if (string.IsNullOrEmpty(Config.bot.token)) return;
                _client = new DiscordSocketClient(new DiscordSocketConfig
                {
                    LogLevel = LogSeverity.Verbose
                });
                _client.Log += Log;
                await _client.LoginAsync(TokenType.Bot, Config.bot.token);
                await _client.StartAsync();
                _handler = new CommandHandler();
                await _handler.InitializeAsync(_client);
                Global.Client = _client;
                _client.Ready += Restart;
                await Task.Delay(-1);
            }

            private static Task Log(LogMessage msg)
            {
                Console.WriteLine($"{DateTime.Now:G}" + " : " + msg.Message);
                Global.Log($"Data/Log/BotLog", msg.Message);
                return Task.CompletedTask;
            }
            public async Task Restart()
            {
                while (true)
                {
                    await Task.Delay(1800000);
                    Console.WriteLine("\r\nRestarting...");
                    Console.Clear();
                    await _client.LogoutAsync();
                    await StartAsync();
                }
            }
        }
}
