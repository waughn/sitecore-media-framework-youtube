using Sitecore.ContentSearch;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Indexing.Entities
{
    public class VideoSearchResult : AssetSearchResult
    {
        [IndexField("duration")]
        public string Duration { get; set; }

        [IndexField("dimension")]
        public string Dimension { get; set; }

        [IndexField("definition")]
        public string Definition { get; set; }

        [IndexField("caption")]
        public string Caption { get; set; }

        [IndexField("licensedcontent")]
        public bool LicensedContent { get; set; }

        [IndexField("viewcount")]
        public int ViewCount { get; set; }

        [IndexField("likecount")]
        public int LikeCount { get; set; }

        [IndexField("dislikecount")]
        public int DislikeCount { get; set; }

        [IndexField("favoritecount")]
        public int FavoriteCount { get; set; }

        [IndexField("commentcount")]
        public int CommentCount { get; set; }
    }
}