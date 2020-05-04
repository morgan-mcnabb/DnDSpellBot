using System.Text;
using Newtonsoft.Json;

namespace DnDSpellBot.Modules.Classes
{
    public partial class MonstersByCr
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("results")]
        public Results[] Monsters { get; set; }
        public string BuildMonster(string CR)
        {
            StringBuilder strMonsters = new StringBuilder();

            strMonsters.Append("\t");
            strMonsters.Append("All Monsters with CR " + CR + ":");
            strMonsters.Append("\n");

            for (int i = 0; i < Monsters.Length; i++)
            {
                strMonsters.Append(Monsters[i].Name);
                strMonsters.Append("\n");
            }

            return strMonsters.ToString();
        }
    }

    public partial class Results
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    

}

