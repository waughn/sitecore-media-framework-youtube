using Sitecore.Data.Items;
using Sitecore.MediaFramework.Entities;
using Sitecore.MediaFramework.Export;
using Sitecore.MediaFramework.Synchronize;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities;
using Sitecore.SharedSource.MediaFramework.YouTube.Indexing.Entities;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Synchronize.Synchronizers
{
    public abstract class AssetSynchronizer : SynchronizerBase
    {
        public override MediaServiceEntityData GetMediaData(object entity)
        {
            Asset asset = (Asset)entity;
            return new MediaServiceEntityData { EntityId = asset.UniqueId, EntityName = asset.Snippet.Title };
        }

        public override Item GetRootItem(object entity, Item accountItem)
        {
            return accountItem.Children["MediaContent"];
        }

        public override MediaServiceSearchResult GetSearchResult(object entity, Item accountItem)
        {
            Asset asset = (Asset)entity;
            VideoSearchResult result = base.GetSearchResult<VideoSearchResult>(Constants.IndexName, accountItem, i => i.TemplateId == Templates.Video.TemplateID && i.UniqueID == asset.UniqueId);
            if ((result != null) && (result.UniqueID == asset.UniqueId))
                return result;
            
            return null;
        }

        public override bool NeedUpdate(object entity, Item accountItem, MediaServiceSearchResult searchResult)
        {
            Asset asset = (Asset)entity;
            AssetSearchResult result = (AssetSearchResult)searchResult;
            return (asset.ETag != result.ETag);
        }

        public override Item SyncItem(object entity, Item accountItem)
        {
            Asset asset = (Asset)entity;
            if (ExportQueueManager.IsExist(accountItem, Templates.MediaElement.UniqueID, asset.UniqueId))
                return null;
            
            return base.SyncItem(entity, accountItem);
        }

        public override Item UpdateItem(object entity, Item accountItem, Item item)
        {
            Asset asset = (Asset)entity;
            using (new EditContext(item))
            {
                this.UpdateProperties(item, accountItem, asset);
            }
            return item;
        }

        protected virtual void UpdateProperties(Item item, Item accountItem, Asset asset)
        {
            item.Name = ItemUtil.ProposeValidItemName(asset.Snippet.Title);
            item[Templates.MediaElement.Kind] = asset.Kind;
            item[Templates.MediaElement.UniqueID] = asset.UniqueId;
            item[Templates.MediaElement.ETag] = asset.ETag;
            item[Templates.MediaElement.Title] = asset.Snippet.Title;
            item[Templates.MediaElement.Description] = asset.Snippet.Description;
            item[Templates.MediaElement.PublishedAt] = DateUtil.ToIsoDate(asset.Snippet.PublishedAt);
            item[Templates.MediaElement.DefaultThumbnail] = asset.Snippet.Thumbnails != null && asset.Snippet.Thumbnails.Default != null ? asset.Snippet.Thumbnails.Default.Url : string.Empty;
            item[Templates.MediaElement.MediumThumbnail] = asset.Snippet.Thumbnails != null && asset.Snippet.Thumbnails.Medium != null ? asset.Snippet.Thumbnails.Medium.Url : string.Empty;
            item[Templates.MediaElement.HighThumbnail] = asset.Snippet.Thumbnails != null && asset.Snippet.Thumbnails.High != null ? asset.Snippet.Thumbnails.High.Url : string.Empty;
            item[Templates.MediaElement.StandardThumbnail] = asset.Snippet.Thumbnails != null && asset.Snippet.Thumbnails.Standard != null ? asset.Snippet.Thumbnails.Standard.Url : string.Empty;
            item[Templates.MediaElement.MaxResolutionThumbnail] = asset.Snippet.Thumbnails != null && asset.Snippet.Thumbnails.MaxResolution != null ? asset.Snippet.Thumbnails.MaxResolution.Url : string.Empty;

            if (asset.Status != null)
            {
                item[Templates.MediaElement.PrivacyStatus] = asset.Status.PrivacyStatus;
            }

        }
    }

}
