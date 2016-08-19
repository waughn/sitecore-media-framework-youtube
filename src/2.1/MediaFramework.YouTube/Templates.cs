using Sitecore.Data;

namespace Sitecore.SharedSource.MediaFramework.YouTube
{
    public class Templates
    {
        public static class Account
        {
            public static readonly ID TemplateID = new ID("{36979A85-ED0D-4748-B806-3AB29B505163}");

            // Application
            public static readonly ID ClientID = new ID("{8133E921-301D-45B6-B7F5-0ECF577C492F}");
            public static readonly ID ClientSecret = new ID("{664963F7-77DC-4007-B22A-CDF7609D2FBC}");

            // Authorization
            public static readonly ID AccessToken = new ID("{BAC62BD7-3100-4E67-8A05-0545FEA0D707}");
            public static readonly ID RefreshToken = new ID("{B44AACD5-5572-43DE-8B77-4F5C254E1C37}");
            public static readonly ID TokenType = new ID("{E0BC3525-9F05-4C3D-8B47-BE45EF8C2E0E}");
            public static readonly ID ExpiresIn = new ID("{6E3B6B77-C1D8-471A-89C6-AF0DD0239345}");
            public static readonly ID ExpirationDate = new ID("{1209848E-A1DD-4BDA-8742-F7A4D8F9B0D6}");

            // Scope
            public static readonly ID ScopeYouTube = new ID("{7ABCB1FC-5CEC-4856-8BE2-6305FFDCCF5C}");
            public static readonly ID ScopeYouTubeForceSsl = new ID("{4B3F9CB0-ADCA-467B-B91F-F36501F7110C}");
            public static readonly ID ScopeYouTubeReadOnly = new ID("{A5F118AD-98AB-49E2-A2D3-A0F43BD10196}");
            public static readonly ID ScopeYouTubeUpload = new ID("{D8003016-F263-4B53-A90D-F4662EE3AE77}");
            public static readonly ID ScopeYouTubePartner = new ID("{84702C12-3276-4089-8933-99298B87AFED}");
        }

        public static class AccountPublic
        {
            public static readonly ID TemplateID = new ID("{7A67AB0E-1000-467D-9D91-AC837F56EC1A}");

            public static readonly ID AccessKey = new ID("{D6A4F7E6-1C0B-4C3E-9440-27EFD363FCF2}");

            public static readonly ID VideoIDs = new ID("{FE5E919F-AA4D-4AB0-B03B-845B997086BE}");
            public static readonly ID PlaylistIDs = new ID("{08B22370-BB5A-4AC4-8309-4D8179927346}");
        }

        public static class AccountSettings
        {
            public static readonly ID TemplateID = new ID("{0FBC2224-0715-481F-9959-118F10B90948}");
        }

        public static class MediaElement
        {
            public static readonly ID TemplateID = new ID("{2F28989C-5028-472C-89AF-73E4E8BF1A93}");

            public static readonly ID Kind = new ID("{D17E424C-C0D8-46B2-AC06-1FCFA96F8029}");
            public static readonly ID UniqueID = new ID("{50D6953A-1FAD-42AB-A1AC-9DB3F3F9130B}");
            public static readonly ID ETag = new ID("{A89F86F3-C3A8-4A75-AB5E-36CDD35B20E5}");
            public static readonly ID PublishedAt = new ID("{7D86360B-0D10-4C4D-BAD5-490925B2316A}");
            public static readonly ID Title = new ID("{3EE7453D-BF4F-459A-8669-841BE68C7B1A}");
            public static readonly ID Description = new ID("{EE505512-378B-4A83-8C7A-E904E6E18320}");
            public static readonly ID TagsList = new ID("{865BD22C-D24B-4FF1-9B5D-1C640704621B}");
            public static readonly ID DefaultThumbnail = new ID("{6DB063BC-2CFB-40E1-9B70-DD44A63C85F7}");
            public static readonly ID MediumThumbnail = new ID("{ED61FEAF-D2FF-421F-A2BE-B4D289310794}");
            public static readonly ID HighThumbnail = new ID("{C7E39DD7-622A-46E1-A142-989A26D0BBE6}");
            public static readonly ID StandardThumbnail = new ID("{C7B56BB0-490A-4535-8397-DAAD0927230F}");
            public static readonly ID MaxResolutionThumbnail = new ID("{625E98C2-B1A1-4BCE-BE16-1A54AFF5EB9C}");
            public static readonly ID PrivacyStatus = new ID("{74207C8A-AA8C-4F91-BCCC-8A477639929A}");
        }

        public static class Channel
        {
            public static readonly ID TemplateID = new ID("{18B3281E-E27E-419F-9312-BE36BF3FB3EB}");

            public static readonly ID PlaylistList = new ID("{5772071A-1771-4B9E-A97C-6687A340DAB1}");
            public static readonly ID VideoList = new ID("{084474BB-C293-4D44-B288-46ACA75904EC}");
            public static readonly ID ViewCount = new ID("{5673F5BB-AEDF-484F-A021-85BD774E6805}");
            public static readonly ID CommentCount = new ID("{221E6EA2-0055-4EE1-9833-6361247E8E30}");
            public static readonly ID SubscriberCount = new ID("{6834AD17-D059-4D38-B12B-65A00DDFF291}");
            public static readonly ID HiddenSubscriberCount = new ID("{E782EF77-14F0-40A7-B00D-0E2F79D90099}");
            public static readonly ID VideoCount = new ID("{28939D29-3798-43C8-A75F-1CC07DDC8C4E}");
        }

        public static class Playlist
        {
            public static readonly ID TemplateID = new ID("{75DABEF6-6A24-4992-9887-5D027F9D73E7}");

            public static readonly ID VideoList = new ID("{5DF2FF2D-5D4F-4846-9A08-BD52AC805754}");
        }

        public static class Tag
        {
            public static readonly ID TemplateID = new ID("{77D9FD44-E388-4534-B37D-A8F9AB8504B6}");

            public static readonly ID Name = new ID("{9F3258E9-20BA-4036-ADF4-D55DBF4FE8BD}");
        }

        public static class Video
        {
            public static readonly ID TemplateID = new ID("{E2619092-0CE0-450E-B2AC-F3299F42ED4C}");

            public static readonly ID Duration = new ID("{2558D3A7-3412-4D14-81F4-945BEB99C79B}");
            public static readonly ID Dimension = new ID("{528AFBD7-A4EB-4A70-9C8A-A04BF4B768FD}");
            public static readonly ID Definition = new ID("{8D35BF59-CA45-47BD-A6BB-8F04D7A102C7}");
            public static readonly ID Caption = new ID("{A655DC63-44A0-4CA1-A69D-37E36BD73758}");
            public static readonly ID LicensedContent = new ID("{E34E4D07-27FE-48E9-ABD7-5DAE2C7D1C94}");
            public static readonly ID ViewCount = new ID("{6471DA5C-D9D1-414C-BF41-079D35A76E8C}");
            public static readonly ID LikeCount = new ID("{DB9DF541-7A7C-4A99-B717-928B6B0F1D1F}");
            public static readonly ID DislikeCount = new ID("{C74BECE0-54D1-41A9-A32D-FE606C863BC0}");
            public static readonly ID FavoriteCount = new ID("{257D7459-A956-41C1-ABEA-BA46F17CD90E}");
            public static readonly ID CommentCount = new ID("{8417616C-E3ED-4D65-BB96-3A1CC3891188}");
        }

        public static class Player
        {
            public static readonly ID TemplateID = new ID("{0F7FE2A5-57CD-489E-AB4A-DA7FD34D1561}");

            public static readonly ID Id = new ID("{ADA87927-71A6-4550-8004-590CC01645B4}");
            public static readonly ID Name = new ID("{0DA16DB8-0863-4867-AC94-A29D403C29B1}");
            public static readonly ID IsDefault = new ID("{CCDF0521-383A-4CEC-B0BC-DF923CF73258}");

            // Add additional parameters...
        }

        public static class Branches
        {
            public static class AccountBranch
            {
                public static readonly ID TemplateID = new ID("{C5F326B3-415C-480F-9934-8BEF2ABDF844}");
            }

            public static class AccountPublicBranch
            {
                public static readonly ID TemplateID = new ID("{7A67AB0E-1000-467D-9D91-AC837F56EC1A}");
            }
        }
    }
}
