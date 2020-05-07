namespace DEBoarded
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class LeaderBoard
    {
        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("leaderboard_id")]
        public long LeaderboardId { get; set; }

        [JsonProperty("start")]
        public long Start { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("leaderboard")]
        public List<Leaderboard> Leaderboard { get; set; }
    }

    public partial class Leaderboard
    {
        [JsonProperty("profile_id")]
        public long ProfileId { get; set; }

        [JsonProperty("rank")]
        public long Rank { get; set; }

        [JsonProperty("rating")]
        public long Rating { get; set; }

        [JsonProperty("steam_id")]
        public string SteamId { get; set; }

        [JsonProperty("icon")]
        public object Icon { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("clan")]
        public object Clan { get; set; }

        [JsonProperty("country")]
        public object Country { get; set; }

        [JsonProperty("previous_rating")]
        public long PreviousRating { get; set; }

        [JsonProperty("highest_rating")]
        public long HighestRating { get; set; }

        [JsonProperty("streak")]
        public long Streak { get; set; }

        [JsonProperty("lowest_streak")]
        public long LowestStreak { get; set; }

        [JsonProperty("highest_streak")]
        public long HighestStreak { get; set; }

        [JsonProperty("games")]
        public long Games { get; set; }

        [JsonProperty("wins")]
        public long Wins { get; set; }

        [JsonProperty("losses")]
        public long Losses { get; set; }

        [JsonProperty("drops")]
        public long Drops { get; set; }

        [JsonProperty("last_match")]
        public long LastMatch { get; set; }

        [JsonProperty("last_match_time")]
        public long LastMatchTime { get; set; }
    }

    public partial class LeaderBoard
    {
        public static LeaderBoard FromJson(string json) => JsonConvert.DeserializeObject<LeaderBoard>(json, Converter2.Settings2);
    }

    public static class Serialize2
    {
        public static string ToJson(this LeaderBoard self) => JsonConvert.SerializeObject(self, Converter2.Settings2);
    }

    internal static class Converter2
    {
        public static readonly JsonSerializerSettings Settings2 = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
