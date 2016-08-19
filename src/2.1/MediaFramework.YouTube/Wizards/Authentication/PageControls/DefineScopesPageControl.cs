using System;
using Sitecore.Web.UI.HtmlControls;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Wizards.Authentication.PageControls
{
    public class DefineScopesPageControl : AuthenticationWizardPageControl
    {
        protected Checkbox ScopeYouTube;
        protected Checkbox ScopeYouTubeForceSsl;
        protected Checkbox ScopeYouTubeReadOnly;
        protected Checkbox ScopeYouTubeUpload;
        protected Checkbox ScopeYouTubePartner;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.WizardForm.WizardPageChanged += this.WizardFormWizardPageChanged;
            this.WizardForm.WizardPageChanging += this.WizardFormWizardPageChanging;

            if (Sitecore.Context.ClientPage.IsPostBack || Sitecore.Context.ClientPage.IsEvent)
                return;

            this.SetScopeValues();
        }

        protected virtual void WizardFormWizardPageChanged(object sender, AuthenticationWizardPageChangedEventArgs e)
        {
            if (e.CurrentPage != this.WizardPageId)
                return;

            this.ScopeYouTube.Checked = true;
            this.ScopeYouTubeForceSsl.Checked = true;
            this.ScopeYouTubeReadOnly.Checked = true;
            this.ScopeYouTubeUpload.Checked = (bool)this.WizardForm.ServerProperties[Constants.ServerProperties.ScopeYouTubeUpload];
            this.ScopeYouTubePartner.Checked = true;

        }

        protected virtual void WizardFormWizardPageChanging(object sender, AuthenticationWizardPageChangingEventArgs e)
        {
            if (e.CurrentPage != this.WizardPageId)
                return;

            this.WizardForm.ServerProperties[Constants.ServerProperties.ScopeYouTube] = this.ScopeYouTube.Checked;
            this.WizardForm.ServerProperties[Constants.ServerProperties.ScopeYouTubeForceSsl] = this.ScopeYouTubeForceSsl.Checked;
            this.WizardForm.ServerProperties[Constants.ServerProperties.ScopeYouTubeReadOnly] = this.ScopeYouTubeReadOnly.Checked;
            this.WizardForm.ServerProperties[Constants.ServerProperties.ScopeYouTubeUpload] = this.ScopeYouTubeUpload.Checked;
            this.WizardForm.ServerProperties[Constants.ServerProperties.ScopeYouTubePartner] = this.ScopeYouTubePartner.Checked;
        }

        protected virtual void SetScopeValues()
        {
            this.WizardForm.ServerProperties[Constants.ServerProperties.ScopeYouTube] = true;
            this.WizardForm.ServerProperties[Constants.ServerProperties.ScopeYouTubeForceSsl] = true;
            this.WizardForm.ServerProperties[Constants.ServerProperties.ScopeYouTubeReadOnly] = true;
            this.WizardForm.ServerProperties[Constants.ServerProperties.ScopeYouTubeUpload] = false;
            this.WizardForm.ServerProperties[Constants.ServerProperties.ScopeYouTubePartner] = true;

            if (this.WizardForm.AccountItem != null)
            {
                this.WizardForm.ServerProperties[Constants.ServerProperties.ScopeYouTubeUpload] = this.WizardForm.AccountItem[Templates.Account.ScopeYouTubeUpload] == "1";
            }
        }
    }
}
