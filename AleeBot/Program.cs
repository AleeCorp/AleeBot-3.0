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

namespace AleeBot
{
     public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = $"AleeBot {Data.Version} Console";
            Console.WriteLine("Starting AleeBot.NET");
            Console.WriteLine($"Version: {Data.Version}\n");
            Console.WriteLine("Machine Name: ", Environment.MachineName);
            Console.WriteLine("OS Version: ", Environment.OSVersion);
            Console.WriteLine("\n");
            if (File.Exists("token.txt"))
            {
                new Program().MainAsync().GetAwaiter().GetResult();
            } else
            {
                Console.WriteLine("[ERROR] token.txt isn't found.");
                Environment.Exit(0);
            }
        }

        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public async Task MainAsync()
        {

            _client = new DiscordSocketClient();
            _commands = new CommandService();

            #if DEBUG
            _client.Log += Log;
            #endif

            await _client.LoginAsync(TokenType.Bot, File.ReadAllText("token.txt"));
            await _client.StartAsync();
            await _client.SetGameAsync(name:$"AleeBot {Data.Version} | {Data.prefix}help");
            
            _client.MessageReceived += Message;

            _client.Ready += () =>
            {
                Console.WriteLine($"[SUCCESS] AleeBot {Data.Version} is now ready!");

                return Task.CompletedTask;
            };

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task Message(SocketMessage message)
        {

            if (message.Content == Data.prefix + "help")
            {
                var embed = new EmbedBuilder();
                embed.WithTitle($"AleeBot.NET {Data.Version} Help.");
                embed.WithDescription($"Every command you input into AleeBot is `{Data.prefix}`");
                embed.WithColor(Color.Green);
                embed.AddField("Information:", "help\nping\ngit\nabout\nuptime\nchangelog");
                embed.AddField("Bot Owner Only:", "poweroff\nreboot");
                embed.WithFooter("AleeCorp Copyright 2012-2019");
                embed.WithCurrentTimestamp();
                await message.Channel.SendMessageAsync(embed: embed.Build());
            }
            else if (message.Content == Data.prefix + "ping")
            {
                await message.Channel.SendMessageAsync($"<@{message.Author.Id}>, 🏓 Pong!");
            }
            else if (message.Content == Data.prefix + "poweroff")
            {
                if (message.Author.Id == 242775871059001344)
                {
                    await message.Channel.SendMessageAsync("⚠ AleeBot will now exit!");
                    Console.WriteLine("[INFO] AleeBot is powering off...");
                    await _client.StopAsync();
                    Console.WriteLine("[SUCCESS] Press any key to exit...");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                else
                {
                    await message.Channel.SendMessageAsync($"<@{message.Author.Id}>, You don't have permissions to power me off...");
                }
            }
            else if (message.Content == Data.prefix + "reboot")
            {
                if (message.Author.Id == 242775871059001344)
                {
                    await message.Channel.SendMessageAsync("⚠ AleeBot will now reboot!");
                    await _client.StopAsync();
                    Console.WriteLine("[INFO] AleeBot is restarting...");
                    await MainAsync();
                }
                else
                {
                    await message.Channel.SendMessageAsync($"<@{message.Author.Id}>, You don't have permissions to reboot me...");
                }

            }
            else if (message.Content == Data.prefix + "git")
            {
                await message.Channel.SendMessageAsync("Feel free to contribute in the AleeBot repo by following this link!\nhttps://github.com/AleeCorp/AleeBot.NET");
            }
            else if (message.Content == Data.prefix + "about")
            {
                var embed = new EmbedBuilder();
                embed.WithTitle($"About AleeBot {Data.Version}");
                embed.WithColor(Color.Green);
                embed.AddField("Server Information", $"Machine Name: {Environment.MachineName}\nOS Version: {Environment.OSVersion}\n");
                embed.AddField("Contributors", "Andrew (Alee14) - Original creator of AleeBot 1.0 and 2.0");
                embed.AddField("Built on", ".NET Core 3 Preview");
                embed.WithFooter("AleeCorp Copyright 2012-2019, Licensed with GPL-3.0");
                await message.Channel.SendMessageAsync(embed: embed.Build());
            }
            else if (message.Content == Data.prefix + "uptime")
            {
                var embed = new EmbedBuilder();
                embed.WithTitle("AleeBot Uptime");
                embed.WithColor(Color.Green);
                embed.AddField("System Uptime", "Coming Soon!");
                embed.AddField("Bot Uptime", "Coming Soon!");
                await message.Channel.SendMessageAsync(embed: embed.Build());
            }
            else if (message.Content == Data.prefix + "changelog")
            {
                var embed = new EmbedBuilder();
                embed.WithTitle("AleeBot Changelog");
                embed.WithColor(Color.Green);
                embed.WithDescription($"Changelog for AleeBot {Data.Version}");
                embed.AddField("What's new?", "- Added Uptime\n- Changed the help command\n- Added a changelog command");
                embed.WithFooter("Thanks for using AleeBot!");
                await message.Channel.SendMessageAsync(embed: embed.Build());
            }
        }
    }
}
