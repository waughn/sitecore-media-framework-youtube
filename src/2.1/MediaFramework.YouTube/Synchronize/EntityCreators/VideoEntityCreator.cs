using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities.Videos;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Synchronize.EntityCreators
{
    public class VideoEntityCreator : AssetEntityCreator<Video>
    {
        public override object CreateEntity(Item item)
        {
            var video = (Video)base.CreateEntity(item);

            video.ContentDetails = new ContentDetails();
            video.ContentDetails.Duration = item[Templates.Video.Duration];
            video.ContentDetails.Dimension = item[Templates.Video.Dimension];
            video.ContentDetails.Definition = item[Templates.Video.Definition];
            video.ContentDetails.Caption = item[Templates.Video.Caption];
            video.ContentDetails.LicensedContent = ((CheckboxField)item.Fields[Templates.Video.LicensedContent]).Checked;

            video.Statistics = new Statistics();
            video.Statistics.ViewCount = GetFieldInteger(item[Templates.Video.ViewCount]);
            video.Statistics.LikeCount = GetFieldInteger(item[Templates.Video.LikeCount]);
            video.Statistics.DislikeCount = GetFieldInteger(item[Templates.Video.DislikeCount]);
            video.Statistics.FavoriteCount = GetFieldInteger(item[Templates.Video.FavoriteCount]);
            video.Statistics.CommentCount = GetFieldInteger(item[Templates.Video.CommentCount]);

            return video;
        }

        public static int GetFieldInteger(string value)
        {
            int integer;
            int.TryParse(value, out integer);

            return integer;
        }
    }
}
