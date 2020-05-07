namespace DEBoarded
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class LobBrowser
    {
        [JsonProperty("match_id", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? MatchId { get; set; }

        [JsonProperty("lobby_id", NullValueHandling = NullValueHandling.Ignore)]
        public string LobbyId { get; set; }

        [JsonProperty("match_uuid", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? MatchUuid { get; set; }

        [JsonProperty("version")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Version { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("num_players", NullValueHandling = NullValueHandling.Ignore)]
        public long? NumPlayers { get; set; }

        [JsonProperty("num_slots", NullValueHandling = NullValueHandling.Ignore)]
        public long? NumSlots { get; set; }

        [JsonProperty("average_rating")]
        public long? AverageRating { get; set; }

        [JsonProperty("cheats", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Cheats { get; set; }

        [JsonProperty("full_tech_tree", NullValueHandling = NullValueHandling.Ignore)]
        public bool? FullTechTree { get; set; }

        [JsonProperty("ending_age", NullValueHandling = NullValueHandling.Ignore)]
        public long? EndingAge { get; set; }

        [JsonProperty("expansion")]
        public object Expansion { get; set; }

        [JsonProperty("game_type", NullValueHandling = NullValueHandling.Ignore)]
        public long? GameType { get; set; }

        [JsonProperty("has_custom_content")]
        public object HasCustomContent { get; set; }

        [JsonProperty("has_password", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HasPassword { get; set; }

        [JsonProperty("lock_speed", NullValueHandling = NullValueHandling.Ignore)]
        public bool? LockSpeed { get; set; }

        [JsonProperty("lock_teams", NullValueHandling = NullValueHandling.Ignore)]
        public bool? LockTeams { get; set; }

        [JsonProperty("map_size", NullValueHandling = NullValueHandling.Ignore)]
        public long? MapSize { get; set; }

        [JsonProperty("map_type", NullValueHandling = NullValueHandling.Ignore)]
        public long? MapType { get; set; }

        [JsonProperty("pop", NullValueHandling = NullValueHandling.Ignore)]
        public long? Pop { get; set; }

        [JsonProperty("ranked")]
        public object Ranked { get; set; }

        [JsonProperty("leaderboard_id")]
        public object LeaderboardId { get; set; }

        [JsonProperty("rating_type", NullValueHandling = NullValueHandling.Ignore)]
        public long? RatingType { get; set; }

        [JsonProperty("resources", NullValueHandling = NullValueHandling.Ignore)]
        public long? Resources { get; set; }

        [JsonProperty("rms")]
        public string Rms { get; set; }

        [JsonProperty("scenario")]
        public string Scenario { get; set; }

        //[JsonProperty("server", NullValueHandling = NullValueHandling.Ignore)]
        //public Server? Server { get; set; }

        [JsonProperty("shared_exploration", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SharedExploration { get; set; }

        [JsonProperty("speed", NullValueHandling = NullValueHandling.Ignore)]
        public long? Speed { get; set; }

        [JsonProperty("starting_age", NullValueHandling = NullValueHandling.Ignore)]
        public long? StartingAge { get; set; }

        [JsonProperty("team_together", NullValueHandling = NullValueHandling.Ignore)]
        public bool? TeamTogether { get; set; }

        [JsonProperty("team_positions", NullValueHandling = NullValueHandling.Ignore)]
        public bool? TeamPositions { get; set; }

        [JsonProperty("treaty_length", NullValueHandling = NullValueHandling.Ignore)]
        public long? TreatyLength { get; set; }

        [JsonProperty("turbo", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Turbo { get; set; }

        [JsonProperty("victory", NullValueHandling = NullValueHandling.Ignore)]
        public long? Victory { get; set; }

        [JsonProperty("victory_time", NullValueHandling = NullValueHandling.Ignore)]
        public long? VictoryTime { get; set; }

        [JsonProperty("visibility", NullValueHandling = NullValueHandling.Ignore)]
        public long? Visibility { get; set; }

        [JsonProperty("opened", NullValueHandling = NullValueHandling.Ignore)]
        public long? Opened { get; set; }

        [JsonProperty("started")]
        public object Started { get; set; }

        [JsonProperty("finished")]
        public object Finished { get; set; }

        [JsonProperty("players", NullValueHandling = NullValueHandling.Ignore)]
        public Player[] Players { get; set; }
    }

    public partial class Player
    {
        [JsonProperty("profile_id")]
        public long? ProfileId { get; set; }

        [JsonProperty("steam_id")]
        public string SteamId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("clan")]
        public string Clan { get; set; }

        [JsonProperty("country")]
        public object Country { get; set; }

        [JsonProperty("slot", NullValueHandling = NullValueHandling.Ignore)]
        public long? Slot { get; set; }

        [JsonProperty("slot_type", NullValueHandling = NullValueHandling.Ignore)]
        public long? SlotType { get; set; }

        [JsonProperty("rating")]
        public long? Rating { get; set; }

        [JsonProperty("games")]
        public long? Games { get; set; }

        [JsonProperty("wins")]
        public long? Wins { get; set; }

        [JsonProperty("streak")]
        public long? Streak { get; set; }

        [JsonProperty("drops")]
        public long? Drops { get; set; }

        [JsonProperty("color")]
        public object Color { get; set; }

        [JsonProperty("team")]
        public object Team { get; set; }

        [JsonProperty("civ")]
        public object Civ { get; set; }
    }

    //public enum Server { Australiasoutheast, Brazilsouth, Eastus, Southeastasia, Ukwest, Westeurope, Westindia, Westus2 };

    public partial class LobBrowser
    {
        
        public static LobBrowser[] FromJson(string json) => JsonConvert.DeserializeObject<LobBrowser[]>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this LobBrowser[] self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                //ServerConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

 
}
