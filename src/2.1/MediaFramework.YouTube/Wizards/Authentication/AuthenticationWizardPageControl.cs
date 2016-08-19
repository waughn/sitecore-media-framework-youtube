using System.Web.UI;
using Sitecore.Reflection;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.XmlControls;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Wizards.Authentication
{
    public abstract class AuthenticationWizardPageControl : XmlControl, IMessageHandler
    {
        private WizardDialogBaseXmlControl _wizardPageControl;

        public virtual AuthenticationWizardForm WizardForm
        {
            get
            {
                return Sitecore.Context.ClientPage.CodeBeside as AuthenticationWizardForm;
            }
        }

        public virtual string WizardPageId
        {
            get
            {
                return this.WizardPageControl.ID;
            }
        }

        public WizardDialogBaseXmlControl WizardPageControl
        {
            get
            {
                if (_wizardPageControl == null)
                {
                    Control parent = this.Parent;
                    while (parent != null && !(parent is WizardDialogBaseXmlControl))
                        parent = parent.Parent;
                    _wizardPageControl = parent as WizardDialogBaseXmlControl ?? new WizardDialogBaseXmlControl();
                }

                return _wizardPageControl;
            }
        }

        public string WizardPageHeader
        {
            get
            {
                return (string)ReflectionUtil.GetProperty(this.WizardPageControl, "Header");
            }
            set
            {
                ReflectionUtil.SetProperty(this.WizardPageControl, "Header", value);
            }
        }

        public string WizardPageHeaderText
        {
            get
            {
                return (string)ReflectionUtil.GetProperty(this.WizardPageControl, "Text");
            }
            set
            {
                ReflectionUtil.SetProperty(this.WizardPageControl, "Text", value);
            }
        }
    }
}
