using System;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Wizards.Authentication.PageControls
{
    public class WebApplicationClientIdPageControl : AuthenticationWizardPageControl
    {
        protected const string OnClientInformationChangedMessageName = "WebApplicationClientIdPageControl:OnClientInformationChanged";

        protected Edit ClientId;
        protected Edit ClientSecret;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.WizardForm.WizardPageChanged += this.WizardFormWizardPageChanged;
            this.WizardForm.WizardPageChanging += this.WizardFormWizardPageChanging;

            if (Sitecore.Context.ClientPage.IsPostBack || Sitecore.Context.ClientPage.IsEvent)
                return; 
            
            this.SetClientValues();
        }

        protected virtual void WizardFormWizardPageChanged(object sender, AuthenticationWizardPageChangedEventArgs e)
        {
            if (e.CurrentPage != this.WizardPageId)
                return;

            this.ClientId.Value = this.WizardForm.ServerProperties[Constants.ServerProperties.ClientId] != null ? this.WizardForm.ServerProperties[Constants.ServerProperties.ClientId].ToString() : string.Empty;
            this.ClientSecret.Value = this.WizardForm.ServerProperties[Constants.ServerProperties.ClientSecret] != null ? this.WizardForm.ServerProperties[Constants.ServerProperties.ClientSecret].ToString() : string.Empty;

            this.OnClientInformationChanged();

            if (string.IsNullOrEmpty(this.ClientId.Value))
                Sitecore.Context.ClientPage.ClientResponse.Focus(this.ClientId.ID);
        }

        protected virtual void WizardFormWizardPageChanging(object sender, AuthenticationWizardPageChangingEventArgs e)
        {
            if (e.CurrentPage != this.WizardPageId)
                return;

            this.WizardForm.ServerProperties[Constants.ServerProperties.ClientId] = this.ClientId.Value;
            this.WizardForm.ServerProperties[Constants.ServerProperties.ClientSecret] = this.ClientSecret.Value;
        }

        public override void HandleMessage(Message message)
        {
            if (string.Compare(message.Name, OnClientInformationChangedMessageName, StringComparison.InvariantCulture) == 0)
                this.OnClientInformationChanged();
            else
                base.HandleMessage(message);
        }

        protected virtual void OnClientInformationChanged()
        {
            this.WizardForm.ButtonNext.Disabled = string.IsNullOrEmpty(this.ClientId.Value) || string.IsNullOrEmpty(this.ClientSecret.Value);
        }

        protected virtual void SetClientValues()
        {
            if (this.WizardForm.AccountItem != null)
            {
                this.WizardForm.ServerProperties[Constants.ServerProperties.ClientId] = this.WizardForm.AccountItem[Templates.Account.ClientID];
                this.WizardForm.ServerProperties[Constants.ServerProperties.ClientSecret] = this.WizardForm.AccountItem[Templates.Account.ClientSecret];
            }
        }
    }
}
