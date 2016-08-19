using Newtonsoft.Json;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Entities.Common
{
    public class Thumbnail
    {
        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }

        [JsonProperty("width", NullValueHandling = NullValueHandling.Ignore)]
        public string Width { get; set; }

        [JsonProperty("height", NullValueHandling = NullValueHandling.Ignore)]
        public string Height { get; set; }
    }
}
