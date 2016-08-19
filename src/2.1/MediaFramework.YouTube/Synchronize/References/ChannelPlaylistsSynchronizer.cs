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
    public class ChannelPlaylistsSynchronizer : IdReferenceSynchronizer<Channel>
    {
        protected readonly string RequestName = "read_channel_playlists";

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

            var playlistIds = new List<string>();

            do
            {
                pageTokenParameter.Value = nextPageToken;

                IRestResponse<PagedCollection<Playlist>> pagedEntities = context.Read<PagedCollection<Playlist>>(this.RequestName, parameters);
                if (pagedEntities == null || pagedEntities.Data == null || pagedEntities.Data.Items == null)
                {
                    LogHelper.Warn("Null Result during synchronization", this);
                    throw new HttpException("Http null result");
                }

                nextPageToken = (pagedEntities.Data.NextPageToken != null && pagedEntities.Data.NextPageToken != nextPageToken) ? pagedEntities.Data.NextPageToken : string.Empty;
                playlistIds.AddRange(pagedEntities.Data.Items.Select(p => p.UniqueId));
            }
            while (!string.IsNullOrEmpty(nextPageToken));

            if (!playlistIds.Any())
                return new List<ID>();

            var ancestorFilter = ContentSearchUtil.GetAncestorFilter<PlaylistSearchResult>(accountItem, Templates.Playlist.TemplateID);
            var expression = playlistIds.Aggregate(PredicateBuilder.False<PlaylistSearchResult>(), (current, playlistId) => current.Or(i => i.UniqueID == playlistId));

            var all = ContentSearchUtil.FindAll(Constants.IndexName, PredicateBuilder.And(ancestorFilter, expression));

            if (all.Count < playlistIds.Count)
            {
                var itemSynchronizer = MediaFrameworkContext.GetItemSynchronizer(typeof(Playlist));
                if (itemSynchronizer != null)
                {
                    foreach (var id in playlistIds)
                    {
                        string playlistId = id;
                        if (all.All(i => i.UniqueID != playlistId))
                        {
                            var playlist = new Playlist
                            {
                                UniqueId = playlistId,
                                Snippet = new Snippet { PlaylistId = playlistId }
                            };
                            
                            var playlistSearchResult = itemSynchronizer.Fallback(playlist, accountItem) as PlaylistSearchResult;

                            if (playlistSearchResult != null)
                                all.Add(playlistSearchResult);
                        }
                    }
                }
            }

            return all.Select(i => i.ItemId).ToList();
        }
    }
}
