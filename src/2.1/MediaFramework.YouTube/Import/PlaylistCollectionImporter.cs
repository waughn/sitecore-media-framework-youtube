using System;
using System.Collections.Generic;
using Sitecore.Data.Items;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Import
{
    public class PlaylistCollectionImporter : EntityCollectionImporter<Playlist>
    {
        protected override string RequestName
        {
            get
            {
                return "read_playlists";
            }
        }

        public override IEnumerable<object> GetData(Item accountItem)
        {
            base.IdParameterValue = string.Empty;
            base.MineParameterValue = false;
            base.NoParametersNeeded = false;

            if (accountItem.TemplateID == Templates.AccountPublic.TemplateID)
            {
                string ids = accountItem[Templates.AccountPublic.PlaylistIDs];
                if (!string.IsNullOrEmpty(ids))
                {
                    string[] idList = ids.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    base.IdParameterValue = string.Join(",", idList);
                }
            }
            else
            {
                base.MineParameterValue = true;
            }

            return base.GetData(accountItem);
        }
    }
}
