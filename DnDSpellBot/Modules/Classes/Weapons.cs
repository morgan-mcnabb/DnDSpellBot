using System.Text;
using Newtonsoft.Json;


namespace DnDSpellBot.Modules.Classes
{
    public partial class Weapons
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("results")]
        public WeaponsArray[] Results { get; set; }

        public string BuildWeapons()
        {
            StringBuilder strWeapon = new StringBuilder();
            strWeapon.Append("Weapons found:");
            strWeapon.Append("\n");
            for (int i = 0; i < Results.Length; i++)
            {
                WeaponsArray currentWeapon = Results[i];

                strWeapon.Append("\t\t" + currentWeapon.Name + ":");
                strWeapon.Append("\n");

                strWeapon.Append("Category: " + currentWeapon.Category);
                strWeapon.Append("\n");

                strWeapon.Append("Cost: " + currentWeapon.Cost);
                strWeapon.Append("\n");

                strWeapon.Append("Damage Dice: " + currentWeapon.DamageDice);
                strWeapon.Append("\n");

                strWeapon.Append("Damage Type: " + currentWeapon.DamageType);
                strWeapon.Append("\n");

                strWeapon.Append("Weight: " + currentWeapon.Weight);
                strWeapon.Append("\n");

                strWeapon.Append("Weapon Properties: ");
                strWeapon.Append("\n");

                if(currentWeapon.Properties != null)
                {
                    foreach (string value in currentWeapon.Properties)
                    {
                        strWeapon.Append("\t" + value);
                        strWeapon.Append("\n");
                    }
                }
                
            }

            return strWeapon.ToString();
        }

        
    }

    public partial class WeaponsArray
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("cost")]
        public string Cost { get; set; }

        [JsonProperty("damage_dice")]
        public string DamageDice { get; set; }

        [JsonProperty("damage_type")]
        public string DamageType { get; set; }

        [JsonProperty("weight")]
        public string Weight { get; set; }

        [JsonProperty("properties")]
        public string[] Properties { get; set; }
    }
}
