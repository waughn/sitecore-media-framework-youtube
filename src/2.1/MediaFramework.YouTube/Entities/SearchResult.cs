using Newtonsoft.Json;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities.Common;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Entities
{
    public class SearchResult
    {
        [JsonProperty("kind", NullValueHandling = NullValueHandling.Ignore)]
        public string Kind { get; set; }

        [JsonProperty("etag", NullValueHandling = NullValueHandling.Ignore)]
        public string ETag { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public Resource Id { get; set; }

        public override string ToString()
        {
            return string.Format("(type: {0}, id: {1})", this.GetType().Name, this.Id);
        }
    }
}
