using Sitecore.Data.Items;
using Sitecore.MediaFramework.Entities;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities;
using Sitecore.SharedSource.MediaFramework.YouTube.Indexing.Entities;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Synchronize.Fallback
{
    public class PlaylistFallback : AssetFallback<PlaylistSearchResult>
    {
        protected override Item GetItem(object entity, Item accountItem)
        {
            var playlist = (Playlist)entity;
            return accountItem.Axes.SelectSingleItem(string.Format("./MediaContent//*[@@templateid='{0}' and @uniqueid='{1}']", Templates.Playlist.TemplateID, playlist.UniqueId));
        }

        protected override MediaServiceSearchResult GetSearchResult(Item item)
        {
            PlaylistSearchResult searchResult = (PlaylistSearchResult)base.GetSearchResult(item);

            return searchResult;
        }
    }
}
