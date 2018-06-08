using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;

namespace Pillowcat.Commands
{

        public class PillowcatHelp : ModuleBase<SocketCommandContext>
        {
            private Random _rnd;

            [Command("help")]
            public async Task AddRandomColor()
            {
                _rnd = new Random();
                var embed = new EmbedBuilder();
                try
                {
                int r = _rnd.Next(255), g = _rnd.Next(255), b = _rnd.Next(255);
                embed.WithDescription("This is a simple help command");
                        embed.WithColor(new Color(r, g, b));
                    await Context.Channel.SendMessageAsync("", false, embed);
                    Console.WriteLine($"{DateTime.Now:G}" + $" : Server: {Context.Guild}, Id: {Context.Guild.Id} || Channel: {Context.Channel} || User: {Context.User} || Used: add");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
