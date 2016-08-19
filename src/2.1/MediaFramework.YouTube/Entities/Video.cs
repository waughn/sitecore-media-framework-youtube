using Newtonsoft.Json;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities.Videos;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Entities
{
    public class Video : Asset
    {
        [JsonProperty("contentDetails", NullValueHandling = NullValueHandling.Ignore)]
        public ContentDetails ContentDetails { get; set; }

        [JsonProperty("statistics", NullValueHandling = NullValueHandling.Ignore)]
        public Statistics Statistics { get; set; }
    }
}
