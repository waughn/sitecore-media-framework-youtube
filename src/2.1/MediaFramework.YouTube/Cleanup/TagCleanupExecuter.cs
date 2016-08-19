using Sitecore.MediaFramework.Cleanup;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities;
using Sitecore.SharedSource.MediaFramework.YouTube.Indexing.Entities;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Cleanup
{
    public class TagCleanupExecuter : CleanupExecuterBase<Tag, TagSearchResult>
    {
        protected override string GetEntityId(Tag entity)
        {
            return entity.Name;
        }

        protected override string GetSearchResultId(TagSearchResult searchResult)
        {
            return searchResult.TagName;
        }
    }
}
