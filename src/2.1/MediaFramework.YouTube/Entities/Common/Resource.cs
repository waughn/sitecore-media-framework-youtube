using Newtonsoft.Json;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Entities.Common
{
    public class Resource
    {
        [JsonProperty("kind", NullValueHandling = NullValueHandling.Ignore)]
        public string Kind { get; set; }

        [JsonProperty("videoId", NullValueHandling = NullValueHandling.Ignore)]
        public string VideoId { get; set; }
    }
}
