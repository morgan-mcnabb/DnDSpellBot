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

namespace DnDSpellBot.Modules
{
    public class SpellModule : ModuleBase
    {
        private readonly HttpClient Client = new HttpClient();
        private Regex StringFind = new Regex(@"^((?:\w+\s?\-?){1,5})$");

        [Command("Spell")]
        public async Task SpellCommand([Remainder]string spellSearch)
        {
            string spellData = await RunAsync(spellSearch);
            await ReplyAsync(spellData);
        }


        private string BuildSpell(Spell spell)
        {
            StringBuilder strSpell = new StringBuilder();

            strSpell.Append("Spell Name: ");
            strSpell.Append(spell.name);
            strSpell.Append("\n");

            strSpell.Append("Spell Description: ");
            strSpell.Append("\n");
            for(int i = 0; i < spell.description.Length; i++)
            {
                strSpell.Append(spell.description[i]);
                strSpell.Append("\t");
                strSpell.Append("\n");
            }
            strSpell.Append("\n");

            strSpell.Append("Spell Base Level: ");
            strSpell.Append(spell.level.ToString());
            strSpell.Append("\n");

            if(spell.higherLevel != null)
            {
                strSpell.Append("Spell at higher level: ");
                strSpell.Append("\n");
                for (int i = 0; i < spell.higherLevel.Length; i++)
                {
                    strSpell.Append(spell.higherLevel[i]);
                    strSpell.Append("\n");
                }
            }

            strSpell.Append("Spell Range: ");
            strSpell.Append(spell.range);
            strSpell.Append("\n");

            strSpell.Append("Spell Components: ");
            if (spell.components.Length < 0) strSpell.Append("This spell requires no components to cast.");
            else
            {
                for(int i = 0; i < spell.components.Length; i++)
                {
                    strSpell.Append(spell.components[i]);
                    if (i != spell.components.Length - 1) strSpell.Append(", ");
                }
            }
            strSpell.Append("\n");

            if(spell.material != null)
            {
                strSpell.Append("Spell Material: ");
                strSpell.Append(spell.material);
                strSpell.Append("\n");
            }

            strSpell.Append("Spell Ritual? ");
            string rit = spell.ritual ? "This spell is a ritual." : "This spell is not a ritual";
            strSpell.Append(rit);
            strSpell.Append("\n");

            strSpell.Append("Spell Duration: ");
            strSpell.Append(spell.duration);
            strSpell.Append("\n");

            strSpell.Append("Spell Concentration? ");
            string con = spell.concentration ? "This spell requires concentration" : "This spell does not require concentration";
            strSpell.Append(con);
            strSpell.Append("\n");

            strSpell.Append("Spell Casting Time: ");
            strSpell.Append(spell.casting_time);
            strSpell.Append("\n");

            strSpell.Append("Spell School: ");
            strSpell.Append(spell.school.name);
            strSpell.Append("\n");

            strSpell.Append("Classes that can use this spell: ");
            for(int i = 0; i < spell.classes.Length; i++)
            {
                strSpell.Append(spell.classes[i].name);
                if (i != spell.classes.Length - 1) strSpell.Append(", ");
            }
            strSpell.Append("\n");

            if(spell.subclasses.Length > 0)
            {
                strSpell.Append("Spell Subclasses: ");
                for (int i = 0; i < spell.subclasses.Length; i++)
                {
                    strSpell.Append(spell.subclasses[i].name);
                    if (i != spell.subclasses.Length - 1) strSpell.Append(", ");
                }
                strSpell.Append("\n");
            }

            return strSpell.ToString();
        }

        internal class Spell
        {
            public int Count = 17;
            public string _id { get; set; }
            public string index { get; set; }
            public string name { get; set; }
            [JsonProperty(PropertyName = "desc")]
            public string[] description { get; set; } //
            [JsonProperty(PropertyName = "higher_level")]
            public string[] higherLevel { get; set; }
            public string range { get; set; }
            public string[] components { get; set; } //
            public string material { get; set; }
            public bool ritual { get; set; }
            public string duration { get; set; }
            public bool concentration { get; set; }
            public string casting_time { get; set; }
            public int level { get; set; }
            public School school { get; set; } //
            public Class[] classes { get; set; } //
            public Subclasses[] subclasses { get; set; } //
            public string url { get; set; }

        }

        internal class Description
        {
            public string description { get; set; }
            public string higherLevel { get;set; }
        }

        internal class School
        {
            public string name { get; set; }
            public string url { get; set; }
        }
        internal class Class
        {
            public string name { get; set; }
            public string url { get; set; }
        }
        internal class Subclasses
        {
            public string name { get; set; }
            public string url { get; set; }
        }


        private async Task<Spell> GetSpells(string path)
        {
            var response = await Client.GetAsync(path);

            if (!response.IsSuccessStatusCode) return null;

            var responseData = response.Content.ReadAsStringAsync().Result;

            var spell = JsonConvert.DeserializeObject<Spell>(responseData);

            return spell;
        }

        private async Task<string> RunAsync(string spellSearch)
        {
            spellSearch.TrimStart()
                       .TrimEnd()
                       .ToLower();
            Match stringMatch = Regex.Match(spellSearch, StringFind.ToString());
            if (!stringMatch.Success) return "A problem occurred... [Invalid Character Operation, likely too many spaces]";
            spellSearch = spellSearch.Replace(' ', '-');

            Client.BaseAddress = new Uri("http://dnd5eapi.co/api/spells/" + spellSearch + "/");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applications/json"));
            try
            {
                var spell = await GetSpells("http://dnd5eapi.co/api/spells/" + spellSearch + "/");
                string stringSpell =  BuildSpell(spell);
                return stringSpell;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

    }
}
