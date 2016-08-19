using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;
using Sitecore.Data.Items;
using Sitecore.MediaFramework.Diagnostics;
using Sitecore.MediaFramework.Import;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities.Collections;
using Sitecore.SharedSource.MediaFramework.YouTube.Security;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Import
{
    public class PlaylistItemCollectionImporter : IImportExecuter
    {
        protected string RequestName
        {
            get
            {
                return "read_playlistitems";
            }
        }

        public IEnumerable<object> GetData(Item accountItem)
        {
            string[] idList = { };

            if (accountItem.TemplateID == Templates.AccountPublic.TemplateID)
            {
                string ids = accountItem[Templates.AccountPublic.PlaylistIDs];
                if (!string.IsNullOrEmpty(ids))
                {
                    idList = ids.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                }
            }
            else
            {
                var data = ImportManager.ImportList<Playlist>("import_youtube_playlists", accountItem);
                if (data != null)
                    idList = data.Select(d => d.UniqueId).ToArray();
            }

            var authenticator = new YouTubeAuthenticator(accountItem);
            var context = new RestSharp.RestContext(Constants.SitecoreRestSharpService, authenticator);
            var parameters = new List<Parameter>();

            var playlistIdParameter = new Parameter
            {
                Type = ParameterType.UrlSegment,
                Name = "playlistId"
            };

            parameters.Add(playlistIdParameter);

            string nextPageToken = string.Empty;
            var pageTokenParameter = new Parameter
            {
                Type = ParameterType.UrlSegment,
                Name = "pageToken",
                Value = nextPageToken
            };

            parameters.Add(pageTokenParameter);

            foreach (string id in idList)
            {
                {
                    playlistIdParameter.Value = id;

                    do
                    {
                        pageTokenParameter.Value = nextPageToken;

                        IRestResponse<PagedCollection<PlaylistItem>> pagedEntities = context.Read<PagedCollection<PlaylistItem>>(this.RequestName, parameters);
                        if (pagedEntities == null || pagedEntities.Data == null || pagedEntities.Data.Items == null)
                        {
                            LogHelper.Warn("Null Result during importing", this);
                            throw new HttpException("Http null result");
                        }

                        nextPageToken = (pagedEntities.Data.NextPageToken != null && pagedEntities.Data.NextPageToken != nextPageToken) ? pagedEntities.Data.NextPageToken : string.Empty;
                        foreach (PlaylistItem entity in pagedEntities.Data.Items)
                        {
                            yield return entity;
                        }
                    }
                    while (!string.IsNullOrEmpty(nextPageToken));
                }
            }
        }
    }
}
