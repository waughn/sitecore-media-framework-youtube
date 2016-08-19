using Sitecore.Data.Items;
using Sitecore.MediaFramework.Entities;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Synchronize.Synchronizers
{
    public class PlaylistItemSynchronizer : AssetSynchronizer
    {
        public override MediaServiceEntityData GetMediaData(object entity)
        {
            MediaServiceEntityData mediaData = base.GetMediaData(entity);
            mediaData.TemplateId = Templates.Video.TemplateID;
            return mediaData;
        }

        protected override void UpdateProperties(Item item, Item accountItem, Asset asset)
        {
            base.UpdateProperties(item, accountItem, asset);

            var playlistItem = (PlaylistItem)asset;

            if (playlistItem.Snippet != null && playlistItem.Snippet.ResourceId != null)
            {
                item[Templates.MediaElement.Kind] = playlistItem.Snippet.ResourceId.Kind;
                item[Templates.MediaElement.UniqueID] = playlistItem.Snippet.ResourceId.VideoId;
            }
        }
    }
}
