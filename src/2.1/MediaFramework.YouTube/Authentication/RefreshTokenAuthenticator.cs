namespace Sitecore.SharedSource.MediaFramework.YouTube.Authentication
{
    public class RefreshTokenAuthenticator : BaseAuthenticator
    {
        protected override string RequestName
        {
            get
            {
                return "authenticate_refreshtoken";
            }
        }
    }
}
