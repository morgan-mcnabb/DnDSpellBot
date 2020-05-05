using DnDSpellBot.Modules.Classes;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace DnDSpellBot.Services.APICalls
{
    class CharacterClassCalls
    {
        private readonly Regex ClassFind = new Regex(@"^((?:\w+\s?\-?){1})$");

        public async Task<Classes> ClassSearchAsync(HttpClient Client, string className)
        {
            className = className.Trim(' ').ToLower();
            className = Regex.Replace(className, @"\s+", string.Empty);
            Match stringMatch = Regex.Match(className, ClassFind.ToString());
            if (!stringMatch.Success) return null;

            try
            {
                var response = await Client.GetAsync("classes/" + className);

                if (!response.IsSuccessStatusCode) return null;

                var responseData = response.Content.ReadAsStringAsync().Result;

                var classData = JsonConvert.DeserializeObject<Classes>(responseData);

                return classData;
            }
            catch
            {
                return null;
            }
        }
    }
}
