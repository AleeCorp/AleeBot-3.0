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
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace AleeBot.Modules
{
    public class Help : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task HelpAsync()
        {
            var embed = new EmbedBuilder();
            embed.WithTitle($"AleeBot.NET {Data.Version} Help.");
            embed.WithDescription($"Every command you input into AleeBot is `{Data.prefix}`");
            embed.WithColor(Color.Green);
            embed.AddField("Information:", "help\nping\ngit\nabout\nuptime\nchangelog", true);
            embed.AddField("User Information:", "avatar", true);
            embed.AddField("Bot Owner Only:", "say\npoweroff", true);
            embed.WithFooter("AleeCorp Copyright 2012-2019");
            embed.WithCurrentTimestamp();
            await Context.Channel.SendMessageAsync(embed: embed.Build());
        }
    }
}
