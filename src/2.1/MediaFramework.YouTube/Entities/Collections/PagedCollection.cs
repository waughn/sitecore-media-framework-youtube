using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Entities.Collections
{
    public class PagedCollection<T>
    {
        [JsonProperty("items")]
        public List<T> Items { get; set; }

        [JsonProperty("nextPageToken")]
        public string NextPageToken { get; set; }
    }
}
