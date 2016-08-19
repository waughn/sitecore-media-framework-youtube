using Newtonsoft.Json;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Entities.Videos
{
    public class Statistics
    {
        [JsonProperty("viewCount", NullValueHandling = NullValueHandling.Ignore)]
        public int ViewCount { get; set; }

        [JsonProperty("likeCount", NullValueHandling = NullValueHandling.Ignore)]
        public int LikeCount { get; set; }

        [JsonProperty("dislikeCount", NullValueHandling = NullValueHandling.Ignore)]
        public int DislikeCount { get; set; }

        [JsonProperty("favoriteCount", NullValueHandling = NullValueHandling.Ignore)]
        public int FavoriteCount { get; set; }

        [JsonProperty("commentCount", NullValueHandling = NullValueHandling.Ignore)]
        public int CommentCount { get; set; }
    }
}
