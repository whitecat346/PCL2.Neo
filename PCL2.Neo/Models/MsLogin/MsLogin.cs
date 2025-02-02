using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Threading;
using PCL2.Neo.Models;

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
        //var client = new HttpClient();
        //string data =
        //    $"grant_type=urn:ietf:params:oauth:grant-type:device_code&client_id={clientId}&device_code={deviceCode}";
        //var content = new StringContent(data, Encoding.UTF8, "application/x-www-form-urlencoded");
        //var request = new HttpRequestMessage()
        //{
        //    Method = HttpMethod.Post,
        //    RequestUri = new Uri("https://login.microsoftonline.com/consumers/oauth2/v2.0/token"),
        //    Content = content
        //};

        //try
        //{
        //    HttpResponseMessage response = null;
        //    do
        //    {
        //        Thread.Sleep((interval - 1) * 1000);
        //        response = await client.PostAsync(request.RequestUri, request.Content);
        //    } while (!response.IsSuccessStatusCode);

        //    var responseContent =
        //        await JsonSerializer.DeserializeAsync<GetUserAuthorizationStatusData>(
        //            await response.Content.ReadAsStreamAsync());

        //    return responseContent;
        //}
        //catch (Exception e)
        //{
        //    if (e.Message.Contains("slow_down"))
        //    {
        //        Thread.Sleep(1000);
        //    }
        //    else if (e.Message.Contains("authorization_pending"))
        //    {
        //    }

        //    throw;
        //}
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
        //var client = new HttpClient();
        //var request = new HttpRequestMessage()
        //{
        //    Method = HttpMethod.Post,
        //    RequestUri = new Uri("https://user.auth.xboxlive.com/user/authenticate"),
        //    Content = new StringContent(
        //        // TODO: need add access token
        //        """{"Properties":{"AuthMethod":"RPS","SiteName":"user.auth.xboxlive.com","RpsTicket":"<access_token>"},"RelyingParty":"http://auth.xboxlive.com","TokenType":"JWT"}""",
        //        Encoding.UTF8, "application/json")
        //};

        //try
        //{
        //    var response = await client.PostAsync(request.RequestUri, request.Content);
        //    response.EnsureSuccessStatusCode();
        //    var responseContent =
        //        await JsonSerializer.DeserializeAsync<XboxAuthenticateData>(
        //            await response.Content.ReadAsStreamAsync());
        //    return responseContent;
        //}
        //catch (Exception e)
        //{
        //    throw;
        //}
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
        //var client = new HttpClient();
        //var request = new HttpRequestMessage()
        //{
        //    Method = HttpMethod.Post,
        //    RequestUri = new Uri("https://user.auth.xboxlive.com/user/authenticate"),
        //    Content = new StringContent(
        //        // TODO: need add xbl token
        //        """{"Properties":{"SandboxId":"RETAIL","UserTokens":["xbl_token"]},"RelyingParty":"rp://api.minecraftservices.com/","TokenType":"JWT"}""",
        //        Encoding.UTF8, "application/json")
        //};

        //try
        //{
        //    var response = await client.PostAsync(request.RequestUri, request.Content);
        //    response.EnsureSuccessStatusCode();
        //    var responseContent =
        //        await JsonSerializer.DeserializeAsync<XboxAuthenticateData>(
        //            await response.Content.ReadAsStreamAsync());
        //    return responseContent;
        //}
        //catch (Exception e)
        //{
        //    throw;
        //}

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
        //var client = new HttpClient();
        //var request = new HttpRequestMessage()
        //{
        //    Method = HttpMethod.Post,
        //    RequestUri = new Uri("https://api.minecraftservices.com/authentication/login_with_xbox"),
        //    Content = new StringContent(
        //        $"{{\"identityToken\":\"XBL3.0 x={uhs};{xstsToken}\"}}", Encoding.UTF8, "application/json")
        //};

        //try
        //{
        //    var response = await client.PostAsync(request.RequestUri, request.Content);
        //    response.EnsureSuccessStatusCode();
        //    var responseContent =
        //        await JsonSerializer.DeserializeAsync<MinecraftAccessTokenData>(
        //            await response.Content.ReadAsStreamAsync());
        //    return responseContent;
        //}
        //catch (Exception e)
        //{
        //    throw;
        //}

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
        //var client = new HttpClient();
        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer ", token);
        //try
        //{
        //    var response =
        //        await client.GetAsync(new Uri("https://api.minecraftservices.com/entitlements/mcstore"));
        //    response.EnsureSuccessStatusCode();
        //    var responseContent =
        //        await JsonSerializer.DeserializeAsync<IsGameExistData>(
        //            await response.Content.ReadAsStreamAsync());
        //    return responseContent;
        //}
        //catch (Exception e)
        //{
        //    throw;
        //}

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
        //var client = new HttpClient();
        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer ", token);
        //try
        //{
        //    var response =
        //        await client.GetAsync(new Uri("https://api.minecraftservices.com/minecraft/profile"));
        //    response.EnsureSuccessStatusCode();
        //    var responseContent =
        //        await JsonSerializer.DeserializeAsync<UserUuid>(
        //            await response.Content.ReadAsStreamAsync());
        //    return responseContent;
        //}
        //catch (Exception e)
        //{
        //    throw;
        //}

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