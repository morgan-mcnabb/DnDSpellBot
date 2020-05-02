using System.Linq;
using System.Text;
using Discord.Commands;
using DnDSpellBot.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DnDSpellBot.Modules
{
    public class SpellsByClassModule : ModuleBase
    {
        //validates the class string entered by user
        private readonly Regex validate = new Regex(@"^[A-Za-z]+$");

        [Command("spellsfor")]
        public async Task GetSpellsByClass([Remainder]string classSearch)
        {
            //validate string and turn into meaningful data
            classSearch = classSearch.Trim(' ').ToLower();
            classSearch = Regex.Replace(classSearch, @"\s+", string.Empty);
            if(!Regex.Match(classSearch, validate.ToString()).Success)
            {
                await ReplyAsync("Enter exactly one word with no whitespace, please.");
                return;
            }

            //establish connection to API and get spells
            APIService api = new APIService();
            var allSpells = await api.GetAllSpells();
            if (allSpells.Count == 0) await ReplyAsync(classSearch + " cannot cast spells. ");

            //dictionary for printing spells by class data
            Dictionary<string, int> classSpells = new Dictionary<string, int>();

            classSearch = char.ToUpper(classSearch[0]) + classSearch.Substring(1);

            //add spells by class data to dictionary
            for(int i = 0; i < allSpells.Count; i++)
            {
                for(int j = 0; j < allSpells[i].Results.Length; j++)
                {
                    if (allSpells[i].Results[j].DndClass.Contains(classSearch)) classSpells.Add(allSpells[i].Results[j].Name, allSpells[i].Results[j].LevelInt);
                }
            }

            if (classSpells.Count() < 1)
            {
                await ReplyAsync("This class cannot cast spells. ");
                return;
            }

            //order by spell level
            classSpells = classSpells.OrderBy(x => x.Value)
                          .ToDictionary(pair => pair.Key, pair => pair.Value);

            //build string to print
            string printData = BuildClassSpells(classSpells, classSearch);

            //discord bot can't print 2000+ characters per reply
            if (printData.Length >= 2000)
            {
                string firsthalf = printData.Substring(0, printData.Length / 2);
                string secondhalf = printData.Substring(printData.Length / 2);

                await ReplyAsync(firsthalf);
                await ReplyAsync(secondhalf);
            }

            else await ReplyAsync(printData);
        }

        //builds string for spells by class data
        private string BuildClassSpells(Dictionary<string, int> classSpells, string className)
        {
            //builder for return string
            StringBuilder strClassSpells = new StringBuilder();

            //initialize level displays to false, should only print once
            bool[] levelDisplays = new bool[10];
            for (int i = 0; i < levelDisplays.Length; i++) levelDisplays[i] = false;

            //build the string dynamically
            foreach(KeyValuePair<string, int> data in classSpells)
            {
                if(!levelDisplays[data.Value])
                {
                    strClassSpells.Append("\n" + 
                        className);

                    if (data.Value == 0) strClassSpells.Append(" Cantrips: " + "\n");
                    else strClassSpells.Append(" Level " + data.Value + " Spells: " + "\n");

                    levelDisplays[data.Value] = true;
                }
                strClassSpells.Append(data.Key);
                strClassSpells.Append("\n");
            }

            return strClassSpells.ToString();
        }
    }
}
