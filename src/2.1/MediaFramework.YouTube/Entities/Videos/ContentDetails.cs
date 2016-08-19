using Newtonsoft.Json;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Entities.Videos
{
    public class ContentDetails
    {
        [JsonProperty("duration", NullValueHandling = NullValueHandling.Ignore)]
        public string Duration { get; set; }

        [JsonProperty("dimension", NullValueHandling = NullValueHandling.Ignore)]
        public string Dimension { get; set; }

        [JsonProperty("definition", NullValueHandling = NullValueHandling.Ignore)]
        public string Definition { get; set; }

        [JsonProperty("caption", NullValueHandling = NullValueHandling.Ignore)]
        public string Caption { get; set; }

        [JsonProperty("licensedContent", NullValueHandling = NullValueHandling.Ignore)]
        public bool LicensedContent { get; set; }
    }
}
