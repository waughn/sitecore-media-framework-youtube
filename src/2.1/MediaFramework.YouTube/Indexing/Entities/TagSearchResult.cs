using Sitecore.ContentSearch;
using Sitecore.MediaFramework.Entities;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Indexing.Entities
{
    public class TagSearchResult : MediaServiceSearchResult
    {
        [IndexField("name")]
        public string TagName { get; set; }
    }
}
