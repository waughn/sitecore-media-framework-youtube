using RestSharp;
using Sitecore.Collections;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Authentication
{
    public interface IAuthenticationExecuter<T>
    {
        IRestResponse<T> Authenticate(SafeDictionary<string> parameters);
    }
}