using Discord;
using Discord.Net;
using Discord.WebSocket;
using Discord.Commands;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace DnDSpellBot.Modules
{
    public class InfoModule : ModuleBase
    {
        [Command("help")]
        [Summary("Echoes a message")]
        public async Task HelloCommand()
        {
            StringBuilder helpText = new StringBuilder();
            helpText.Append("Thank you for using this Dungeons and Dragons bot!");
            helpText.Append("\n");
            helpText.Append("\n");

            helpText.Append("Each command starts with the ! character.");
            helpText.Append("\n");
            helpText.Append("\n");

            helpText.Append("List of commands and what they do: ");
            helpText.Append("\n");
            helpText.Append("\n");

            helpText.Append("!classes - returns a list of all 5E classes to choose from.");
            helpText.Append("\n");
            helpText.Append("\n");

            helpText.Append("!class [class name] - !class followed by the name of the class returns all details from a class.");
            helpText.Append("\n");
            helpText.Append("---NOTE--- due to discords 2000 character limit, these messages will come in multiple messages. Give them time to print properly.");
            helpText.Append("\n");
            helpText.Append("\n");

            helpText.Append("!magicitem [magic item name] - !magicitem followed by the name of the item will return details about the magic item.");
            helpText.Append("\n");
            helpText.Append("\n");

            helpText.Append("!monster [monster name] - !monster followed by the monster's name will return relevant information about the monster.");
            helpText.Append("\n");
            helpText.Append("\n");

            helpText.Append("!monstersbycr [Challenge Rating] - !monstersbycr followed by the Challenge Rating will return all monsters with that Challenge Rating.");
            helpText.Append("\n");
            helpText.Append("\n");

            helpText.Append("!spell [spell name] - !spell followed by the spell's name will return all relevant information for the spell.");
            helpText.Append("\n");
            helpText.Append("\n");

            helpText.Append("!spellsfor [class name] - !spellsfor followed by the name of the class will return all spells that class can learn, ordered by spell level.");
            helpText.Append("\n");
            helpText.Append("\n");

            helpText.Append("!weapon [weapon name] - !weapon followed by the weapon's name will return all relevant information regarding the weapon entered. ");
            helpText.Append("\n");
            helpText.Append("\n");

            helpText.Append("!allweapons - !allweapons will return all simple and martial weapons available in 5E.");

            string printData = helpText.ToString();

            await ReplyAsync(printData);
        }
    }
}
