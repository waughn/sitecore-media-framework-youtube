using Sitecore.Data.Items;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities.Channels;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Synchronize.EntityCreators
{
    public class ChannelEntityCreator : AssetEntityCreator<Channel>
    {
        public override object CreateEntity(Item item)
        {
            var channel = (Channel)base.CreateEntity(item);

            channel.Statistics = new Statistics();
            channel.Statistics.ViewCount = GetFieldInteger(item[Templates.Channel.ViewCount]);
            channel.Statistics.CommentCount = GetFieldInteger(item[Templates.Channel.CommentCount]);
            channel.Statistics.SubscriberCount = GetFieldInteger(item[Templates.Channel.SubscriberCount]);
            channel.Statistics.HiddenSubscriberCount = GetFieldBoolean(item[Templates.Channel.HiddenSubscriberCount]);
            channel.Statistics.VideoCount = GetFieldInteger(item[Templates.Channel.VideoCount]);

            return channel;
        }

        public static int GetFieldInteger(string value)
        {
            int integer;
            int.TryParse(value, out integer);

            return integer;
        }

        public static bool GetFieldBoolean(string value)
        {
            return value == "1";
        }
    }
}
