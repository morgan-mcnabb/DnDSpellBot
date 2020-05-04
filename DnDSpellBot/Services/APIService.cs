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
        private readonly Regex WeaponFind = new Regex(@"^((?:\w+\s?\-?){1,2})$");
        private readonly Regex RemoveNonLetters = new Regex(@"[^a-zA-Z-\s+]");
        private const string Limit = "&limit=321";


        #region Methods
        public async Task<Weapons> GetAllWeapons()
        {
            try
            {
                var response = await Client.GetAsync("weapons/");

                if (!response.IsSuccessStatusCode) return null;

                var responseData = response.Content.ReadAsStringAsync().Result;

                var weapons = JsonConvert.DeserializeObject<Weapons>(responseData);

                return weapons;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Weapons> GetWeapon(string weapon)
        {
            weapon = weapon.TrimEnd()
                           .TrimStart()
                           .ToLower();
            string[] words;
            if (weapon.Contains(' '))
            {
                words = weapon.Split(' ');
                weapon = words[1] + ' ' + words[0];
            }

            try
            {
                var response = await Client.GetAsync("/weapons/?search=" + weapon);

                if (!response.IsSuccessStatusCode) return null;

                var responseData = response.Content.ReadAsStringAsync().Result;

                var weapons = JsonConvert.DeserializeObject<Weapons>(responseData);

                return weapons;
            }
            catch
            {
                return null;
            }
            
        }
        

        #region Spells
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
                string path = "spells/" + spellSearch + "/";

                var response = await Client.GetAsync(path);
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
        #endregion

        #region SpellsByClass

        //logic for parsing pages in API
        public async Task<AllSpells> GetAllSpells(string classN)
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

        #region MonstersByCr
        public async Task<MonstersByCr> GetMonstersByCR(string CR)
        {
            
            try
            {
                var response = await Client.GetAsync("monsters/?challenge_rating=" + CR + Limit);
                if (!response.IsSuccessStatusCode) return null;

                var responseData = response.Content.ReadAsStringAsync().Result;


                //turn JSON object into meaningful data
                var monsters = JsonConvert.DeserializeObject<MonstersByCr>(responseData);

                return monsters;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #endregion
    }
}
