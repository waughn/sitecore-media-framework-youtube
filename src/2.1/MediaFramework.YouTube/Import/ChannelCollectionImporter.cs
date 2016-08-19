using System.Collections.Generic;
using Sitecore.Data.Items;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Import
{
    public class ChannelCollectionImporter : EntityCollectionImporter<Channel>
    {
        protected override string RequestName
        {
            get
            {
                return "read_channels";
            }
        }

        public override IEnumerable<object> GetData(Item accountItem)
        {
            base.IdParameterValue = string.Empty;
            base.MineParameterValue = false;
            base.NoParametersNeeded = accountItem.TemplateID == Templates.Account.TemplateID;

            return base.GetData(accountItem);
        }
    }
}
