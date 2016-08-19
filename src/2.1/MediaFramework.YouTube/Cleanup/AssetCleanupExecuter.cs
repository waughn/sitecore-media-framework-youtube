using Sitecore.SharedSource.MediaFramework.YouTube.Entities;
using Sitecore.SharedSource.MediaFramework.YouTube.Indexing.Entities;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Cleanup
{
    public class AssetCleanupExecuter : ExtendedCleanupExecuterBase<Asset, AssetSearchResult>
    {
        protected override string GetEntityId(Asset entity)
        {
            if (entity.Snippet != null && entity.Snippet.ResourceId != null && !string.IsNullOrEmpty(entity.Snippet.ResourceId.VideoId))
            {
                return entity.Snippet.ResourceId.VideoId;
            }

            return entity.UniqueId;
        }

        protected override string GetSearchResultId(AssetSearchResult searchResult)
        {
            return searchResult.UniqueID;
        }
    }
}
