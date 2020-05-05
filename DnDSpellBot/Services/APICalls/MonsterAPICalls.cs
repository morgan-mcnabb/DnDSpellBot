using DnDSpellBot.Modules.Classes;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DnDSpellBot.Services.APICalls
{
    class MonsterAPICalls
    {
       

        public async Task<MonstersByCr> SearchMonstersByCR(HttpClient Client, string CR, string Limit)
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

        public async Task<Monster> MonsterSearchAsync(HttpClient Client, string monsterSearch)
        {
             Regex MonsterFind = new Regex(@"^((?:\w+\s?\-?){1,4})$");
             Regex RemoveNonLetters = new Regex(@"[^a-zA-Z-\s+]");

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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

    }
}
