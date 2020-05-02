using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DnDSpellBot.Modules.Classes
{
    public partial class Monsters
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

        [JsonProperty("proficiencies")]
        public Proficiency[] Proficiencies { get; set; }

        [JsonProperty("damage_vulnerabilities")]
        public object[] DamageVulnerabilities { get; set; }

        [JsonProperty("damage_resistances")]
        public object[] DamageResistances { get; set; }

        [JsonProperty("damage_immunities")]
        public object[] DamageImmunities { get; set; }

        [JsonProperty("condition_immunities")]
        public object[] ConditionImmunities { get; set; }

        [JsonProperty("senses")]
        public Senses Senses { get; set; }

        [JsonProperty("languages")]
        public string Languages { get; set; }

        [JsonProperty("challenge_rating")]
        public int ChallengeRating { get; set; }

        [JsonProperty("special_abilities")]
        public SpecialAbility[] SpecialAbilities { get; set; }

        [JsonProperty("actions")]
        public Action[] Actions { get; set; }

        [JsonProperty("legendary_actions")]
        public LegendaryAction[] LegendaryActions { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public partial class Action
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("attack_bonus", NullValueHandling = NullValueHandling.Ignore)]
        public int? AttackBonus { get; set; }

        [JsonProperty("dc", NullValueHandling = NullValueHandling.Ignore)]
        public Dc Dc { get; set; }

        [JsonProperty("damage", NullValueHandling = NullValueHandling.Ignore)]
        public Damage[] Damage { get; set; }

        [JsonProperty("usage", NullValueHandling = NullValueHandling.Ignore)]
        public Usage Usage { get; set; }
    }

    public partial class Damage
    {
        [JsonProperty("damage_type")]
        public DcTypeClass DamageType { get; set; }

        [JsonProperty("damage_dice")]
        public string DamageDice { get; set; }

        [JsonProperty("damage_bonus")]
        public int DamageBonus { get; set; }
    }

    public partial class DcTypeClass
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public partial class Dc
    {
        [JsonProperty("dc_type")]
        public DcTypeClass DcType { get; set; }

        [JsonProperty("dc_value")]
        public int DcValue { get; set; }

        [JsonProperty("success_type")]
        public string SuccessType { get; set; }
    }

    public partial class Usage
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("times")]
        public int Times { get; set; }
    }

    public partial class LegendaryAction
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("attack_bonus", NullValueHandling = NullValueHandling.Ignore)]
        public int? AttackBonus { get; set; }

        [JsonProperty("damage", NullValueHandling = NullValueHandling.Ignore)]
        public Damage[] Damage { get; set; }
    }

    public partial class Proficiency
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }
    }

    public partial class Senses
    {
        [JsonProperty("darkvision")]
        public string Darkvision { get; set; }

        [JsonProperty("passive_perception")]
        public int PassivePerception { get; set; }
    }

    public partial class SpecialAbility
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("dc", NullValueHandling = NullValueHandling.Ignore)]
        public Dc Dc { get; set; }
    }

    public partial class Speed
    {
        [JsonProperty("walk")]
        public string Walk { get; set; }

        [JsonProperty("swim")]
        public string Swim { get; set; }
    }

}
