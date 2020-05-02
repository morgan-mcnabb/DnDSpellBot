using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using DnDSpellBot.Modules.Classes;
using Newtonsoft.Json;

namespace DnDSpellBot.Services
{
    public class APIService
    {
        private static readonly HttpClient Client = new HttpClient()
        {
            //BaseAddress = new Uri("http://dnd5eapi.co/api/")
            BaseAddress = new Uri("https://api.open5e.com/")
        };
        private readonly Regex SpellFind = new Regex(@"^((?:\w+\s?\-?){1,5})$");

        #region Spells
        private async Task<Spell> GetSpells(string path)
        {
            var response = await Client.GetAsync(path);

            if (!response.IsSuccessStatusCode) return null;

            var responseData = response.Content.ReadAsStringAsync().Result;

            var spell = JsonConvert.DeserializeObject<Spell>(responseData);

            return spell;
        }

        public async Task<Spell> SpellSearchAsync(string spellSearch)
        {
            spellSearch = spellSearch.TrimStart()
                                     .TrimEnd()
                                     .ToLower();
            Match stringMatch = Regex.Match(spellSearch, SpellFind.ToString());
            if (!stringMatch.Success) return null;
            spellSearch = spellSearch.Replace(' ', '-');

            //Client.BaseAddress = new Uri("http://dnd5eapi.co/api/spells/" + spellSearch + "/");
            //Client.DefaultRequestHeaders.Accept.Clear();
            //Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applications/json"));
            try
            {
                //var spell = await GetSpells("http://dnd5eapi.co/api/spells/" + spellSearch + "/");
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

        public async Task<AllSpells> GetData(string path)
        {
            var response = await Client.GetAsync("spells/?format=json" + path);

            if (!response.IsSuccessStatusCode) return null;

            var responseData = response.Content.ReadAsStringAsync().Result;

            var allSpells = JsonConvert.DeserializeObject<AllSpells>(responseData);

            return allSpells;

        }
        public async Task<List<AllSpells>> GetAllSpells()
        {
            try
            {
                List<AllSpells> listSpells = new List<AllSpells>();

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

    }
}
