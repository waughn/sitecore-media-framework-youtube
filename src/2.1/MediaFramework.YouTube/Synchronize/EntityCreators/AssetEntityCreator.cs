using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.MediaFramework.Synchronize;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities.Common;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Synchronize.EntityCreators
{
    public abstract class AssetEntityCreator<T> : IMediaServiceEntityCreator where T : Asset, new()
    {
        public virtual object CreateEntity(Item item)
        {
            var tagItems = ((MultilistField)item.Fields[Templates.MediaElement.TagsList]).GetItems();

            T asset = Activator.CreateInstance<T>();
            asset.Kind = item[Templates.MediaElement.Kind];
            asset.UniqueId = item[Templates.MediaElement.UniqueID];
            asset.ETag = item[Templates.MediaElement.ETag];

            asset.Snippet = new Snippet
            {
                Title = item[Templates.MediaElement.Title],
                Description = item[Templates.MediaElement.Description],
                PublishedAt = DateUtil.IsoDateToDateTime(item[Templates.MediaElement.PublishedAt]),
                Tags = tagItems.Any() ? tagItems.Select(x => x[Templates.Tag.Name]).ToList() : new List<string>(),
                Thumbnails =
                    new Thumbnails
                    {
                        Default = new Thumbnail { Url = item[Templates.MediaElement.DefaultThumbnail] },
                        Medium = new Thumbnail { Url = item[Templates.MediaElement.MediumThumbnail] },
                        High = new Thumbnail { Url = item[Templates.MediaElement.HighThumbnail] },
                        Standard = new Thumbnail { Url = item[Templates.MediaElement.StandardThumbnail] },
                        MaxResolution = new Thumbnail { Url = item[Templates.MediaElement.MaxResolutionThumbnail] }
                    }
            };

            asset.Status = new Status
            {
                PrivacyStatus = item[Templates.MediaElement.PrivacyStatus]
            };

            return asset;
        }
    }
}
