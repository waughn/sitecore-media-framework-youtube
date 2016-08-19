using Sitecore.Data.Items;
using Sitecore.MediaFramework.Entities;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities;
using Sitecore.SharedSource.MediaFramework.YouTube.Indexing.Entities;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Synchronize.Fallback
{
    public class VideoFallback : AssetFallback<VideoSearchResult>
    {
        protected override Item GetItem(object entity, Item accountItem)
        {
            var video = (Video)entity;
            return accountItem.Axes.SelectSingleItem(string.Format("./MediaContent//*[@@templateid='{0}' and @uniqueid='{1}']", Templates.Video.TemplateID, video.UniqueId));
        }

        protected override MediaServiceSearchResult GetSearchResult(Item item)
        {
            VideoSearchResult searchResult = (VideoSearchResult)base.GetSearchResult(item);

            return searchResult;
        }
    }
}
