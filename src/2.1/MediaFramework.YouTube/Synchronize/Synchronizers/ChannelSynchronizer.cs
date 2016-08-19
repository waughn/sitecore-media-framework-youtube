using System.Globalization;
using Sitecore.Data.Items;
using Sitecore.MediaFramework.Entities;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Synchronize.Synchronizers
{
    public class ChannelSynchronizer : AssetSynchronizer
    {
        public override MediaServiceEntityData GetMediaData(object entity)
        {
            MediaServiceEntityData mediaData = base.GetMediaData(entity);
            mediaData.TemplateId = Templates.Channel.TemplateID;
            return mediaData;
        }

        protected override void UpdateProperties(Item item, Item accountItem, Asset asset)
        {
            base.UpdateProperties(item, accountItem, asset);

            var channel = (Channel)asset;

            if (channel.Statistics != null)
            {
                item[Templates.Channel.ViewCount] = channel.Statistics.ViewCount.ToString(CultureInfo.InvariantCulture);
                item[Templates.Channel.CommentCount] = channel.Statistics.CommentCount.ToString(CultureInfo.InvariantCulture);
                item[Templates.Channel.SubscriberCount] = channel.Statistics.SubscriberCount.ToString(CultureInfo.InvariantCulture);
                item[Templates.Channel.HiddenSubscriberCount] = channel.Statistics.HiddenSubscriberCount ? "1" : "0";
                item[Templates.Channel.VideoCount] = channel.Statistics.VideoCount.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}
