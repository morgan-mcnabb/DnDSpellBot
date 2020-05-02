using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DnDSpellBot.Modules.Classes
{
    //JSON data
    public partial class Monster
    {
        [JsonProperty("index")]
        public string Index { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("subtype")]
        public object Subtype { get; set; }

        [JsonProperty("alignment")]
        public string Alignment { get; set; }

        [JsonProperty("armor_class")]
        public int ArmorClass { get; set; }

        [JsonProperty("hit_points")]
        public int HitPoints { get; set; }

        [JsonProperty("hit_dice")]
        public string HitDice { get; set; }

        [JsonProperty("speed")]
        public Speed Speed { get; set; }

        [JsonProperty("strength")]
        public int Strength { get; set; }

        [JsonProperty("dexterity")]
        public int Dexterity { get; set; }

        [JsonProperty("constitution")]
        public int Constitution { get; set; }

        [JsonProperty("intelligence")]
        public int Intelligence { get; set; }

        [JsonProperty("wisdom")]
        public int Wisdom { get; set; }

        [JsonProperty("charisma")]
        public int Charisma { get; set; }

        [JsonProperty("strength_save", NullValueHandling = NullValueHandling.Ignore)]
        public int? StrengthSave { get; set; }

        [JsonProperty("dexterity_save", NullValueHandling = NullValueHandling.Ignore)]
        public int?  DexteritySave { get; set; }

        [JsonProperty("constitution_save", NullValueHandling = NullValueHandling.Ignore)]
        public int? ConstitutionSave { get; set; }

        [JsonProperty("intelligence_save", NullValueHandling = NullValueHandling.Ignore)]
        public int? IntelligenceSave { get; set; }

        [JsonProperty("wisdom_save", NullValueHandling = NullValueHandling.Ignore)]
        public int? WisdomSave { get; set; }

        [JsonProperty("charisma_save", NullValueHandling = NullValueHandling.Ignore)]
        public int? CharismaSave { get; set; }

        [JsonProperty("perception", NullValueHandling = NullValueHandling.Ignore)]
        public int? Perception { get; set; }

        [JsonProperty("proficiencies")]
        public Proficiency[] Proficiencies { get; set; }

        [JsonProperty("damage_vulnerabilities")]
        public string DamageVulnerabilities { get; set; }

        [JsonProperty("damage_resistances")]
        public string DamageResistances { get; set; }

        [JsonProperty("damage_immunities")]
        public string DamageImmunities { get; set; }

        [JsonProperty("condition_immunities")]
        public string ConditionImmunities { get; set; }

        [JsonProperty("senses")]
        public string Senses { get; set; }

        [JsonProperty("languages")]
        public string Languages { get; set; }

        [JsonProperty("challenge_rating")]
        public string ChallengeRating { get; set; }

        [JsonProperty("special_abilities")]
        public SpecialAbility[] SpecialAbilities { get; set; }

        [JsonProperty("actions")]
        public Action[] Actions { get; set; }

        [JsonProperty("reactions")]
        public string Reactions { get; set; }

        [JsonProperty("legendary_desc")]
        public string LegendaryDescription { get; set; }

        [JsonProperty("legendary_actions")]
        public LegendaryAction[] LegendaryActions { get; set; }

        [JsonProperty("spell_list")]
        public string[] SpellList { get; set; }

        public string MonsterToString()
        {
            StringBuilder strMonster = new StringBuilder();

            strMonster.Append("Monster Name: " + Name);
            strMonster.Append("\n");

            strMonster.Append(Name + " Size: " + Size);
            strMonster.Append("\n");

            strMonster.Append(Name + " Type: " + Type);
            strMonster.Append("\n");

            strMonster.Append(Name + " Armor Class: " + ArmorClass);
            strMonster.Append("\n");

            strMonster.Append(Name + " HP: " + HitPoints);
            strMonster.Append("\n");

            if(Speed.Walk != null) strMonster.Append(Name + " Walk Speed: " + Speed + "\n");
            if(Speed.Swim != null) strMonster.Append(Name + " Swim Speed: " + Speed + "\n");

            if(StrengthSave != null) strMonster.Append(Name + " Strength Save: " + StrengthSave + "\n");
            if(DexteritySave != null) strMonster.Append(Name + " Dexterity Save: " + DexteritySave + "\n");
            if(ConstitutionSave != null) strMonster.Append(Name + " Constitution Save: " + ConstitutionSave + "\n");
            if(IntelligenceSave != null) strMonster.Append(Name + " Intelligence Save: " + IntelligenceSave + "\n");
            if(WisdomSave != null) strMonster.Append(Name + " Wisdom Save: " + WisdomSave + "\n");
            if(CharismaSave != null) strMonster.Append(Name + " Charisma Save: " + CharismaSave + "\n");

            if (Perception != null) strMonster.Append(Name + " Perception: " + Perception + "\n");

            if(Proficiencies != null)
            {
                strMonster.Append(Name + " Proficiencies: ");
                strMonster.Append("\n");
                for (int i = 0; i < Proficiencies.Length; i++)
                {
                    strMonster.Append(Proficiencies[i].Name + ": +" + Proficiencies[i].Value);
                    strMonster.Append("\n");
                }
            }
            strMonster.Append("\n");

            if (DamageVulnerabilities != "") strMonster.Append(Name + " Damage Vulnerabilities: " + DamageVulnerabilities + "\n");
            if (DamageResistances != "") strMonster.Append(Name + " Damage Resistances: " + DamageResistances + "\n");
            if (DamageImmunities != "") strMonster.Append(Name + " Damage Immunities: " + DamageImmunities + "\n");
            if (ConditionImmunities != "") strMonster.Append(Name + " Damage Immunities: " + ConditionImmunities + "\n");

            if (Senses != "") strMonster.Append(Name + " Senses " + Senses);
            strMonster.Append("\n");

            if (Languages != "") strMonster.Append(Name + " Langauges: " + Languages);
            strMonster.Append("\n");

            strMonster.Append(Name + " Challenge Rating(CR): " + ChallengeRating);
            strMonster.Append("\n");

            if(Actions.Length != 0)
            {
                strMonster.Append(Name + " Actions: ");
                strMonster.Append("\n");
                for(int i = 0; i < Actions.Length; i++)
                {
                    strMonster.Append(Actions[i].Name);
                    strMonster.Append("\n");

                    strMonster.Append(Actions[i].Desc);
                    strMonster.Append("\n");

                    if (Actions[i].AttackBonus != null) strMonster.Append("Attack Bonus: +" + Actions[i].AttackBonus + "\n");
                    if (Actions[i].DamageDice != "") strMonster.Append("Damage Dice: " + Actions[i].DamageDice + "\n");
                    if (Actions[i].DamageBonus != null) strMonster.Append("Damage Bonus: +" + Actions[i].DamageBonus + "\n");
                }
            }
            strMonster.Append("\n");

            if (Reactions != "") strMonster.Append(Name + " Reactions: " + Reactions + "\n");

            if (LegendaryDescription != "") strMonster.Append(Name + " Legendary Description: " + LegendaryDescription + "\n");

            if(LegendaryActions != null)
            {
                strMonster.Append(Name + " Legendary Action(s): ");
                strMonster.Append("\n");
                for(int i = 0; i < LegendaryActions.Length; i++)
                {
                    strMonster.Append(LegendaryActions[i].Name);
                    strMonster.Append("\n");

                    strMonster.Append(LegendaryActions[i].Desc);
                    strMonster.Append("\n");

                    if (LegendaryActions[i].AttackBonus != null) strMonster.Append("Attack Bonus: +" + LegendaryActions[i].AttackBonus + "\n");
                    if (LegendaryActions[i].DamageDice != "") strMonster.Append("Damage Dice: " + LegendaryActions[i].DamageDice + "\n");
                    if (LegendaryActions[i].DamageBonus != null) strMonster.Append("Damage Bonus: +" + LegendaryActions[i].DamageBonus + "\n");
                }
            }

            if(SpecialAbilities != null)
            {
                strMonster.Append(Name + " Special Abilities: ");
                strMonster.Append("\n");

                for (int i = 0; i < SpecialAbilities.Length; i++)
                {
                    strMonster.Append(SpecialAbilities[i].Name);
                    strMonster.Append("\n");

                    strMonster.Append(SpecialAbilities[i].Desc);
                    strMonster.Append("\n");

                }

            }

            return strMonster.ToString();
        }
    }

    //JSON data
    public partial class Action
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("attack_bonus", NullValueHandling = NullValueHandling.Ignore)]
        public int? AttackBonus { get; set; }

        [JsonProperty("damage_dice", NullValueHandling = NullValueHandling.Ignore)]
        public string DamageDice { get; set; }

        [JsonProperty("damage_bonus", NullValueHandling = NullValueHandling.Ignore)]
        public int? DamageBonus { get; set; }

    }

    //JSON data
    public partial class LegendaryAction
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("attack_bonus", NullValueHandling = NullValueHandling.Ignore)]
        public int? AttackBonus { get; set; }

        [JsonProperty("damage_dice", NullValueHandling = NullValueHandling.Ignore)]
        public string DamageDice { get; set; }

        [JsonProperty("damage_bonus", NullValueHandling = NullValueHandling.Ignore)]
        public int? DamageBonus { get; set; }

    }

    //JSON data
    public partial class Proficiency
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }
    }

    //JSON data
    public partial class SpecialAbility
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }
    }

    //JSON data
    public partial class Speed
    {
        [JsonProperty("walk")]
        public string Walk { get; set; }

        [JsonProperty("swim")]
        public string Swim { get; set; }
    }


    
}
