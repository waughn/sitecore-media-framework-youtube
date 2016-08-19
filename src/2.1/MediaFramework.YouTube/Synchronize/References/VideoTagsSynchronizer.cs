using System.Collections.Generic;
using System.Linq;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Integration.Common.Utils;
using Sitecore.MediaFramework;
using Sitecore.MediaFramework.Synchronize;
using Sitecore.MediaFramework.Synchronize.References;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities;
using Sitecore.SharedSource.MediaFramework.YouTube.Indexing.Entities;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Synchronize.References
{
    public class VideoTagsSynchronizer : IdReferenceSynchronizer<Video>
    {
        protected override List<ID> GetReference(Video entity, Item accountItem)
        {
            if (entity.Snippet == null || entity.Snippet.Tags == null || entity.Snippet.Tags.Count == 0)
                return new List<ID>();

            var ancestorFilter = ContentSearchUtil.GetAncestorFilter<TagSearchResult>(accountItem, Templates.Tag.TemplateID);
            var expression = entity.Snippet.Tags.Aggregate(PredicateBuilder.False<TagSearchResult>(), (current, tmp) => current.Or(i => i.TagName == tmp));

            List<TagSearchResult> all = ContentSearchUtil.FindAll(Constants.IndexName, ancestorFilter.And(expression));

            if (all.Count < entity.Snippet.Tags.Count)
            {
                IItemSynchronizer itemSynchronizer = MediaFrameworkContext.GetItemSynchronizer(typeof(Tag));
                if (itemSynchronizer != null)
                {
                    foreach (string tag in entity.Snippet.Tags)
                    {
                        string tagName = tag;
                        if (all.All(i => i.Name != tagName))
                        {
                            var tagSearchResult = itemSynchronizer.Fallback(new Tag { Name = tagName }, accountItem) as TagSearchResult;

                            if (tagSearchResult != null)
                                all.Add(tagSearchResult);
                        }
                    }
                }
            }

            return all.Select(i => i.ItemId).ToList();
        }
    }
}
