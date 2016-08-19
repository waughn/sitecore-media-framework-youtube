using System.Collections.Generic;
using RestSharp;
using Sitecore.Collections;
using Sitecore.RestSharp.Data;
using Sitecore.SharedSource.MediaFramework.YouTube.Entities;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Authentication
{
    public abstract class BaseAuthenticator : IAuthenticationExecuter<Token>
    {
        protected abstract string RequestName { get; }

        public IRestResponse<Token> Authenticate(SafeDictionary<string> parameters)
        {
            var context = new Sitecore.RestSharp.RestContext(Constants.SitecoreRestSharpService);

            var requestParameters = new List<Parameter>();
            foreach (var parameter in parameters)
            {
                var requestParameter = new Parameter
                {
                    Name = parameter.Key,
                    Value = parameter.Value,
                    Type = ParameterType.UrlSegment

                };

                requestParameters.Add(requestParameter);
            }

            return context.Create<RestEmptyType, Token>(this.RequestName, null, requestParameters);
        }
    }
}
