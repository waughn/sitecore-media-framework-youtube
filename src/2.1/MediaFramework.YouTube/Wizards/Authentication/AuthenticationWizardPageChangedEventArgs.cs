using System;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Wizards.Authentication
{
    public class AuthenticationWizardPageChangedEventArgs : EventArgs
    {
        public string CurrentPage { get; private set; }

        public string PreviousPage { get; private set; }

        public AuthenticationWizardPageChangedEventArgs(string currentPage, string previousPage)
        {
            this.CurrentPage = currentPage;
            this.PreviousPage = previousPage;
        }
    }
}
