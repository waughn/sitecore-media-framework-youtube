using System.Collections.Generic;
using System.Web;
using RestSharp;
using Sitecore.Data.Items;
using Sitecore.MediaFramework.Diagnostics;
using Sitecore.MediaFramework.Import;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities.Collections;
using Sitecore.SharedSource.MediaFramework.YouTube.Security;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Import
{
    public abstract class EntityCollectionImporter<TEntity> : IImportExecuter
    {
        protected abstract string RequestName { get; }

        protected virtual string IdParameterValue { get; set; }

        protected virtual bool MineParameterValue { get; set; }

        protected virtual bool NoParametersNeeded { get; set; }

        public virtual IEnumerable<object> GetData(Item accountItem)
        {
            var authenticator = new YouTubeAuthenticator(accountItem);
            var context = new Sitecore.RestSharp.RestContext(Constants.SitecoreRestSharpService, authenticator);
            var parameters = new List<Parameter>();

            if (!string.IsNullOrEmpty(this.IdParameterValue))
            {
                var idParameter = new Parameter
                {
                    Type = ParameterType.UrlSegment,
                    Name = "id",
                    Value = this.IdParameterValue
                };

                parameters.Add(idParameter);
            }

            if (this.MineParameterValue)
            {
                var mineParameter = new Parameter
                {
                    Type = ParameterType.UrlSegment,
                    Name = "mine",
                    Value = this.MineParameterValue
                };

                parameters.Add(mineParameter);
            }

            string nextPageToken = string.Empty;
            var pageTokenParameter = new Parameter
            {
                Type = ParameterType.UrlSegment,
                Name = "pageToken",
                Value = nextPageToken
            };

            parameters.Add(pageTokenParameter);

            if (!string.IsNullOrEmpty(this.IdParameterValue) || this.MineParameterValue || this.NoParametersNeeded)
            {
                do
                {
                    pageTokenParameter.Value = nextPageToken;

                    IRestResponse<PagedCollection<TEntity>> pagedEntities = context.Read<PagedCollection<TEntity>>(this.RequestName, parameters);
                    if (pagedEntities == null || pagedEntities.Data == null || pagedEntities.Data.Items == null)
                    {
                        LogHelper.Warn("Null Result during importing", this);
                        throw new HttpException("Http null result");
                    }

                    nextPageToken = (pagedEntities.Data.NextPageToken != null && pagedEntities.Data.NextPageToken != nextPageToken) ? pagedEntities.Data.NextPageToken : string.Empty;
                    foreach (TEntity entity in pagedEntities.Data.Items)
                    {
                        yield return entity;
                    }

                } while (!string.IsNullOrEmpty(nextPageToken));
            }
        }
    }
}
