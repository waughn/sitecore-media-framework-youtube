using System;
using System.Linq;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.MediaFramework.Entities;
using Sitecore.MediaFramework.Synchronize.Fallback;
using Sitecore.SharedSource.MediaFramework.YouTube.Indexing.Entities;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Synchronize.Fallback
{
    public abstract class AssetFallback<T> : DatabaseFallbackBase where T : AssetSearchResult, new()
    {
        protected override MediaServiceSearchResult GetSearchResult(Item item)
        {
            T asset = Activator.CreateInstance<T>();
            
            asset.Kind = item[Templates.MediaElement.Kind];
            asset.UniqueID = item[Templates.MediaElement.UniqueID];
            asset.ETag = item[Templates.MediaElement.ETag];
            asset.Title = item[Templates.MediaElement.Title];
            asset.Description = item[Templates.MediaElement.Description];
            asset.PublishedAt = DateUtil.IsoDateToDateTime(item[Templates.MediaElement.PublishedAt]);
            asset.TagsList = ID.ParseArray(item[Templates.MediaElement.TagsList], false).ToList();
            asset.DefaultThumbnail = item[Templates.MediaElement.DefaultThumbnail];
            asset.MediumThumbnail = item[Templates.MediaElement.MediumThumbnail];
            asset.HighThumbnail = item[Templates.MediaElement.HighThumbnail];
            asset.StandardThumbnail = item[Templates.MediaElement.StandardThumbnail];
            asset.MaxResolutionThumbnail = item[Templates.MediaElement.MaxResolutionThumbnail];
            asset.PrivacyStatus = item[Templates.MediaElement.PrivacyStatus];

            return asset;
        }
    }
}
