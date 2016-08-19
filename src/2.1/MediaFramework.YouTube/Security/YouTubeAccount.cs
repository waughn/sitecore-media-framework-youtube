using System;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Security
{
    public class YouTubeAccount
    {
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; set; }
        public string ExpiresIn { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
