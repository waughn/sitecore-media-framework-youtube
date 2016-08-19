using Sitecore.MediaFramework.Cleanup;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities;
using Sitecore.SharedSource.MediaFramework.YouTube.Indexing.Entities;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Cleanup
{
    public class PlaylistCleanupExecuter : CleanupExecuterBase<Asset, AssetSearchResult>
    {
        protected override string GetEntityId(Asset entity)
        {
            return entity.UniqueId;
        }

        protected override string GetSearchResultId(AssetSearchResult searchResult)
        {
            return searchResult.UniqueID;
        }
    }
}
