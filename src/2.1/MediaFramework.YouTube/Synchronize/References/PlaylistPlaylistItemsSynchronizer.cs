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
    public class PlaylistPlaylistItemsSynchronizer : IdReferenceSynchronizer<Playlist>
    {
        protected readonly string RequestName = "read_playlist_playlistitems";

        protected override List<ID> GetReference(Playlist entity, Item accountItem)
        {
            var authenticator = new YouTubeAuthenticator(accountItem);
            var context = new RestSharp.RestContext(Constants.SitecoreRestSharpService, authenticator);
            var parameters = new List<Parameter>
            {
                new Parameter
                {
                    Type = ParameterType.UrlSegment,
                    Name = "playlistId",
                    Value = entity.UniqueId
                },
            };

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

                IRestResponse<PagedCollection<PlaylistItem>> pagedEntities = context.Read<PagedCollection<PlaylistItem>>(this.RequestName, parameters);
                if (pagedEntities == null || pagedEntities.Data == null || pagedEntities.Data.Items == null)
                {
                    LogHelper.Warn("Null Result during synchronization", this);
                    throw new HttpException("Http null result");
                }

                nextPageToken = (pagedEntities.Data.NextPageToken != null && pagedEntities.Data.NextPageToken != nextPageToken) ? pagedEntities.Data.NextPageToken : string.Empty;
                videoIds.AddRange(pagedEntities.Data.Items.Select(p => p.ContentDetails.VideoId));

            }
            while (!string.IsNullOrEmpty(nextPageToken));

            if (!videoIds.Any())
                return new List<ID>();

            var ancestorFilter = ContentSearchUtil.GetAncestorFilter<PlaylistItemSearchResult>(accountItem, Templates.Video.TemplateID);
            var expression = videoIds.Aggregate(PredicateBuilder.False<PlaylistItemSearchResult>(), (current, videoId) => current.Or(i => i.UniqueID == videoId));

            var all = ContentSearchUtil.FindAll(Constants.IndexName, PredicateBuilder.And(ancestorFilter, expression));

            if (all.Count < videoIds.Count)
            {
                var itemSynchronizer = MediaFrameworkContext.GetItemSynchronizer(typeof(PlaylistItem));
                if (itemSynchronizer != null)
                {
                    foreach (var id in videoIds)
                    {
                        string videoId = id;
                        if (all.All(i => i.UniqueID != videoId))
                        {
                            var playlistItem = new PlaylistItem
                            {
                                UniqueId = videoId,
                                Snippet = new Snippet { ResourceId = new Resource { VideoId = videoId } }
                            };
                            
                            var playlistItemSearchResult = itemSynchronizer.Fallback(playlistItem, accountItem) as PlaylistItemSearchResult;

                            if (playlistItemSearchResult != null)
                                all.Add(playlistItemSearchResult);
                        }
                    }
                }
            }

            return all.Select(i => i.ItemId).ToList();
        }
    }
}
