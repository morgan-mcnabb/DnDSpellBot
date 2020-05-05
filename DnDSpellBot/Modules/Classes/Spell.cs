using System.Text;
using Newtonsoft.Json;
namespace DnDSpellBot.Modules.Classes
{

    //JSON data
    public partial class Spell
    {
        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("higher_level")]
        public string HigherLevel { get; set; }

        [JsonProperty("range")]
        public string Range { get; set; }

        [JsonProperty("components")]
        public string Components { get; set; }

        [JsonProperty("material")]
        public string Material { get; set; }

        [JsonProperty("ritual")]
        public string Ritual { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("concentration")]
        public string Concentration { get; set; }

        [JsonProperty("casting_time")]
        public string CastingTime { get; set; }

        [JsonProperty("level_int")]
        public int LevelInt { get; set; }

        [JsonProperty("school")]
        public string School { get; set; }

        [JsonProperty("dnd_class")]
        public string DndClass { get; set; }

        [JsonProperty("circles")]
        public string Circles { get; set; }

        //turns JSON data to meaningful string
        //TODO: consolidate
        public string SpellToString()
        {
            StringBuilder strSpell = new StringBuilder();

            strSpell.Append("Spell Name: ");
            strSpell.Append(Name);
            strSpell.Append("\n");

            strSpell.Append("Spell Description: ");
            strSpell.Append("\n");
            strSpell.Append(Desc);
            strSpell.Append("\n\n");

            strSpell.Append("Spell Base Level: ");
            if (LevelInt == 0) strSpell.Append("Cantrip");
            else strSpell.Append(LevelInt.ToString());
            strSpell.Append("\n\n");

            if (HigherLevel != "")
            {
                strSpell.Append("Spell at higher level: ");
                strSpell.Append("\n");
                strSpell.Append(HigherLevel);
                strSpell.Append("\n");
            }

            strSpell.Append("Spell Range: ");
            strSpell.Append(Range);
            strSpell.Append("\n");

            strSpell.Append("Spell Components: ");
            if (Components.Length < 0) strSpell.Append("This requires no components to cast.");
            else strSpell.Append(Components);
            
            strSpell.Append("\n");

            if (Material != "")
            {
                strSpell.Append("Spell Material: ");
                strSpell.Append(Material);
                strSpell.Append("\n");
            }

            strSpell.Append("Spell Ritual? ");
            strSpell.Append(Ritual);
            strSpell.Append("\n");

            strSpell.Append("Spell Duration: ");
            strSpell.Append(Duration);
            strSpell.Append("\n");

            strSpell.Append("Spell Concentration? ");
            strSpell.Append(Concentration);
            strSpell.Append("\n");

            strSpell.Append("Spell Casting Time: ");
            strSpell.Append(CastingTime);
            strSpell.Append("\n");

            strSpell.Append("Spell School: ");
            strSpell.Append(School);
            strSpell.Append("\n");

            strSpell.Append("Classes that can use this: ");
            strSpell.Append(DndClass);
            strSpell.Append("\n");

            
            return strSpell.ToString();
        }

    }
    public partial class AllSpells
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("results")]
        public Spell[] Results { get; set; }
    }
}
