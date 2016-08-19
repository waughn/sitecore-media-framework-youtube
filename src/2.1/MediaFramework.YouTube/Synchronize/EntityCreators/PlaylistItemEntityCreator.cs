using Sitecore.Data.Items;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Synchronize.EntityCreators
{
    public class PlaylistItemEntityCreator : AssetEntityCreator<Video>
    {
        public override object CreateEntity(Item item)
        {
            var playlistItem = (PlaylistItem)base.CreateEntity(item);

            return playlistItem;
        }
    }
}
