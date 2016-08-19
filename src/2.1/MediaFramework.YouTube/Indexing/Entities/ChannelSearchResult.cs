using System.Collections.Generic;
using Sitecore.ContentSearch;
using Sitecore.Data;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Indexing.Entities
{
    public class ChannelSearchResult : AssetSearchResult
    {
        [IndexField("playlistlist")]
        public List<ID> PlaylistList { get; set; }

        [IndexField("videolist")]
        public List<ID> VideoList { get; set; }

        [IndexField("viewcount")]
        public int ViewCount { get; set; }

        [IndexField("commentcount")]
        public int CommentCount { get; set; }

        [IndexField("subscribercount")]
        public int SubscriberCount { get; set; }

        [IndexField("hiddensubscribercount")]
        public int HiddenSubscriberCount { get; set; }

        [IndexField("videocount")]
        public int VideoCount { get; set; }
    }
}
