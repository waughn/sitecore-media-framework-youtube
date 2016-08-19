namespace Sitecore.SharedSource.MediaFramework.YouTube
{
    public static class Constants
    {
        public static readonly string IndexName = "mediaframework_youtube_index";
        public static readonly string SitecoreRestSharpService = "mediaframework_youtube";

        public static class Authentication
        {
            public static readonly string BaseUrl = "https://accounts.google.com/o/oauth2/auth";
            public static readonly string RedirectUri = "/sitecore/shell/Applications/MediaFramework/YouTube/Wizards/Authentication/Authenticate.aspx";
            public static readonly string ControlWizard = "control:YouTubeAuthenticationWizard";
        }

        public static class GoogleApiScope
        {
            public static readonly string YouTube = "https://www.googleapis.com/auth/youtube";
            public static readonly string YouTubeForceSsl = "https://www.googleapis.com/auth/youtube.force-ssl";
            public static readonly string YouTubeReadOnly = "https://www.googleapis.com/auth/youtube.readonly";
            public static readonly string YouTubeUpload = "https://www.googleapis.com/auth/youtube.upload";
            public static readonly string YouTubePartner = "https://www.googleapis.com/auth/youtubepartner";
        }

        public static class QueryString
        {
            public static readonly string StateKey = "stateKey";
            public static readonly string State = "state";
            public static readonly string Code = "code";
            public static readonly string Error = "error";

            public static readonly string Id = "id";
            public static readonly string Database = "db";
        }

        public static class ServerProperties
        {
            public static readonly string AuthStateKey = "authStateKey";
            public static readonly string AuthStartDateUtc = "authStartDateUtc";

            public static readonly string ClientId = "ClientId";
            public static readonly string ClientSecret = "ClientSecret";

            public static readonly string AccessToken = "AccessToken";
            public static readonly string TokenType = "TokenType";
            public static readonly string ExpiresIn = "ExpiresIn";
            public static readonly string RefreshToken = "RefreshToken";

            public static readonly string ScopeYouTube = "ScopeYouTube";
            public static readonly string ScopeYouTubeForceSsl = "ScopeYouTubeForceSsl";
            public static readonly string ScopeYouTubeReadOnly = "ScopeYouTubeReadOnly";
            public static readonly string ScopeYouTubeUpload = "ScopeYouTubeUpload";
            public static readonly string ScopeYouTubePartner = "ScopeYouTubePartner";
        }
    }
}
