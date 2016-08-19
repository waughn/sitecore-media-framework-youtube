using System;
using System.Globalization;
using Sitecore.Data.Items;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Wizards.Authentication.PageControls
{
    public class GoodByePageControl : AuthenticationWizardPageControl
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.WizardForm.WizardPageChanged += this.WizardFormWizardPageChanged;
            this.WizardForm.WizardPageChanging += this.WizardFormWizardPageChanging;
        }

        protected virtual void WizardFormWizardPageChanged(object sender, AuthenticationWizardPageChangedEventArgs e)
        {
            if (e.CurrentPage != this.WizardPageId)
                return;

            this.WizardForm.ButtonBack.Disabled = true;
        }

        protected virtual void WizardFormWizardPageChanging(object sender, AuthenticationWizardPageChangingEventArgs e)
        {
            if (e.NewPage != this.WizardPageId)
                return;

            this.ConfigureAccount();
        }

        protected virtual void ConfigureAccount()
        {
            Item accountItem = this.WizardForm.AccountItem;

            accountItem.Editing.BeginEdit();

            accountItem[Templates.Account.ClientID] = (string)this.WizardForm.ServerProperties[Constants.ServerProperties.ClientId];
            accountItem[Templates.Account.ClientSecret] = (string)this.WizardForm.ServerProperties[Constants.ServerProperties.ClientSecret];

            accountItem[Templates.Account.AccessToken] = (string)this.WizardForm.ServerProperties[Constants.ServerProperties.AccessToken];
            accountItem[Templates.Account.RefreshToken] = (string)this.WizardForm.ServerProperties[Constants.ServerProperties.RefreshToken];
            accountItem[Templates.Account.TokenType] = (string)this.WizardForm.ServerProperties[Constants.ServerProperties.TokenType];
            accountItem[Templates.Account.ExpiresIn] = ((int)this.WizardForm.ServerProperties[Constants.ServerProperties.ExpiresIn]).ToString(CultureInfo.InvariantCulture);
            accountItem[Templates.Account.ExpirationDate] = DateUtil.ToIsoDate(DateTime.UtcNow.AddSeconds((int)this.WizardForm.ServerProperties[Constants.ServerProperties.ExpiresIn]), false, true);

            accountItem[Templates.Account.ScopeYouTube] = (bool)this.WizardForm.ServerProperties[Constants.ServerProperties.ScopeYouTube] ? "1" : "0";
            accountItem[Templates.Account.ScopeYouTubeForceSsl] = (bool)this.WizardForm.ServerProperties[Constants.ServerProperties.ScopeYouTubeForceSsl] ? "1" : "0";
            accountItem[Templates.Account.ScopeYouTubeReadOnly] = (bool)this.WizardForm.ServerProperties[Constants.ServerProperties.ScopeYouTubeReadOnly] ? "1" : "0";
            accountItem[Templates.Account.ScopeYouTubeUpload] = (bool)this.WizardForm.ServerProperties[Constants.ServerProperties.ScopeYouTubeUpload] ? "1" : "0";
            accountItem[Templates.Account.ScopeYouTubePartner] = (bool)this.WizardForm.ServerProperties[Constants.ServerProperties.ScopeYouTubePartner] ? "1" : "0";

            accountItem.Editing.EndEdit();
        }
    }
}
