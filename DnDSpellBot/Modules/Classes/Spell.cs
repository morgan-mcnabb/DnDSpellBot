using Discord;
using Discord.Net;
using Discord.WebSocket;
using Discord.Commands;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using DnDSpellBot.Modules.Classes;
using Newtonsoft.Json;
using System.Collections;
using System.Reflection;

namespace DnDSpellBot.Modules.Classes
{

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

    


    //public class Spell
    //{
    //    public int Count = 17;
    //    public string _id { get; set; }
    //    public string index { get; set; }
    //    public string name { get; set; }
    //    [JsonProperty(PropertyName = "desc")]
    //    public string[] description { get; set; } //
    //    [JsonProperty(PropertyName = "higher_level")]
    //    public string[] higherLevel { get; set; }
    //    public string range { get; set; }
    //    public string[] components { get; set; } //
    //    public string material { get; set; }
    //    public bool ritual { get; set; }
    //    public string duration { get; set; }
    //    public bool concentration { get; set; }
    //    public string casting_time { get; set; }
    //    public int level { get; set; }
    //    public School school { get; set; } //
    //    public Class[] classes { get; set; } //
    //    public Subclasses[] subclasses { get; set; } //
    //    public string url { get; set; }
    //    [JsonProperty(PropertyName = "level_int")]
    //    public int level_int { get; set; }
    //    public string dnd_class { get; set; }

    //    public string SpellToString()
    //    {
    //        StringBuilder strSpell = new StringBuilder();

    //        strSpell.Append("Spell Name: ");
    //        strSpell.Append(name);
    //        strSpell.Append("\n");

    //        strSpell.Append("Spell Description: ");
    //        strSpell.Append("\n");
    //        for (int i = 0; i < description.Length; i++)
    //        {
    //            strSpell.Append(description[i]);
    //            strSpell.Append("\n");
    //        }
    //        strSpell.Append("\n");

    //        strSpell.Append("Spell Base Level: ");
    //        if (level == 0) strSpell.Append("Cantrip");
    //        else strSpell.Append(level.ToString());
    //        strSpell.Append("\n");

    //        if (higherLevel != null)
    //        {
    //            strSpell.Append("Spell at higher level: ");
    //            strSpell.Append("\n");
    //            for (int i = 0; i < higherLevel.Length; i++)
    //            {
    //                strSpell.Append(higherLevel[i]);
    //                strSpell.Append("\n");
    //            }
    //        }

    //        strSpell.Append("Spell Range: ");
    //        strSpell.Append(range);
    //        strSpell.Append("\n");

    //        strSpell.Append("Spell Components: ");
    //        if (components.Length < 0) strSpell.Append("This requires no components to cast.");
    //        else
    //        {
    //            for (int i = 0; i < components.Length; i++)
    //            {
    //                if (components[i] == "V") strSpell.Append("Verbal");
    //                else if (components[i] == "S") strSpell.Append("Somatic");
    //                else strSpell.Append("Material");
    //                if (i != components.Length - 1) strSpell.Append(", ");
    //            }
    //        }
    //        strSpell.Append("\n");

    //        if (material != null)
    //        {
    //            strSpell.Append("Spell Material: ");
    //            strSpell.Append(material);
    //            strSpell.Append("\n");
    //        }

    //        strSpell.Append("Spell Ritual? ");
    //        string rit = ritual ? "This is a ritual." : "This is not a ritual";
    //        strSpell.Append(rit);
    //        strSpell.Append("\n");

    //        strSpell.Append("Spell Duration: ");
    //        strSpell.Append(duration);
    //        strSpell.Append("\n");

    //        strSpell.Append("Spell Concentration? ");
    //        string con = concentration ? "This requires concentration" : "This does not require concentration";
    //        strSpell.Append(con);
    //        strSpell.Append("\n");

    //        strSpell.Append("Spell Casting Time: ");
    //        strSpell.Append(casting_time);
    //        strSpell.Append("\n");

    //        strSpell.Append("Spell School: ");
    //        strSpell.Append(school.name);
    //        strSpell.Append("\n");

    //        strSpell.Append("Classes that can use this: ");
    //        for (int i = 0; i < classes.Length; i++)
    //        {
    //            strSpell.Append(classes[i].name);
    //            if (i != classes.Length - 1) strSpell.Append(", ");
    //        }
    //        strSpell.Append("\n");

    //        if (subclasses.Length > 0)
    //        {
    //            strSpell.Append("Spell Subclasses: ");
    //            for (int i = 0; i < subclasses.Length; i++)
    //            {
    //                strSpell.Append(subclasses[i].name);
    //                if (i != subclasses.Length - 1) strSpell.Append(", ");
    //            }
    //            strSpell.Append("\n");
    //        }
    //        return strSpell.ToString();
    //    }
    //    public class Description
    //    {
    //        public string description { get; set; }
    //        public string higherLevel { get; set; }
    //    }

    //    public class School
    //    {
    //        public string name { get; set; }
    //        public string url { get; set; }
    //    }
    //    public class Class
    //    {
    //        public string name { get; set; }
    //        public string url { get; set; }
    //    }
    //    public class Subclasses
    //    {
    //        public string name { get; set; }
    //        public string url { get; set; }
    //    }

    //}
}
