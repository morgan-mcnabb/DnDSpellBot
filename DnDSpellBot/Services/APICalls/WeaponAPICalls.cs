using DnDSpellBot.Modules.Classes;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DnDSpellBot.Services.APICalls
{
    class WeaponAPICalls
    {
        private readonly Regex WeaponFind = new Regex(@"^((?:\w+\s?\-?){1,2})$");

        public async Task<Weapons> GetAllWeaponsAsync(HttpClient Client)
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

        public async Task<Weapons> WeaponSearchAsync(HttpClient Client, string weapon)
        {
            weapon = weapon.TrimEnd()
                           .TrimStart()
                           .ToLower();
            Match stringMatch = Regex.Match(weapon, WeaponFind.ToString());
            if (!stringMatch.Success) return null;

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
    }
}
