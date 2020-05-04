using Discord.Commands;
using DnDSpellBot.Services;
using System.Threading.Tasks;

namespace DnDSpellBot.Modules
{
    public class MagicItemModule : ModuleBase
    {
        [Command("magicitem")]
        public async Task GetMagicItems([Remainder] string magicItem)
        {
            APIService api = new APIService();
            var item = await api.MagicItemCall(magicItem);

            if(item == null)
            {
                await ReplyAsync("Sorry, I couldn't find that magic item!");
                return;
            }

            string printData = item.BuildMagicItem();
            await ReplyAsync(printData);
        }
    }
}
