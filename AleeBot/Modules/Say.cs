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
using Discord.Commands;
using Discord;
using System.Threading.Tasks;

namespace AleeBot.Modules
{
    public class Say : ModuleBase<SocketCommandContext>
    {
        [Command("say")]
        public async Task SayAsync(string echo)
        {
            if (Context.User.Id == 242775871059001344)
            {
                if (ChannelPermissions.Text.ManageMessages == false)
                {
                    await Context.Message.DeleteAsync();
                    await ReplyAsync(echo);
                }
                else
                {
                    await ReplyAsync(echo);
                }
            }
            else
            {
                await ReplyAsync($"{Context.User.Mention}, You don't have permissions to use this command.");
            }
        }
    }
}
