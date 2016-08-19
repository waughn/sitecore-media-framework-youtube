using System.Collections.Generic;
using System.Linq;
using Sitecore.Data.Items;
using Sitecore.MediaFramework.Import;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Import
{
    public class TagCollectionImporter : EntityCollectionImporter<Video>
    {
        protected override string RequestName
        {
            get
            {
                return "read_tags";
            }
        }

        public override IEnumerable<object> GetData(Item accountItem)
        {
            this.IdParameterValue = null;
            base.MineParameterValue = false;
            base.NoParametersNeeded = false;

            if (accountItem.TemplateID == Templates.Account.TemplateID)
            {
                var data = ImportManager.ImportList<SearchResult>("import_youtube_channel_videos", accountItem);
                if (data != null)
                    IdParameterValue = string.Join(",", data.Where(d => d.Id != null).Select(d => d.Id.VideoId));
            }

            return this.ReadAllTags(base.GetData(accountItem).OfType<Video>());
        }

        protected virtual IEnumerable<Tag> ReadAllTags(IEnumerable<Video> videos)
        {
            var tags = videos.Where(v => v.Snippet != null && v.Snippet.Tags != null).SelectMany(v => v.Snippet.Tags).ToList().Distinct();

            foreach (var tag in tags)
                yield return new Tag
                {
                    Name = tag
                };
        }
    }
}
