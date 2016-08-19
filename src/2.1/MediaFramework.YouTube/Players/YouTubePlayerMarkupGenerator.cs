using Sitecore.Data.Items;
using Sitecore.MediaFramework.Account;
using Sitecore.MediaFramework.Pipelines.MediaGenerateMarkup;
using Sitecore.MediaFramework.Players;
using System;
using System.Linq;
using System.Web;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Players
{
    public class YouTubePlayerMarkupGenerator : PlayerMarkupGeneratorBase
    {
        private string embedHtml;
        private string scriptUrl;

        public override PlayerMarkupResult Generate(MediaGenerateMarkupArgs args)
        {
            string playerIdentifier = Guid.NewGuid().ToString("N");

            PlayerMarkupResult result = new PlayerMarkupResult
            {
                Html = string.Format(this.EmbedHtml, playerIdentifier, args.MediaItem[Templates.MediaElement.UniqueID], args.Properties.Width, args.Properties.Height, string.Empty)
            };

            result.ScriptUrls.Add(this.ScriptUrl);
            result.ScriptUrls.Add(this.AnalyticsScriptUrl);

            return result;
        }

        public override string GetPreviewImage(MediaGenerateMarkupArgs args)
        {
            return PlayerManager.GetPreviewImage(args, Templates.MediaElement.DefaultThumbnail);
        }

        public override Item GetDefaultPlayer(MediaGenerateMarkupArgs args)
        {
            return AccountManager.GetPlayers(args.AccountItem).FirstOrDefault(player => (player[Templates.Player.IsDefault] == "1"));
        }

        public override string GetMediaId(Item item)
        {
            return item[Templates.MediaElement.UniqueID];
        }

        public string EmbedHtml
        {
            get
            {
                return this.embedHtml;
            }
            set
            {
                this.embedHtml = HttpUtility.UrlDecode(value);
            }
        }

        public string ScriptUrl
        {
            get
            {
                return this.scriptUrl;
            }
            set
            {
                this.scriptUrl = HttpUtility.UrlDecode(value);
            }
        }
    }
}
