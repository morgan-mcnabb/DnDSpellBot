using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System.Text;

namespace DnDSpellBot.Modules.Classes
{
    public partial class AllSpells
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("results")]
        public Result[] Results { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("level_int")]
        public int LevelInt { get; set; }

        [JsonProperty("dnd_class")]
        public string DndClass { get; set; }
    }

    public enum CastingTime { The10Minutes, The1Action, The1BonusAction, The1Hour, The1Minute, The8Hours };

    public enum Circles { ArcticForest, Desert, Empty, Forest, Swamp, Underdark };

    public enum Components { V, VS, VSM };

    public enum Concentration { No, Yes };

    public enum DocumentSlug { WotcSrd };

    public enum DocumentTitle { SystemsReferenceDocument };
}
