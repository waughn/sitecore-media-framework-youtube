using Sitecore.Data.Items;
using Sitecore.MediaFramework.Entities;
using Sitecore.MediaFramework.Synchronize;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities;
using Sitecore.SharedSource.MediaFramework.YouTube.Indexing.Entities;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Synchronize.Synchronizers
{
    public class TagSynchronizer : SynchronizerBase
    {
        public override Item UpdateItem(object entity, Item accountItem, Item item)
        {
            var tag = (Tag) entity;
            using (new EditContext(item))
            {
                item[FieldIDs.DisplayName] = tag.Name;
                item[Templates.Tag.Name] = tag.Name;
            }
            return item;
        }

        public override Item GetRootItem(object entity, Item accountItem)
        {
            return accountItem.Children["Tags"];
        }

        public override bool NeedUpdate(object entity, Item accountItem, MediaServiceSearchResult searchResult)
        {
            return false;
        }

        public override MediaServiceSearchResult GetSearchResult(object entity, Item accountItem)
        {
            var tag = (Tag) entity;
            return this.GetSearchResult<TagSearchResult>(Constants.IndexName, accountItem, i => i.TemplateId == Templates.Tag.TemplateID && i.Name == ItemUtil.ProposeValidItemName(tag.Name));
        }

        public override MediaServiceEntityData GetMediaData(object entity)
        {
            var tag = (Tag) entity;
            return new MediaServiceEntityData()
            {
                EntityId = tag.Name,
                EntityName = tag.Name,
                TemplateId = Templates.Tag.TemplateID
            };
        }
    }
}
