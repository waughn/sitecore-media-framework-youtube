using System;
using System.Collections.Specialized;
using System.Net;
using System.Web.UI;
using Sitecore.Collections;
using Sitecore.SharedSource.MediaFramework.YouTube.Authentication;
using Sitecore.Web;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Wizards.Authentication
{
    public class Authenticate : Page
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!string.IsNullOrEmpty(this.Request.QueryString[Constants.QueryString.StateKey]))
                this.RequestAccountCredentials(this.Request.QueryString[Constants.QueryString.StateKey]);
            else
                this.ReceiveAccountCredentials();
        }

        protected virtual void RequestAccountCredentials(string wizardStateKey)
        {
            var index = string.Format("WizardAuthenticationParameters_{0}", wizardStateKey);

            if (this.Session[index] is SafeDictionary<string>)
            {
                var parameters = this.Session[index] as SafeDictionary<string>;
                parameters.Add("redirect_uri", WebUtil.GetFullUrl(Constants.Authentication.RedirectUri));

                var uriBuilder = new UriBuilder(Constants.Authentication.BaseUrl)
                {
                    Query = WebUtil.BuildQueryString(parameters, false, false)
                };

                this.Response.Redirect(uriBuilder.ToString(), false);
                this.Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                this.CloseWindow();
            }
        }

        protected virtual void ReceiveAccountCredentials()
        {
            var wizardStateKey = this.Request.QueryString[Constants.QueryString.State];
            var index = string.Format("WizardTokenParameters_{0}", wizardStateKey);
            var tokenindex = string.Format("WizardAuthenticationToken_{0}", wizardStateKey);

            if (!string.IsNullOrEmpty(this.Request.QueryString[Constants.QueryString.Error]))
            {
                var collection = new NameValueCollection { { "ErrorMessage", this.Request.QueryString[Constants.QueryString.Error] } };
                this.Session[tokenindex] = collection;
            }
            else if (!string.IsNullOrEmpty(this.Request.QueryString[Constants.QueryString.Code]))
            {
                if (this.Session[index] is SafeDictionary<string>)
                {
                    var parameters = this.Session[index] as SafeDictionary<string>;
                    parameters.Add("code", this.Request.QueryString[Constants.QueryString.Code]);
                    parameters.Add("redirect_uri", WebUtil.GetFullUrl(Constants.Authentication.RedirectUri));

                    var authenticator = new TokenAuthenticator();
                    var response = authenticator.Authenticate(parameters);

                    if (response != null && response.StatusCode == HttpStatusCode.OK)
                    {
                        var token = response.Data;
                        this.Session[tokenindex] = token;
                    }
                    else
                    {
                        var message = response == null ? "Unauthorized" : response.StatusDescription;
                        var collection = new NameValueCollection { { "ErrorMessage", message } };
                        this.Session[tokenindex] = collection;
                    }
                }
            }

            this.CloseWindow();
        }

        protected virtual void CloseWindow()
        {
            if (this.Page.ClientScript.IsStartupScriptRegistered(this.GetType(), "CloseAddAccountWindow"))
                return;
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "CloseAddAccountWindow", "window.top.close();", true);
        }
    }
}
