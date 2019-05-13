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
using System.Diagnostics;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace AleeBot.Modules
{
    public class Uptime : ModuleBase<SocketCommandContext>
    {
        [Command("uptime")]
        public async Task UptimeAync()
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("AleeBot Uptime");
            embed.WithColor(Color.Green);
            embed.AddField("System Uptime", SysUptime());
            embed.AddField("Bot Uptime", "Coming Soon!");
            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }

        public TimeSpan SysUptime()
        {
                using (var uptime = new PerformanceCounter("System", "System Up Time"))
                {
                    uptime.NextValue();
                    return TimeSpan.FromSeconds(uptime.NextValue());
                }
            
        }
    }
}
