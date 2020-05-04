using System.Linq;
using System.Text;
using Discord.Commands;
using DnDSpellBot.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;



namespace DnDSpellBot.Modules
{
   
    public class AllWeaponsModule : ModuleBase
    {
        [Command("allweapons")]
        public async Task Weapons()
        {
            APIService api = new APIService();
            var allWeapons = await api.GetAllWeapons();

            if(allWeapons.Results.Length == 0)
            {
                await ReplyAsync("Sorry, there are no weapons!");
                return;
            }

            string printData = allWeapons.BuildWeapons();
            //discord bot can't print 2000+ character per reply, so split
            if (printData.Length >= 2000)
            {
                string first = printData.Substring(0, printData.Length / 4);
                string second = printData.Substring(printData.Length / 4, printData.Length / 4);
                string third = printData.Substring(2 * printData.Length / 4, printData.Length / 4);
                string fourth = printData.Substring(3 * printData.Length / 4);

                await ReplyAsync(first);
                await ReplyAsync(second);
                await ReplyAsync(third);
                await ReplyAsync(fourth);
            }

            else await ReplyAsync(printData);
        }

        [Command("getweapon")]
        public async Task FindWeapon([Remainder]string weaponName)
        {
            APIService api = new APIService();
            var weapon = await api.GetWeapon(weaponName);
            if(weapon.Results.Length == 0)
            {
                await ReplyAsync("Sorry, I couldn't find a weapon with that name!");
                return;
            }

            string printData = weapon.BuildWeapons();

            await ReplyAsync(printData);
        }


    }
}
