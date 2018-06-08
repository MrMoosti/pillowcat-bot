using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace Pillowcat
{
    internal static class Global
    {
        internal static DiscordSocketClient Client { get; set; }

        internal static void Log(string filePath, string text)
        {
            if (!File.Exists(filePath)) Directory.CreateDirectory(filePath);

            filePath += $"/{DateTime.Now:MMMM, yyyy}";
            if (!File.Exists(filePath)) Directory.CreateDirectory(filePath);

            filePath += $"/{DateTime.Now:dddd, MMMM d, yyyy}.txt";
            using (var sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine($"{DateTime.Now:G}" + $" : " + text);
            }
        }
        internal static void LogError(string error, SocketGuild guild, SocketUser user, SocketMessage msg)
        {
            try
            {
                Log("Data/Log/ErrorLog", $"---Guild Id: {guild.Id}---Guild: {guild.Name}---User: {user.Username}---");
                Log("Data/Log/ErrorLog", msg.Content);
                Log("Data/Log/ErrorLog", error);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }
        internal static string GetJson(string url)
        {
            var uri = new Uri(url);
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = WebRequestMethods.Http.Get;
            var response = (HttpWebResponse)request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException());
            var output = reader.ReadToEnd();
            response.Close();
            return output;
        }
    }
}