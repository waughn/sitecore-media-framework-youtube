using Newtonsoft.Json;

namespace Sitecore.SharedSource.MediaFramework.YouTube.Entities
{
    public class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("refresh_token", NullValueHandling = NullValueHandling.Ignore)]
        public string RefreshToken { get; set; }
    }
}
