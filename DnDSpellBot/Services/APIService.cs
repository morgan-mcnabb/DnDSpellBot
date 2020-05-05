using DnDSpellBot.Modules.Classes;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using DnDSpellBot.Services.APICalls;

namespace DnDSpellBot.Services
{
    public class APIService
    {
        //establish http client pointed at 5e API
        private static readonly HttpClient Client = new HttpClient()
        {
            BaseAddress = new Uri("https://api.open5e.com/")
        };
        private const string Limit = "&limit=321";

        #region Methods

        public async Task<Weapons> AllWeaponsCall() => await new WeaponAPICalls().GetAllWeaponsAsync(Client);
        public async Task<Weapons> WeaponCall(string weapon) => await new WeaponAPICalls().WeaponSearchAsync(Client, weapon);
        public async Task<Spell> SpellCall(string search) => await new SpellAPICalls().SpellSearchAsync(Client, search);
        public async Task<AllSpells> SpellsByClass(string className) => await new SpellAPICalls().GetAllSpellsByClassAsync(Client, className, Limit);
        public async Task<Monster> MonsterCall(string monsterName) => await new MonsterAPICalls().MonsterSearchAsync(Client, monsterName);
        public async Task<MonstersByCr> MonstersByCRCall(string CR) => await new MonsterAPICalls().SearchMonstersByCR(Client, CR, Limit);
        public async Task<MagicItems> MagicItemCall(string magicItemName) => await new MagicItemCalls().MagicItemSearchAsync(Client, magicItemName);
        public async Task<Classes> ClassSearchCall(string className) => await new CharacterClassCalls().ClassSearchAsync(Client, className);
        public async Task<AllClasses> GetClassesCall() => await new CharacterClassCalls().AllClassesAsync(Client);

        #endregion
    }
}
