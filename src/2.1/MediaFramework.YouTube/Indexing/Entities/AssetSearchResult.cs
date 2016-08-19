using System;
using System.Collections.Generic;
using Sitecore.ContentSearch;
using Sitecore.Data;
using Sitecore.MediaFramework.Entities;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Indexing.Entities
{
    public class AssetSearchResult : MediaServiceSearchResult
    {
        [IndexField("kind")]
        public string Kind { get; set; }

        [IndexField("uniqueid")]
        public string UniqueID { get; set; }

        [IndexField("etag")]
        public string ETag { get; set; }

        [IndexField("publishedat")]
        public DateTime PublishedAt { get; set; }

        [IndexField("title")]
        public string Title { get; set; }

        [IndexField("description")]
        public string Description { get; set; }

        [IndexField("tagslist")]
        public List<ID> TagsList { get; set; }

        [IndexField("defaultthumbnail")]
        public string DefaultThumbnail { get; set; }

        [IndexField("mediumthumbnail")]
        public string MediumThumbnail { get; set; }

        [IndexField("highthumbnail")]
        public string HighThumbnail { get; set; }

        [IndexField("standardthumbnail")]
        public string StandardThumbnail { get; set; }

        [IndexField("maxresolutionthumbnail")]
        public string MaxResolutionThumbnail { get; set; }

        [IndexField("privacystatus")]
        public string PrivacyStatus { get; set; }
    }
}
