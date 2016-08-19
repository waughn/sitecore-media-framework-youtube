using System.Collections.Generic;
using Sitecore.ContentSearch;
using Sitecore.Data;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Indexing.Entities
{
    public class PlaylistSearchResult : AssetSearchResult
    {
        [IndexField("videolist")]
        public List<ID> VideoList { get; set; }
    }
}
