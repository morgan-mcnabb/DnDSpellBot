using Discord.Commands;
using System.Threading.Tasks;
using DnDSpellBot.Modules.Classes;
using DnDSpellBot.Services;

namespace DnDSpellBot.Modules
{
    public class SpellModule : ModuleBase
    {
        [Command("Spell")]
        [Summary("Accepts a spell and searches the DnD 5e API for the spell")]
        public async Task SpellCommand([Remainder]string spellSearch)
        {
            //establish connection to API
            APIService spellAPI = new APIService();

            //get spell data and convert to meaningful string
            var spell = await spellAPI.SpellCall(spellSearch);
            string strSpell = BuildSpell(spell);

            //discord bot can't print 2000+ character per reply, so split
            if (strSpell.Length >= 2000)
            {
                string firsthalf = strSpell.Substring(0, strSpell.Length / 2);
                string secondhalf = strSpell.Substring(strSpell.Length / 2);

                await ReplyAsync(firsthalf);
                await ReplyAsync(secondhalf);
            }

            else await ReplyAsync(strSpell);
        }

        //build the string unless it does not exist/typo
        private string BuildSpell(Spell spell) => spell != null ? spell.SpellToString(): "I couldn't find that spell, sorry!";
    }
}