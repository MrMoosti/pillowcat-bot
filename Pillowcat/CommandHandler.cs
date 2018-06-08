using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Pillowcat
{
    public class CommandHandler
    {
        DiscordSocketClient _client;
        CommandService _service;

        public async Task InitializeAsync(DiscordSocketClient client)
        {
            _client = client;
            _service = new CommandService();
            await _service.AddModulesAsync(Assembly.GetEntryAssembly());
            _client.MessageReceived += HandleCommandAsync; //handling a command
        }

        public async Task HandleCommandAsync(SocketMessage s)
        {
            if (!(s is SocketUserMessage msg)) return;
            var context = new SocketCommandContext(_client, msg);
            var argPos = 0;
            if (!context.User.IsBot)
            {
                if (msg.HasStringPrefix(Config.bot.cmdPrefix, ref argPos) || msg.HasMentionPrefix(_client.CurrentUser, ref argPos))
                {
                        var result = await _service.ExecuteAsync(context, argPos);
                        if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                        {
                            var embed = new EmbedBuilder();

                            embed.WithColor(Color.DarkRed);
                            await context.Channel.SendMessageAsync("", false, ErrorHandler.HandleErrors(result, msg));
                            Global.LogError(result.ErrorReason, context.Guild, msg.Author, msg);
                        }
                }
            }
        }
    }
}