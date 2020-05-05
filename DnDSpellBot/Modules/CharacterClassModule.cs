using Discord.Commands;
using DnDSpellBot.Services;
using System.Threading.Tasks;
using System;
namespace DnDSpellBot.Modules
{
    public class CharacterClassModule : ModuleBase
    {
        [Command("class")]
        public async Task GetClassData([Remainder]string className)
        {
            APIService api = new APIService();

            var classData = await api.ClassSearchCall(className);

            if (classData == null)
            {
                await ReplyAsync("Sorry, I couldn't find a class with that name!");
                return;
            }

            var printData = classData.BuildClassString();

            double loopNumNotInt = printData.Length / 2000.0;

            //convert loopnum to int, need ceiling
            int loopNum = (int)Math.Ceiling(loopNumNotInt);

            //string offset
            int offset = printData.Length / loopNum;

            //max reply length is 2000 characters, all class data have 7000+ characters
            for (int i = 0; i < loopNum; i++)
            {
                await ReplyAsync(printData.Substring(i * offset, offset));
            }
        }

        [Command("allclasses")]
        public async Task GetClasses()
        {
            APIService api = new APIService();

            var classes = await api.GetClassesCall();

            string printData = classes.BuildClasses();

            await ReplyAsync(printData);
        }
    }
}
