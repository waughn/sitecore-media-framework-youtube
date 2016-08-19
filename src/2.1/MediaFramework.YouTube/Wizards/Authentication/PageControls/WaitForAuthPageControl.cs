using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;
using Sitecore.Collections;
using Sitecore.Globalization;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Wizards.Authentication.PageControls
{
    public class WaitForAuthPageControl : AuthenticationWizardPageControl
    {
        protected const string CheckAuthStatusMessageName = "WaitForAuthPageControl:CheckAuthStatus";

        protected Border ErrorPane;
        protected Border WaitPane;
        protected Literal ErrorText;

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

            this.AuthenticateAccount();

            this.WizardForm.ButtonBack.Disabled = false;
            this.WizardForm.ButtonNext.Disabled = true;
        }

        protected virtual void WizardFormWizardPageChanging(object sender, AuthenticationWizardPageChangingEventArgs e)
        {
            if (e.CurrentPage != this.WizardPageId)
                return;
            this.WaitPane.Visible = true;
            this.ErrorPane.Visible = false;
        }

        public override void HandleMessage(Message message)
        {
            if (string.Compare(message.Name, CheckAuthStatusMessageName, StringComparison.InvariantCulture) == 0)
                this.CheckAuthStatus();
            else
                base.HandleMessage(message);
        }

        protected virtual void AuthenticateAccount()
        {
            Guid guid = Guid.NewGuid();
            this.WizardForm.ServerProperties[Constants.ServerProperties.AuthStateKey] = guid;
            this.WizardForm.ServerProperties[Constants.ServerProperties.AuthStartDateUtc] = DateTime.UtcNow;

            var authParameters = new SafeDictionary<string>
            {
                {"response_type", "code"},
                {"access_type", "offline"},
                {"approval_prompt", "force"},
                {"client_id", this.WizardForm.ServerProperties[Constants.ServerProperties.ClientId].ToString()},
                {"scope", string.Join("+", this.GetScopes())},
                {"state", guid.ToString()}
            };

            var tokenParameters = new SafeDictionary<string>
            {
                {"client_id", this.WizardForm.ServerProperties[Constants.ServerProperties.ClientId].ToString()},
                {"client_secret", this.WizardForm.ServerProperties[Constants.ServerProperties.ClientSecret].ToString()}
            };

            var authIndex = string.Format("WizardAuthenticationParameters_{0}", guid);
            HttpContext.Current.Session[authIndex] = authParameters;

            var tokenIndex = string.Format("WizardTokenParameters_{0}", guid);
            HttpContext.Current.Session[tokenIndex] = tokenParameters;

            var authUrl = string.Format("{0}?stateKey={1}", Constants.Authentication.RedirectUri, guid);
            SheerResponse.Eval("window.top.open('" + WebUtil.GetFullUrl(authUrl) + "', 'Auth', 'location=0,menubar=0,status=0,toolbar=0,resizable=1');");
            SheerResponse.Eval(this.GetCheckAuthCompletingJavaScript());
        }

        protected virtual List<string> GetScopes()
        {
            var scopes = new List<string>();

            if ((bool)this.WizardForm.ServerProperties[Constants.ServerProperties.ScopeYouTube])
                scopes.Add(Constants.GoogleApiScope.YouTube);

            if ((bool)this.WizardForm.ServerProperties[Constants.ServerProperties.ScopeYouTubeForceSsl])
                scopes.Add(Constants.GoogleApiScope.YouTubeForceSsl);

            if ((bool)this.WizardForm.ServerProperties[Constants.ServerProperties.ScopeYouTubeReadOnly])
                scopes.Add(Constants.GoogleApiScope.YouTubeReadOnly);

            if ((bool)this.WizardForm.ServerProperties[Constants.ServerProperties.ScopeYouTubeUpload])
                scopes.Add(Constants.GoogleApiScope.YouTubeUpload);

            if ((bool)this.WizardForm.ServerProperties[Constants.ServerProperties.ScopeYouTubePartner])
                scopes.Add(Constants.GoogleApiScope.YouTubePartner);

            return scopes;
        }

        protected virtual void CheckAuthStatus()
        {
            HttpSessionState session = HttpContext.Current.Session;
            var name = string.Format("WizardAuthenticationToken_{0}", this.WizardForm.ServerProperties[Constants.ServerProperties.AuthStateKey]);
            double totalSeconds = DateTime.UtcNow.Subtract((DateTime)this.WizardForm.ServerProperties[Constants.ServerProperties.AuthStartDateUtc]).TotalSeconds;
            const int authenticationTimeout = 60; 

            if (session != null && session[name] != null)
            {
                if (session[name] is Token)
                {
                    var token = session[name] as Token;

                    this.WizardForm.ServerProperties[Constants.ServerProperties.AccessToken] = token.AccessToken;
                    this.WizardForm.ServerProperties[Constants.ServerProperties.TokenType] = token.TokenType;
                    this.WizardForm.ServerProperties[Constants.ServerProperties.ExpiresIn] = token.ExpiresIn;
                    this.WizardForm.ServerProperties[Constants.ServerProperties.RefreshToken] = token.RefreshToken;

                    this.WizardForm.Next();
                }
                else if (session[name] is NameValueCollection)
                {
                    var collection = session[name] as NameValueCollection;
                    string message = collection["ErrorMessage"];
                    if (!string.IsNullOrEmpty(message))
                        this.ShowAuthError(message);
                }

                session[name] = null;
                session.Remove(name);
            }
            else if (totalSeconds >= authenticationTimeout)
                this.ShowAuthError(Translate.Text("The authentication process timed out."));
            else
                SheerResponse.Eval(this.GetCheckAuthCompletingJavaScript());
        }

        protected string GetCheckAuthCompletingJavaScript()
        {
            return "window.setTimeout(function() { window.scForm.postEvent('', '', '" + CheckAuthStatusMessageName + "'); }, 2000);";
        }

        protected void ShowAuthError(string message)
        {
            this.WaitPane.Visible = false;
            this.ErrorPane.Visible = true;
            this.ErrorText.Text = message;
        }
    }
}
