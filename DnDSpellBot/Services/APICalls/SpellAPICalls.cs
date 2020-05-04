using DnDSpellBot.Modules.Classes;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace DnDSpellBot.Services.APICalls
{
    class SpellAPICalls
    {
        //validation for spell string
        private readonly Regex SpellFind = new Regex(@"^((?:\w+\s?\-?){1,5})$");

        //validating spell string and returning spell
        public async Task<Spell> SpellSearchAsync(HttpClient client, string spellSearch)
        {

            //validate spell string
            spellSearch = spellSearch.TrimStart()
                                     .TrimEnd()
                                     .ToLower();
            Match stringMatch = Regex.Match(spellSearch, SpellFind.ToString());
            if (!stringMatch.Success) return null;

            //turn string into meaningful api call
            spellSearch = spellSearch.Replace(' ', '-');

            try
            {
                string path = "spells/" + spellSearch + "/";

                var response = await client.GetAsync(path);
                if (!response.IsSuccessStatusCode) return null;


                var responseData = response.Content.ReadAsStringAsync().Result;


                //turn JSON object into meaningful data
                var spell = JsonConvert.DeserializeObject<Spell>(responseData);

                return spell;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<AllSpells> GetAllSpellsByClassAsync(HttpClient Client, string classN, string Limit)
        {
            try
            {

                //establish connection to API with current page within API
                var response = await Client.GetAsync("spells/?search=" + classN + Limit);
                if (!response.IsSuccessStatusCode) return null;


                var responseData = response.Content.ReadAsStringAsync().Result;

                //turn JSON data into meanigful AllSpell object
                var allSpells = JsonConvert.DeserializeObject<AllSpells>(responseData);

                return allSpells;
            }
            catch
            {
                return null;
            }

        }
    }
}
