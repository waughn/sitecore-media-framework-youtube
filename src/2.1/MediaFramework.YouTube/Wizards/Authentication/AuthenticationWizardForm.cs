using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.MediaFramework.Account;
using Sitecore.SharedSource.MediaFramework.YouTube.Wizards.Authentication.PageControls;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Wizards.Authentication
{
    public class AuthenticationWizardForm : WizardForm
    {
        protected WizardDialogBaseXmlControl WelcomePage;
        protected WelcomePageControl WelcomePageContent;
        protected WizardDialogBaseXmlControl WebApplicationClientIdPage;
        protected WebApplicationClientIdPageControl WebApplicationClientIdPageContent;
        protected WizardDialogBaseXmlControl DefineScopesPage;
        protected DefineScopesPageControl DefineScopesPageContent;
        protected WizardDialogBaseXmlControl WaitForAuthPage;
        protected WaitForAuthPageControl WaitForAuthPageContent;
        protected WizardDialogBaseXmlControl GoodByePage;
        protected GoodByePageControl GoodByePageContent;

        public Button ButtonNext
        {
            get
            {
                return this.NextButton;
            }
        }

        public Button ButtonCancel
        {
            get
            {
                return this.CancelButton;
            }
        }

        public Button ButtonBack
        {
            get
            {
                return this.BackButton;
            }
        }

        private Item _accountItem;

        public Item AccountItem
        {
            get
            {
                if (_accountItem == null)
                    _accountItem = this.GetAccountItem();

                return _accountItem;
            }
        }

        public event EventHandler<AuthenticationWizardPageChangedEventArgs> WizardPageChanged;

        public event EventHandler<AuthenticationWizardPageChangingEventArgs> WizardPageChanging;

        public void WizardEnd()
        {
            this.EndWizard();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (Context.ClientPage.IsPostBack || Context.ClientPage.IsEvent)
                return;

            this.WizardPageChanging(this, new AuthenticationWizardPageChangingEventArgs(this.Active, this.Active));
        }

        protected override void ActivePageChanged(string page, string oldPage)
        {
            base.ActivePageChanged(page, oldPage);

            if (this.WizardPageChanged == null)
                return;

            this.WizardPageChanged(this, new AuthenticationWizardPageChangedEventArgs(page, oldPage));
        }

        protected override bool ActivePageChanging(string page, ref string newpage)
        {
            if (this.WizardPageChanging == null)
                return base.ActivePageChanging(page, ref newpage);

            var e = new AuthenticationWizardPageChangingEventArgs(page, newpage);
            this.WizardPageChanging(this, e);

            if (string.Compare(e.NewPage, newpage, StringComparison.InvariantCulture) != 0)
                newpage = e.NewPage;

            return !e.Cancel;
        }

        protected virtual Item GetAccountItem()
        {
            Item item = null;

            var id = HttpContext.Current.Request.QueryString[Constants.QueryString.Id];
            var db = HttpContext.Current.Request.QueryString[Constants.QueryString.Database];

            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(db))
            {
                var database = Factory.GetDatabase(db);
                var ids = new List<ID> { new ID(id) };

                item = AccountManager.GetAccountsByIds(database, ids).FirstOrDefault();
            }

            return item;
        }
    }
}
