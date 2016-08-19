using System.Globalization;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.MediaFramework.Entities;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Synchronize.Synchronizers
{
    public class VideoSynchronizer : AssetSynchronizer
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

            var video = (Video)asset;

            if (video.ContentDetails != null)
            {
                item[Templates.Video.Duration] = video.ContentDetails.Duration;
                item[Templates.Video.Dimension] = video.ContentDetails.Dimension;
                item[Templates.Video.Definition] = video.ContentDetails.Definition;
                item[Templates.Video.Caption] = video.ContentDetails.Caption;
                ((CheckboxField)item.Fields[Templates.Video.LicensedContent]).Checked = video.ContentDetails.LicensedContent;
            }

            if (video.Statistics != null)
            {
                item[Templates.Video.ViewCount] = video.Statistics.ViewCount.ToString(CultureInfo.InvariantCulture);
                item[Templates.Video.LikeCount] = video.Statistics.LikeCount.ToString(CultureInfo.InvariantCulture);
                item[Templates.Video.DislikeCount] = video.Statistics.DislikeCount.ToString(CultureInfo.InvariantCulture);
                item[Templates.Video.FavoriteCount] = video.Statistics.FavoriteCount.ToString(CultureInfo.InvariantCulture);
                item[Templates.Video.CommentCount] = video.Statistics.CommentCount.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}
