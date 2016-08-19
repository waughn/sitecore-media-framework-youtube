using Sitecore.Data.Items;
using Sitecore.MediaFramework.Entities;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities;
using Sitecore.SharedSource.MediaFramework.YouTube.Indexing.Entities;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Synchronize.Fallback
{
    public class PlaylistItemFallback : AssetFallback<PlaylistItemSearchResult>
    {
        protected override Item GetItem(object entity, Item accountItem)
        {
            var playlistItem = (PlaylistItem)entity;
            return accountItem.Axes.SelectSingleItem(string.Format("./MediaContent//*[@@templateid='{0}' and @uniqueid='{1}']", Templates.Video.TemplateID, playlistItem.Snippet.ResourceId.VideoId));
        }

        protected override MediaServiceSearchResult GetSearchResult(Item item)
        {
            var searchResult = (PlaylistItemSearchResult)base.GetSearchResult(item);

            return searchResult;
        }
    }
}
