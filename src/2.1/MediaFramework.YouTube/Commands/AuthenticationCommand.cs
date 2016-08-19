using System;
using System.Collections.Specialized;
using Sitecore.Data.Items;
using Sitecore.Integration.Common.Commands;
using Sitecore.MediaFramework.Account;
using Sitecore.MediaFramework.Diagnostics;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Text;
using Sitecore.Web.UI.Sheer;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Commands
{
    [Serializable]
    public class AuthenticationCommand : RuleBasedCommand
    {
        protected override CommandState DefaultCommandState
        {
            get
            {
                return CommandState.Hidden;
            }
        }

        protected Item AccountItem { get; private set; }

        public override void Execute(CommandContext context)
        {
            var child = context.Items[0];
            if (child == null)
                return;

            this.AccountItem = AccountManager.GetAccountItemForDescendant(child);
            if (this.AccountItem == null)
            {
                LogHelper.Error("Error while executing manual operation. An Account item cannot be found.", this);
                return;
            }

            var urlString = new UrlString(UIUtil.GetUri(Constants.Authentication.ControlWizard));
            this.AccountItem.Uri.AddToUrlString(urlString);
            UIUtil.AddContentDatabaseParameter(urlString);

            Context.ClientPage.Start(this, "RunWizard", new NameValueCollection
            {
                {
                    "url",
                    urlString.ToString()
                }
            });
        }

        protected void RunWizard(ClientPipelineArgs args)
        {
            if (args.IsPostBack)
            {
                Context.ClientPage.SendMessage(this, string.Format("item:load(id={0})", this.AccountItem.ID));
            }
            else
            {
                Context.ClientPage.ClientResponse.ShowModalDialog(args.Parameters["url"], "550px", "690px", string.Empty, true);
                args.WaitForPostBack();
            }
        }

    }
}
