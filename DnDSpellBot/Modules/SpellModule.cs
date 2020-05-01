using Discord.Commands;
using System.Threading.Tasks;
using DnDSpellBot.Modules.Classes;
using DnDSpellBot.Services;

namespace DnDSpellBot.Modules
{
    public class SpellModule : ModuleBase
    {
        [Command("Spell")]
        [Summary("Accepts a spell and searches the Dnd 5e API for the spell")]
        public async Task SpellCommand([Remainder]string spellSearch)
        {
            APIService spellAPI = new APIService();
            var spell = await spellAPI.SpellSearchAsync(spellSearch);
            string strSpell = BuildSpell(spell);
            if (strSpell.Length >= 2000)
            {
                string firsthalf = strSpell.Substring(0, strSpell.Length / 2);
                string secondhalf = strSpell.Substring(strSpell.Length / 2);
                await ReplyAsync(firsthalf);
                await ReplyAsync(secondhalf);
            }
            else await ReplyAsync(strSpell);
        }

        private string BuildSpell(Spell spell) => spell != null ? spell.SpellToString(): "I couldn't find that spell, sorry!";
    }
}