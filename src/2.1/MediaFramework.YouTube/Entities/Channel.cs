using Newtonsoft.Json;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities.Channels;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Entities
{
    public class Channel : Asset
    {
        [JsonProperty("statistics", NullValueHandling = NullValueHandling.Ignore)]
        public Statistics Statistics { get; set; }
    }
}
