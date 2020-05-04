using Discord.Commands;
using DnDSpellBot.Services;
using System.Threading.Tasks;


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
    }
}
