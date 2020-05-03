using DnDSpellBot.Modules.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DnDSpellBot.Services
{
    public class APIService
    {
        //establish http client pointed at 5e API
        private static readonly HttpClient Client = new HttpClient()
        {
            BaseAddress = new Uri("https://api.open5e.com/")
        };
        //validation for spell string
        private readonly Regex SpellFind = new Regex(@"^((?:\w+\s?\-?){1,5})$");
        private readonly Regex MonsterFind = new Regex(@"^((?:\w+\s?\-?){1,4})$");
        private readonly Regex RemoveNonLetters = new Regex(@"[^a-zA-Z-\s+]");

        #region Spells

        //establishes connection to API and returns Spell object
        private async Task<Spell> GetSpells(string path)
        {
            //consume RESTful API
            var response = await Client.GetAsync(path);
            if (!response.IsSuccessStatusCode) return null;


            var responseData = response.Content.ReadAsStringAsync().Result;


            //turn JSON object into meaningful data
            var spell = JsonConvert.DeserializeObject<Spell>(responseData);

            return spell;
        }

        //validating spell string and returning spell
        public async Task<Spell> SpellSearchAsync(string spellSearch)
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
                //get spell
                var spell = await GetSpells("spells/" + spellSearch + "/");


                return spell;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

        #region SpellsByClass

        //retrieves all spell data for current page
        public async Task<AllSpells> GetData(string path)
        {
            //establish connection to API with current page within API
            var response = await Client.GetAsync("spells/?format=json" + path);
            if (!response.IsSuccessStatusCode) return null;


            var responseData = response.Content.ReadAsStringAsync().Result;

            //turn JSON data into meanigful AllSpell object
            var allSpells = JsonConvert.DeserializeObject<AllSpells>(responseData);

            return allSpells;
        }

        //logic for parsing pages in API
        public async Task<List<AllSpells>> GetAllSpells()
        {
            try
            {
                List<AllSpells> listSpells = new List<AllSpells>();

                //logic for page path in API, calls all spell pages and adds them to list
                for (int i = 1; i < 8; i++)
                {
                    string path = "&page=" + i;
                    listSpells.Add(await GetData(path));
                }

                return listSpells;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Monsters
        public async Task<Monster> GetMonster(string monsterSearch)
        {
            //validate monster string
            monsterSearch = monsterSearch.TrimStart()
                                 .TrimEnd()
                                 .ToLower();
            monsterSearch = RemoveNonLetters.Replace(monsterSearch, "");
            Match stringMatch = Regex.Match(monsterSearch, MonsterFind.ToString());
            if (!stringMatch.Success) return null;

            //turn string into meaningful api call
            monsterSearch = monsterSearch.Replace(' ', '-');
            try
            {
                //consume RESTful API
                var response = await Client.GetAsync("monsters/" + monsterSearch + "/");
                if (!response.IsSuccessStatusCode) return null;


                var responseData = response.Content.ReadAsStringAsync().Result;


                //turn JSON object into meaningful data
                var monster = JsonConvert.DeserializeObject<Monster>(responseData);

                return monster;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        #endregion

    }
}
