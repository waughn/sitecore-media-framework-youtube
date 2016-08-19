using Newtonsoft.Json;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Entities.Channels
{
    public class Statistics
    {
        [JsonProperty("viewCount", NullValueHandling = NullValueHandling.Ignore)]
        public int ViewCount { get; set; }

        [JsonProperty("commentCount", NullValueHandling = NullValueHandling.Ignore)]
        public int CommentCount { get; set; }

        [JsonProperty("subscriberCount", NullValueHandling = NullValueHandling.Ignore)]
        public int SubscriberCount { get; set; }

        [JsonProperty("hiddenSubscriberCount", NullValueHandling = NullValueHandling.Ignore)]
        public bool HiddenSubscriberCount { get; set; }

        [JsonProperty("videoCount", NullValueHandling = NullValueHandling.Ignore)]
        public int VideoCount { get; set; }
    }
}
