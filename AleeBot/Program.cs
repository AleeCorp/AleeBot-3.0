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
using Discord.Commands;
using System.Reflection;

namespace AleeBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = $"AleeBot {Data.Version} Console";
            Console.WriteLine("Starting AleeBot.NET");
            Console.WriteLine($"Version: {Data.Version}\n");
            Console.WriteLine("Machine Name: " + Environment.MachineName);
            Console.WriteLine("OS Version: " + Environment.OSVersion);
            Console.WriteLine("\n");
            if (File.Exists("token.txt"))
            {
                new Program().MainAsync().GetAwaiter().GetResult();
            }
            else
            {
                Console.WriteLine("[ERROR] token.txt isn't found.");
                Environment.Exit(0);
            }
        }

        private DiscordSocketClient _client;
        private CommandService _commands;

        public async Task MainAsync()
        {

            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _client.Log += Log;

            await _client.LoginAsync(TokenType.Bot, File.ReadAllText("token.txt"));
            await _client.StartAsync();
            await _client.SetGameAsync(name: $"AleeBot {Data.Version} | {Data.prefix}help");

            await InstallCommandsAsync();

            _client.Ready += () =>
            {
                Console.WriteLine($"[SUCCESS] AleeBot {Data.Version} is now ready!");

                return Task.CompletedTask;
            };

            await Task.Delay(-1);
        }

        public async Task InstallCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
                                            services: null);
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            int argPos = 0;

            if (!(message.HasStringPrefix(Data.prefix, ref argPos) ||
                message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
                return;

            var context = new SocketCommandContext(_client, message);

            var result = await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: null);
        }



        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }

}
