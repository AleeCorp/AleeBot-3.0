/*******************************************
 * 
 *  AleeBot.NET: A Discord bot that's made in Discord.NET, .NET Core 3.0 and a revival.
 *  Copyright (C) 2019 AleeCorp
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <https://www.gnu.org/licenses/>.
 *
 **********************************************/

using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace AleeBot
{
     public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting AleeBot.NET");
            Console.WriteLine("Version: 3.0 Beta 1\n");
            Console.WriteLine("Machine Name: " + Environment.MachineName);
            Console.WriteLine("OS Version: " + Environment.OSVersion);
            Console.WriteLine("\n");
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        private DiscordSocketClient _client;

        public async Task MainAsync()
        {

            _client = new DiscordSocketClient();

            _client.Log += Log;

            await _client.LoginAsync(TokenType.Bot, File.ReadAllText("config.json"));
            await _client.StartAsync();

            _client.MessageReceived += Message;
            
            // Block this task until the program is closed.
            await Task.Delay(-1);
        }
        private async Task Message(SocketMessage message)
        {
            if (message.Content == "ab:ping")
            {
                await message.Channel.SendMessageAsync("Pong! Running on .NET Core!");
            }
            else if (message.Content == "ab:version")
            {
                await message.Channel.SendMessageAsync("AleeBot 3.0 Beta 1");
            }
            else if (message.Content == "ab:embed")
            {
                Color darkGrey = new Color(96, 125, 139);
                var embed = new EmbedBuilder
                {
                    Title = "Hello World",
                    Color = darkGrey,
                    Description = "This is a embed!",
                };
                await message.Channel.SendMessageAsync(embed: embed.Build());
            }
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
