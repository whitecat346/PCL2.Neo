using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCL2.Neo.Models.MsLogin
{
    public class GetCodePairData
    {
        public required string DeviceCode { get; set; }
        public required string UserCode { get; set; }
        public required string VerificationUri { get; set; }
        public required int ExpiresIn { get; set; }
        public required int Interval { get; set; }
        public required string Message { get; set; }
    }

    public class GetUserAuthorizationStatusData
    {
        public required string TokenType { get; set; }
        public required string Scope { get; set; }
        public required int ExpiresIn { get; set; }
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
        public required string IdToken { get; set; }
    }

    public class XboxAuthenticateXuiData
    {
        public required string Uhs { get; set; }
    }

    public class XboxAuthenticateDisplayClaimsData
    {
        public required IList<XboxAuthenticateXuiData> Xui { get; set; }
    }

    public class XboxAuthenticateData
    {
        public required string IssueInstant { get; set; }
        public required string NotAfter { get; set; }
        public required string Token { get; set; }
        public required XboxAuthenticateDisplayClaimsData DisplayClaims { get; set; }
    }

    public class MinecraftAccessTokenData
    {
        public required string UserName { get; set; }
        public required IList<string> Roles { get; set; }
        public required string AccessToken { get; set; }
        public required string TokenType { get; set; }
        public required int ExpiresIn { get; set; }
    }

    public class IsGameExistItemsData
    {
        public required string Name { get; set; }
        public required string Signature { get; set; }
    }

    public class IsGameExistData
    {
        public required IList<IsGameExistItemsData> Items { get; set; }
        public required string Signature { get; set; }
        public required string KeyId { get; set; }
    }

    public class SkinsItem
    {
        public required string Id { get; set; }

        public required string State { get; set; }

        public required string Url { get; set; }

        public required string Variant { get; set; }

        public required string Alias { get; set; }
    }

    public class UserUuid
    {
        public required string Id { get; set; }

        public required string Name { get; set; }

        public required List<SkinsItem> Skins { get; set; }

        public required List<string> Capes { get; set; }
    }
}
