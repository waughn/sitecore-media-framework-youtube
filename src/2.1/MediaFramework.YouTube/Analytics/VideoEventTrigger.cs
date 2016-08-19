using Sitecore.MediaFramework;
using Sitecore.MediaFramework.Analytics;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Analytics
{
    public class VideoEventTrigger : EventTrigger
    {
        public override void InitEvents()
        {
            this.AddEvent(Templates.Video.TemplateID, PlaybackEvents.PlaybackStarted.ToString(), "YouTube video is started.");
            this.AddEvent(Templates.Video.TemplateID, PlaybackEvents.PlaybackCompleted.ToString(), "YouTube video is completed.");
            this.AddEvent(Templates.Video.TemplateID, PlaybackEvents.PlaybackChanged.ToString(), "YouTube video progress is changed.");
            this.AddEvent(Templates.Video.TemplateID, PlaybackEvents.PlaybackError.ToString(), "YouTube video playback error.");
        }
    }
}
