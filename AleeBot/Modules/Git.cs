﻿/*******************************************
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
using Discord.Commands;
using System.Threading.Tasks;

namespace AleeBot.Modules
{
    public class Git : ModuleBase<SocketCommandContext>
    {
        [Command("git")]
        public async Task GitAsync()
        {
            await Context.Channel.SendMessageAsync("Feel free to contribute in the AleeBot repo by following this link!\nhttps://github.com/AleeCorp/AleeBot.NET");
        }
    }
}
