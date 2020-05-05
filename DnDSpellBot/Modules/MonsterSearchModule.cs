using Discord.Commands;
using DnDSpellBot.Services;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DnDSpellBot.Modules
{
    
    public class MonsterSearchModule : ModuleBase
    {
        [Command("monster")]
        public async Task MonsterSearch([Remainder]string monsterString)
        {
            APIService api = new APIService();

            var monster = await api.MonsterCall(monsterString);
            string buildMonsterString = monster.MonsterToString();
            if (buildMonsterString.Length >= 2000)
            {
                string firsthalf = buildMonsterString.Substring(0, buildMonsterString.Length / 2);
                string secondhalf = buildMonsterString.Substring(buildMonsterString.Length / 2);

                await ReplyAsync(firsthalf);
                await ReplyAsync(secondhalf);
            }

            else await ReplyAsync(buildMonsterString);
        }

        private readonly Regex RemoveNonNumbers = new Regex(@"[^0-9/]");
        [Command("monstersbycr")]
        public async Task MonstersCRSearch([Remainder]string CR)
        {
            CR = CR.Trim(' ');
            CR = Regex.Replace(CR, @"\s+", string.Empty);

            RemoveNonNumbers.Replace(CR, "");
            APIService api = new APIService();
            var monsters = await api.MonstersByCRCall(CR);
            if (monsters.Results.Length == 0)
            {
                await ReplyAsync("No monsters were found with that challenge rating, sorry!");
                return;
            }

            string reply = monsters.BuildMonster(CR);
            if (reply.Length >= 2000)
            {
                string firsthalf = reply.Substring(0, reply.Length / 2);
                string secondhalf = reply.Substring(reply.Length / 2);

                await ReplyAsync(firsthalf);
                await ReplyAsync(secondhalf);
            }

            else await ReplyAsync(reply);


        }
    }
}
