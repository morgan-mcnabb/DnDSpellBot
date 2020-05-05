using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace DnDSpellBot.Modules.Classes
{
    public partial class Classes
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("hit_dice")]
        public string HitDice { get; set; }

        [JsonProperty("hp_at_1st_level")]
        public string HpAt1StLevel { get; set; }

        [JsonProperty("hp_at_higher_levels")]
        public string HpAtHigherLevels { get; set; }

        [JsonProperty("prof_armor")]
        public string ProfArmor { get; set; }

        [JsonProperty("prof_weapons")]
        public string ProfWeapons { get; set; }

        [JsonProperty("prof_tools")]
        public string ProfTools { get; set; }

        [JsonProperty("prof_saving_throws")]
        public string ProfSavingThrows { get; set; }

        [JsonProperty("prof_skills")]
        public string ProfSkills { get; set; }

        [JsonProperty("equipment")]
        public string Equipment { get; set; }

        [JsonProperty("table")]
        public string Table { get; set; }

        [JsonProperty("spellcasting_ability")]
        public string SpellcastingAbility { get; set; }

        [JsonProperty("subtypes_name")]
        public string SubtypesName { get; set; }

        [JsonProperty("archetypes")]
        public Archetype[] Archetypes { get; set; }

        public string BuildClassString()
        {
            StringBuilder strClass = new StringBuilder();

            strClass.Append("Class Name: " + Name);
            strClass.Append("\n");

            strClass.Append(Name + " Description: " + Desc);
            strClass.Append("\n");

            strClass.Append(Name + " Hit Dice: " + HitDice);
            strClass.Append("\n");

            strClass.Append(Name + " HP At 1st Level: " + HpAt1StLevel);
            strClass.Append("\n");

            strClass.Append(Name + " Armor Proficiencies: " + ProfArmor);
            strClass.Append("\n");

            strClass.Append(Name + " Weapon Proficiencies: " + ProfWeapons);
            strClass.Append("\n");

            strClass.Append(Name + " Tool Proficiencies: " + ProfTools);
            strClass.Append("\n");

            strClass.Append(Name + " Saving Throws: " + ProfSavingThrows);
            strClass.Append("\n");

            strClass.Append(Name + " Skill Proficiencies: " + ProfSkills);
            strClass.Append("\n");

            strClass.Append(Name + " Starting Equipment: " + Equipment);
            strClass.Append("\n");

            strClass.Append(Name + " Table: ");
            strClass.Append("\n");
            strClass.Append(Table);
            strClass.Append("\n");

            if (SpellcastingAbility.Length != 0) strClass.Append(Name + " Spellcasting Ability: " + "\n");

            if(Archetypes.Length != 0)
            {
                strClass.Append(Name + " Archetypes: ");
                strClass.Append("\n");
                for(int i = 0; i < Archetypes.Length; i++)
                {
                    strClass.Append("\t" + Archetypes[i].Name);
                    strClass.Append("\n");

                    strClass.Append("\t" + Archetypes[i].Desc);
                    strClass.Append("\n");
                }
            }

            return strClass.ToString();
        }
    }

    public partial class Archetype
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }
    }

}
