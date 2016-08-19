using Sitecore.MediaFramework.Entities;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Synchronize.Synchronizers
{
    public class PlaylistSynchronizer : AssetSynchronizer
    {
        public override MediaServiceEntityData GetMediaData(object entity)
        {
            MediaServiceEntityData mediaData = base.GetMediaData(entity);
            mediaData.TemplateId = Templates.Playlist.TemplateID;
            return mediaData;
        }
    }
}
