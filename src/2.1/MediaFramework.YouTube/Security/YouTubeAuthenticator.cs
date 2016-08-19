using System.Globalization;
using RestSharp;
using Sitecore.Collections;
using Sitecore.Data.Items;
using Sitecore.SharedSource.MediaFramework.YouTube.Authentication;
using DateTime = System.DateTime;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Security
{
    public class YouTubeAuthenticator : IAuthenticator
    {
        protected bool IsPublicKey { get; }
        protected readonly string AccessKey;
        protected readonly YouTubeAccount Account;

        public YouTubeAuthenticator(Item accountItem)
        {
            if (accountItem.TemplateID == Templates.AccountPublic.TemplateID)
            {
                this.IsPublicKey = true;

                this.AccessKey = accountItem[Templates.AccountPublic.AccessKey];
            }
            else if (accountItem.TemplateID == Templates.Account.TemplateID)
            {
                this.IsPublicKey = false;

                this.Account = this.GetAccount(accountItem);
                if (this.Account.ExpirationDate < System.DateTime.UtcNow)
                {
                    var parameters = new SafeDictionary<string>
                    {
                        {"client_id", this.Account.ClientID},
                        {"client_secret", this.Account.ClientSecret},
                        {"refresh_token", this.Account.RefreshToken}
                    };

                    var authenticator = new RefreshTokenAuthenticator();
                    var response = authenticator.Authenticate(parameters);

                    if (response != null && response.Data != null)
                    {
                        this.SaveToken(accountItem, response.Data);
                        this.Account = this.GetAccount(accountItem);
                    }
                }
            }
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            if (this.IsPublicKey)
            {
                request.AddUrlSegment("key", this.AccessKey);
                request.AddUrlSegment("access_token", string.Empty);
            }
            else
            {
                request.AddUrlSegment("key", string.Empty);
                request.AddUrlSegment("access_token", this.Account.AccessToken);
            }
        }

        public YouTubeAccount GetAccount(Item item)
        {
            return new YouTubeAccount
                {
                    ClientID = item[Templates.Account.ClientID],
                    ClientSecret = item[Templates.Account.ClientSecret],
                    AccessToken = item[Templates.Account.AccessToken],
                    RefreshToken = item[Templates.Account.RefreshToken],
                    TokenType = item[Templates.Account.TokenType],
                    ExpiresIn = item[Templates.Account.ExpiresIn],
                    ExpirationDate = DateUtil.IsoDateToDateTime(item[Templates.Account.ExpirationDate])
                };
        }

        public void SaveToken(Item item, Entities.Token token)
        {
            item.Editing.BeginEdit();

            item[Templates.Account.AccessToken] = token.AccessToken;
            item[Templates.Account.TokenType] = token.TokenType;
            item[Templates.Account.ExpiresIn] = token.ExpiresIn.ToString(CultureInfo.InvariantCulture);
            item[Templates.Account.ExpirationDate] = DateUtil.ToIsoDate(DateTime.UtcNow.AddSeconds(token.ExpiresIn), false, true);

            item.Editing.EndEdit();
        }
    }
}
