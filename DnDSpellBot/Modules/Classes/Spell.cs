using Discord;
using Discord.Net;
using Discord.WebSocket;
using Discord.Commands;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using DnDSpellBot.Modules.Classes;
using Newtonsoft.Json;
using System.Collections;
using System.Reflection;

namespace DnDSpellBot.Modules.Classes
{
    class Spell
    {
        public int Count = 17;
        public string _id { get; set; }
        public string index { get; set; }
        public string name { get; set; }
        [JsonProperty(PropertyName = "desc")]
        public string[] description { get; set; } //
        [JsonProperty(PropertyName = "higher_level")]
        public string[] higherLevel { get; set; }
        public string range { get; set; }
        public string[] components { get; set; } //
        public string material { get; set; }
        public bool ritual { get; set; }
        public string duration { get; set; }
        public bool concentration { get; set; }
        public string casting_time { get; set; }
        public int level { get; set; }
        public School school { get; set; } //
        public Class[] classes { get; set; } //
        public Subclasses[] subclasses { get; set; } //
        public string url { get; set; }

        internal class Description
        {
            public string description { get; set; }
            public string higherLevel { get; set; }
        }

        internal class School
        {
            public string name { get; set; }
            public string url { get; set; }
        }
        internal class Class
        {
            public string name { get; set; }
            public string url { get; set; }
        }
        internal class Subclasses
        {
            public string name { get; set; }
            public string url { get; set; }
        }

    }
}
