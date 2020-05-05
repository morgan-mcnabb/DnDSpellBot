using DnDSpellBot.Modules.Classes;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DnDSpellBot.Services.APICalls
{
    class MagicItemCalls
    {
        private readonly Regex MagicItemFind = new Regex(@"^((?:\w+\s?\-?){1,5})$");

        public async Task<MagicItems> MagicItemSearchAsync(HttpClient Client, string magicItemName)
        {
            magicItemName = magicItemName.TrimEnd()
                                         .TrimStart()
                                         .ToLower();
            Match stringMatch = Regex.Match(magicItemName, MagicItemFind.ToString());
            if (!stringMatch.Success) return null;

            try
            {
                string path = "magicitems/?search=" + magicItemName;

                var response = await Client.GetAsync(path);
                if (!response.IsSuccessStatusCode) return null;


                var responseData = response.Content.ReadAsStringAsync().Result;


                //turn JSON object into meaningful data
                var magicItem = JsonConvert.DeserializeObject<MagicItems>(responseData);

                return magicItem;
            }
            catch
            {
                return null;
            }

        }
    }
}
