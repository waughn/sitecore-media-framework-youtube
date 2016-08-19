using System;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Wizards.Authentication
{
    public class AuthenticationWizardPageChangingEventArgs : EventArgs
    {
        public string CurrentPage { get; private set; }

        public string NewPage { get; set; }

        public bool Cancel { get; set; }

        public AuthenticationWizardPageChangingEventArgs(string currentPage, string newPage)
        {
            this.CurrentPage = currentPage;
            this.NewPage = newPage;
            this.Cancel = false;
        }
    }
}
