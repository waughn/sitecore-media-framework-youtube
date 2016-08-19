using Sitecore.Data.Items;
using Sitecore.MediaFramework.Entities;
using Sitecore.MediaFramework.Synchronize.Fallback;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities;
using Sitecore.SharedSource.MediaFramework.YouTube.Indexing.Entities;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Synchronize.Fallback
{
    public class TagFallback : DatabaseFallbackBase
    {
        protected override Item GetItem(object entity, Item accountItem)
        {
            var tag = (Tag)entity;
            return accountItem.Axes.SelectSingleItem(string.Format("./Tags//*[@@templateid='{0}' and @@name='{1}']", Templates.Tag.TemplateID, ItemUtil.ProposeValidItemName(tag.Name)));
        }

        protected override MediaServiceSearchResult GetSearchResult(Item item)
        {
            var tagSearchResult = new TagSearchResult
            {
                Name = item[Templates.Tag.Name]
            };

            return tagSearchResult;
        }
    }
}
