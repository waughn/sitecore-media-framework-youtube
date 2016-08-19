using Sitecore.MediaFramework;
using Sitecore.MediaFramework.Analytics;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Analytics
{
    public class PlaylistEventTrigger : EventTrigger
    {
        public override void InitEvents()
        {
            this.AddEvent(Templates.Playlist.TemplateID, PlaybackEvents.PlaybackStarted.ToString(), "YouTube video is started.");
            this.AddEvent(Templates.Playlist.TemplateID, PlaybackEvents.PlaybackCompleted.ToString(), "YouTube video is completed.");
            this.AddEvent(Templates.Playlist.TemplateID, PlaybackEvents.PlaybackChanged.ToString(), "YouTube video progress is changed.");
            this.AddEvent(Templates.Playlist.TemplateID, PlaybackEvents.PlaybackError.ToString(), "YouTube video playback error.");
        }
    }
}
