using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Data.Items;
using Sitecore.MediaFramework.Import;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Import
{
    public class VideoCollectionImporter : EntityCollectionImporter<Video>
    {
        protected override string RequestName
        {
            get
            {
                return "read_videos";
            }
        }

        public override IEnumerable<object> GetData(Item accountItem)
        {
            base.IdParameterValue = string.Empty;
            base.MineParameterValue = false;
            base.NoParametersNeeded = false;

            if (accountItem.TemplateID == Templates.AccountPublic.TemplateID)
            {
                string ids = accountItem[Templates.AccountPublic.VideoIDs];
                if (!string.IsNullOrEmpty(ids))
                {
                    string[] idList = ids.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    base.IdParameterValue = string.Join(",", idList);
                }
            }
            else
            {
                var data = ImportManager.ImportList<SearchResult>("import_youtube_channel_videos", accountItem);
                if (data != null)
                    base.IdParameterValue = string.Join(",", data.Where(d => d.Id != null).Select(d => d.Id.VideoId));
            }

            return base.GetData(accountItem);
        }
    }
}
