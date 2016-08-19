using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Entities.Common
{
    public class Snippet
    {
        [JsonProperty("publishedAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime PublishedAt { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("thumbnails", NullValueHandling = NullValueHandling.Ignore)]
        public Thumbnails Thumbnails { get; set; }

        [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Tags { get; set; }

        // only: video, playlists, playlistitem
        [JsonProperty("channelId", NullValueHandling = NullValueHandling.Ignore)]
        public string ChannelId { get; set; }

        // only: video, playlists, playlistitem
        [JsonProperty("channelTitle", NullValueHandling = NullValueHandling.Ignore)]
        public string ChannelTitle { get; set; }

        // only: video
        [JsonProperty("categoryId", NullValueHandling = NullValueHandling.Ignore)]
        public string CategoryId { get; set; }

        // only: video
        [JsonProperty("liveBroadcastContent", NullValueHandling = NullValueHandling.Ignore)]
        public string LiveBroadcastContent { get; set; }

        // only: playlistitem
        [JsonProperty("playlistId", NullValueHandling = NullValueHandling.Ignore)]
        public string PlaylistId { get; set; }

        // only: playlistitem
        [JsonProperty("position", NullValueHandling = NullValueHandling.Ignore)]
        public int Position { get; set; }

        // only: playlistitem
        [JsonProperty("resourceId", NullValueHandling = NullValueHandling.Ignore)]
        public Resource ResourceId { get; set; }
    }
}
