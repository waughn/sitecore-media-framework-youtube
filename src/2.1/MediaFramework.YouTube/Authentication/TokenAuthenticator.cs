namespace Sitecore.SharedSource.MediaFramework.YouTube.Authentication
{
    public class TokenAuthenticator : BaseAuthenticator
    {
        protected override string RequestName
        {
            get
            {
                return "authenticate_token";
            }
        }
    }
}
