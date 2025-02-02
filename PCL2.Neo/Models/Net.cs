using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace PCL2.Neo.Models
{
    public class Net
    {
        public enum NetMethod
        {
            Get,
            Post
        }

        public static async Task<string> NetRequest(Uri uri, NetMethod method,
            Dictionary<string, string>? headers = null,
            string? data = null)
        {
            // create http client
            var client = new HttpClient();
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            try
            {
                switch (method)
                {
                    case NetMethod.Get:
                    {
                        var response = await client.GetAsync(uri);
                        response.EnsureSuccessStatusCode();
                        return await response.Content.ReadAsStringAsync();
                    }
                    case NetMethod.Post:
                    {
                        if (data == null)
                        {
                            throw new Exception("data is null");
                        }

                        var response = await client.PostAsync(uri, new StringContent(data));
                        response.EnsureSuccessStatusCode();
                        return await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return null;
        }
    }
}
