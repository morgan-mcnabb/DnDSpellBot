using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DnDSpellBot.Modules.Classes
{
    class Spell : IEnumerable<Spell>
    {
        public int Count = 17;
        public string id { get; set; }
        public string index { get; set; }
        public string name { get; set; }
        public string description { get; set; } 
        public string higherLevel { get; set; }
        public string range { get; set; }
        public string[] components { get; set; } 
        public string material { get; set; }
        public bool ritual { get; set; }
        public string duration { get; set; }
        public string concentration { get; set; }
        public string castingTime { get; set; }
        public int level { get; set; }
        public School school { get; set; } 
        public Class[] classes { get; set; } 
        public Subclasses[] subclasses { get; set; } 
        public string url { get; set; }

        public IEnumerator<Spell> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
