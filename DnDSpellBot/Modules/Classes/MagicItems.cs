using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace DnDSpellBot.Modules.Classes
{
    public partial class MagicItems
    {
        Regex RemoveUnderscores = new Regex(@"[_]");

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("results")]
        public Items[] Results { get; set; }

        public string BuildMagicItem()
        {
            StringBuilder strMagic = new StringBuilder();
            for(int i = 0; i < Results.Length; i++)
            {
                Items magicItem = Results[i];
                strMagic.Append("Item Name: " + magicItem.Name);
                strMagic.Append("\n");

                strMagic.Append(magicItem.Name + "'s Type: " + magicItem.Type);
                strMagic.Append("\n");

                strMagic.Append(magicItem.Name + "'s Description: " + RemoveUnderscores.Replace(magicItem.Desc, ""));
                strMagic.Append("\n");

                strMagic.Append(magicItem.Name + "'s Rarity: " + magicItem.Rarity);
                strMagic.Append("\n");

                if (magicItem.RequiresAttunement.Length == 0) strMagic.Append(magicItem.Name + " Does not require attunement.");
                else strMagic.Append(magicItem.Name + " Requires attunement.");
                strMagic.Append("\n");
            }

            return strMagic.ToString();

        }
    }

    public partial class Items
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("rarity")]
        public string Rarity { get; set; }

        [JsonProperty("requires_attunement")]
        public string RequiresAttunement { get; set; }

    }
}
