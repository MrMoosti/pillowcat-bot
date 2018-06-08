using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Pillowcat
{
    internal static class ErrorHandler
    {
        internal static EmbedBuilder HandleErrors(IResult result, SocketUserMessage msg)
        {
            var embed = new EmbedBuilder();
            var context = new SocketCommandContext(Global.Client, msg);

            switch (result.ToString())
            {
                case "BadArgCount: The input text has too many parameters.":
                    embed.WithDescription(":no_entry_sign: The input has to many parameters");
                    break;
                case "BadArgCount: The input text has too few parameters.":
                    embed.WithDescription(":no_entry_sign: The input text has too few parameters.");
                    break;
                case "UnmetPrecondition: User requires guild permission Administrator":
                    embed.WithDescription(":no_entry_sign: You need admin permissions for this command.");
                    break;
                case "UnmetPrecondition: User requires guild permission ManageRoles":
                    embed.WithDescription(":no_entry_sign: You need Manage Roles permissions for this command.");
                    break;
                default:
                    if (result.ToString().Contains("responded with error 403: Forbidden"))
                    {
                        embed.WithDescription(":arrow_up: PLS drag my role and all the colors to the top of the list :arrow_up:\nOtherwise i can't change the colors of people with a higher rank :sob: ");
                    }
                    else if (result.ToString().Contains("ParseFailed: Failed to parse Int32"))
                    {
                        if (msg.Content.Contains("color add")) embed.WithDescription($"Incorect rgb value. Correct example:\n**add pink 255 105 180**\nMore info on https://htmlcolorcodes.com.");
                    }
                    else if (result.ToString().Contains("Input string was not in a correct format."))
                    {
                        if (msg.Content.Contains("color add")) embed.WithDescription($"Incorect hex code. Correct example:\n**add Pink #FF00FB**\nMore info on https://htmlcolorcodes.com.");
                    }
                    else if (result.ToString().Contains("The server responded with error 50013: Missing Permissions"))
                    {
                        if (msg.Content.Contains("color add")) embed.WithDescription($"Im missing the correct Permissions! Pls reinvite me with the invite link.\n https://discordapp.com/api/oauth2/authorize?client_id=436515089441488907&permissions=469985280&scope=bot.");
                    }
                    else embed.WithDescription("an unexpected error occurred. Pls try again.\n" +
                                               "Join this discord for more help: https://discord.gg/FMTrKqa.");
                    break;

            }
            return embed;
        }
    }
}
