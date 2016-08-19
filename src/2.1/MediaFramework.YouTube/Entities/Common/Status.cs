using Newtonsoft.Json;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Entities.Common
{
    public class Status
    {
        [JsonProperty("privacyStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string PrivacyStatus { get; set; }

        // video
        [JsonProperty("uploadStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string UploadStatus { get; set; }

        // video
        [JsonProperty("failureReason", NullValueHandling = NullValueHandling.Ignore)]
        public string FailureReason { get; set; }

        // video
        [JsonProperty("rejectionReason", NullValueHandling = NullValueHandling.Ignore)]
        public string RejectionReason { get; set; }

        // video
        [JsonProperty("license", NullValueHandling = NullValueHandling.Ignore)]
        public string License { get; set; }

        // video
        [JsonProperty("embeddable", NullValueHandling = NullValueHandling.Ignore)]
        public bool Embeddable { get; set; }

        // video
        [JsonProperty("publicStatsViewable", NullValueHandling = NullValueHandling.Ignore)]
        public bool PublicStatsViewable { get; set; }
    }
}
