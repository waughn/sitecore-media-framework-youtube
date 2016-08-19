using Newtonsoft.Json;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Entities.PlaylistItems
{
    public class ContentDetails
    {
        [JsonProperty("videoId", NullValueHandling = NullValueHandling.Ignore)]
        public string VideoId { get; set; }

        [JsonProperty("startAt", NullValueHandling = NullValueHandling.Ignore)]
        public string StartAt { get; set; }

        [JsonProperty("endAt", NullValueHandling = NullValueHandling.Ignore)]
        public string EndAt { get; set; }

        [JsonProperty("note", NullValueHandling = NullValueHandling.Ignore)]
        public string Note { get; set; }
    }
}
