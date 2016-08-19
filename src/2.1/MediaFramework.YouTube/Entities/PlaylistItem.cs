using Newtonsoft.Json;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities.PlaylistItems;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Entities
{
    public class PlaylistItem : Asset
    {
        [JsonProperty("contentDetails", NullValueHandling = NullValueHandling.Ignore)]
        public ContentDetails ContentDetails { get; set; }
    }
}
