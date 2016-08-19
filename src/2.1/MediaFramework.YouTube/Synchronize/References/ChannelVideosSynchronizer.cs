using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.Data;
using Sitecore.Integration.Common.Utils;
using Sitecore.MediaFramework.Diagnostics;
using Sitecore.MediaFramework.Synchronize.References;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities.Collections;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities.Common;
using Sitecore.SharedSource.MediaFramework.YouTube.Indexing.Entities;
using Sitecore.Data.Items;
using Sitecore.MediaFramework;
using Sitecore.SharedSource.MediaFramework.YouTube.Security;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Synchronize.References
{
    public class ChannelVideosSynchronizer : IdReferenceSynchronizer<Channel>
    {
        protected readonly string RequestName = "read_channel_videos";

        protected override List<ID> GetReference(Channel entity, Item accountItem)
        {
            var authenticator = new YouTubeAuthenticator(accountItem);
            var context = new RestSharp.RestContext(Constants.SitecoreRestSharpService, authenticator);
            var parameters = new List<Parameter>();

            string nextPageToken = string.Empty;
            var pageTokenParameter = new Parameter
            {
                Type = ParameterType.UrlSegment,
                Name = "pageToken",
                Value = nextPageToken
            };

            parameters.Add(pageTokenParameter);

            var videoIds = new List<string>();

            do
            {
                pageTokenParameter.Value = nextPageToken;

                IRestResponse<PagedCollection<SearchResult>> pagedEntities = context.Read<PagedCollection<SearchResult>>(this.RequestName, parameters);
                if (pagedEntities == null || pagedEntities.Data == null || pagedEntities.Data.Items == null)
                {
                    LogHelper.Warn("Null Result during synchronization", this);
                    throw new HttpException("Http null result");
                }

                nextPageToken = (pagedEntities.Data.NextPageToken != null && pagedEntities.Data.NextPageToken != nextPageToken) ? pagedEntities.Data.NextPageToken : string.Empty;
                videoIds.AddRange(pagedEntities.Data.Items.Where(d => d.Id != null).Select(d => d.Id.VideoId));
            }
            while (!string.IsNullOrEmpty(nextPageToken));

            if (!videoIds.Any())
                return new List<ID>();

            var ancestorFilter = ContentSearchUtil.GetAncestorFilter<VideoSearchResult>(accountItem, Templates.Video.TemplateID);
            var expression = videoIds.Aggregate(PredicateBuilder.False<VideoSearchResult>(), (current, videoId) => current.Or(i => i.UniqueID == videoId));

            var all = ContentSearchUtil.FindAll(Constants.IndexName, PredicateBuilder.And(ancestorFilter, expression));

            if (all.Count < videoIds.Count)
            {
                var itemSynchronizer = MediaFrameworkContext.GetItemSynchronizer(typeof(Video));
                if (itemSynchronizer != null)
                {
                    foreach (var id in videoIds)
                    {
                        string videoId = id;
                        if (all.All(i => i.UniqueID != videoId))
                        {
                            var video = new Video
                            {
                                UniqueId = videoId,
                                Snippet = new Snippet
                                {
                                    ResourceId = new Resource { VideoId = videoId }
                                }
                            };

                            var videoSearchResult = itemSynchronizer.Fallback(video, accountItem) as VideoSearchResult;

                            if (videoSearchResult != null)
                                all.Add(videoSearchResult);
                        }
                    }
                }
            }

            return all.Select(i => i.ItemId).ToList();
        }
    }
}
