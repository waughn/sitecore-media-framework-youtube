using Newtonsoft.Json;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities.Common;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Entities
{
    public abstract class Asset
    {
        [JsonProperty("kind", NullValueHandling = NullValueHandling.Ignore)]
        public string Kind { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string UniqueId { get; set; }

        [JsonProperty("etag", NullValueHandling = NullValueHandling.Ignore)]
        public string ETag { get; set; }

        [JsonProperty("snippet", NullValueHandling = NullValueHandling.Ignore)]
        public Snippet Snippet { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public Status Status { get; set; }

        public override string ToString()
        {
            return string.Format("(type: {0}, id: {1}, title: {2})", this.GetType().Name, this.UniqueId, this.Snippet != null ? this.Snippet.Title : string.Empty);
        }
    }
}
