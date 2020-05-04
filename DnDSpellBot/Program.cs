using System;

namespace DnDSpellBot
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                new SpellBot().MainAsync().GetAwaiter().GetResult();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
