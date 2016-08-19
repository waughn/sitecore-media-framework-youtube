using Sitecore.Data.Items;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Synchronize.EntityCreators
{
    public class PlaylistEntityCreator : AssetEntityCreator<Video>
    {
        public override object CreateEntity(Item item)
        {
            var playlist = (Playlist)base.CreateEntity(item);
            return playlist;
        }
    }
}
