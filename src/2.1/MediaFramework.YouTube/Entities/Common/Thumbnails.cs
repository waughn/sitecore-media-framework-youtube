using Newtonsoft.Json;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Entities.Common
{
    public class Thumbnails
    {
        [JsonProperty("default", NullValueHandling = NullValueHandling.Ignore)]
        public Thumbnail Default { get; set; }

        [JsonProperty("medium", NullValueHandling = NullValueHandling.Ignore)]
        public Thumbnail Medium { get; set; }

        [JsonProperty("high", NullValueHandling = NullValueHandling.Ignore)]
        public Thumbnail High { get; set; }

        [JsonProperty("standard", NullValueHandling = NullValueHandling.Ignore)]
        public Thumbnail Standard { get; set; }

        [JsonProperty("maxres", NullValueHandling = NullValueHandling.Ignore)]
        public Thumbnail MaxResolution { get; set; }
    }
}
