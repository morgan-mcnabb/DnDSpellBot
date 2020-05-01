using System;
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
        private readonly HttpClient Client = new HttpClient();
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

            Client.BaseAddress = new Uri("http://dnd5eapi.co/api/spells/" + spellSearch + "/");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applications/json"));
            try
            {
                var spell = await GetSpells("http://dnd5eapi.co/api/spells/" + spellSearch + "/");

                return spell;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion
    }
}
