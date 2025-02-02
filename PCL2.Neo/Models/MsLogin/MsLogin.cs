using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Text.Json;
using System.Threading;

namespace PCL2.Neo.Models.MsLogin;

class MsLogin
{
    public static async Task<GetCodePairData> GetCodePair(string clientId)
    {
        try
        {
            var response = await Net.NetRequest(
                new Uri("https://login.microsoftonline.com/consumers/oauth2/v2.0/devicecode"), Net.NetMethod.Post,
                new Dictionary<string, string>()
                {
                    { "Content-Type: ", "application/json" }
                }, "client_id=clientid&scope=XboxLive.signin offline_access");

            return JsonSerializer.Deserialize<GetCodePairData>(response);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public static async Task<GetUserAuthorizationStatusData> GetUserAuthorizationStatus(string deviceCode,
        string clientId, int interval)
    {
        string response = null;
        do
        {
            try
            {
                response = await Net.NetRequest(
                    new Uri("https://login.microsoftonline.com/consumers/oauth2/v2.0/token"), Net.NetMethod.Post,
                    new Dictionary<string, string>()
                    {
                        { "Content-Type: ", "application/json" }
                    },
                    $"grant_type=urn:ietf:params:oauth:grant-type:device_code&client_id={clientId}&device_code={deviceCode}");
            }
            catch (Exception e)
            {
                if (e.Message.Contains("slow_down"))
                {
                    Thread.Sleep(1000);
                }
                else if (e.Message.Contains("authorization_pending"))
                {
                }
                else
                {
                    throw;
                }
            }
        } while (response is null);

        return JsonSerializer.Deserialize<GetUserAuthorizationStatusData>(response) ??
               throw new NoNullAllowedException();
    }

    public static async Task<XboxAuthenticateData> GetUserAuthorizationStatus(string accessToken)
    {
        try
        {
            var response = await Net.NetRequest(
                new Uri("https://user.auth.xboxlive.com/user/authenticate"), Net.NetMethod.Post,
                new Dictionary<string, string>()
                {
                    { "Content-Type: ", "application/json" }
                },
                $"{{\"Properties\":{{\"AuthMethod\":\"RPS\",\"SiteName\":\"user.auth.xboxlive.com\",\"RpsTicket\":\"{accessToken}\"}},\"RelyingParty\":\"http://auth.xboxlive.com\",\"TokenType\":\"JWT\"}}");

            return JsonSerializer.Deserialize<XboxAuthenticateData>(response);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public static async Task<XboxAuthenticateData> XstsAuthorize(string xblToken)
    {
        try
        {
            var response = await Net.NetRequest(
                new Uri("https://user.auth.xboxlive.com/user/authenticate"), Net.NetMethod.Post,
                new Dictionary<string, string>()
                {
                    { "Content-Type: ", "application/json" }
                },
                $"{{\"Properties\":{{\"SandboxId\":\"RETAIL\",\"UserTokens\":[\"{xblToken}\"]}},\"RelyingParty\":\"rp://api.minecraftservices.com/\",\"TokenType\":\"JWT\"}}");

            return JsonSerializer.Deserialize<XboxAuthenticateData>(response);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public static async Task<MinecraftAccessTokenData> GetMinecraftAccessToken(string uhs, string xstsToken)
    {
        try
        {
            var response = await Net.NetRequest(
                new Uri("https://api.minecraftservices.com/authentication/login_with_xbox"), Net.NetMethod.Post,
                new Dictionary<string, string>()
                {
                    { "Content-Type: ", "application/json" }
                }, $"{{\"identityToken\":\"XBL3.0 x={uhs};{xstsToken}\"}}");

            return JsonSerializer.Deserialize<MinecraftAccessTokenData>(response);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public static async Task<IsGameExistData> IsGameExist(string token)
    {
        try
        {
            var response = await Net.NetRequest(
                new Uri("https://api.minecraftservices.com/entitlements/mcstore"), Net.NetMethod.Get,
                new Dictionary<string, string>()
                {
                    { "Authorization: ", $"Bearer {token}" }
                });

            return JsonSerializer.Deserialize<IsGameExistData>(response);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public static async Task<UserUuid> GetUserUuid(string token)
    {
        try
        {
            var response = await Net.NetRequest(
                new Uri("https://api.minecraftservices.com/minecraft/profile"), Net.NetMethod.Get,
                new Dictionary<string, string>()
                {
                    { "Authorization: ", $"Bearer {token}" }
                });

            return JsonSerializer.Deserialize<UserUuid>(response);
        }
        catch (Exception e)
        {
            throw;
        }
    }
}