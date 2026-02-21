using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SharpCogsInc.Models;

namespace SharpCogsInc.Services;

public class ApiService(HttpClient client)
{
    public async Task<string> GetDistrictAsync()
    {
        var response = await client.GetAsync("districts.js");
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<ClashManifest?> GetClashManifests(string token)
    {
        var windowsManifestRequest = new HttpRequestMessage(HttpMethod.Get,
            "https://corporateclash.net/api/v1/launcher/manifest/v3/production/windows");
        windowsManifestRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await client.SendAsync(windowsManifestRequest);
        response.EnsureSuccessStatusCode();
        var patchManifest = await response.Content.ReadFromJsonAsync<ClashManifest>();
        Debug.Assert(patchManifest != null, nameof(patchManifest) + " != null");
        foreach (var file in patchManifest.Files)
        {
            file.PlatformSpecific = true;
        }

        var resourcesRequest = new HttpRequestMessage(HttpMethod.Get,
            "https://corporateclash.net/api/v1/launcher/manifest/v3/production/resources");

        resourcesRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var resourcesResponse = await client.SendAsync(resourcesRequest);
        resourcesResponse.EnsureSuccessStatusCode();

        var resourceManifest = await resourcesResponse.Content.ReadFromJsonAsync<ClashManifest>();

        if (resourceManifest != null) patchManifest.Files.AddRange(resourceManifest.Files);
        return patchManifest;
    }


    public async Task<RegisterResponseBody?> RegisterClient(ClashRegisterDto registerDto)
    {
        try
        {
            using StringContent content = new(JsonSerializer.Serialize(registerDto), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://corporateclash.net/api/launcher/v1/register", content);
            return await response.Content.ReadFromJsonAsync<RegisterResponseBody>();

        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            throw;
        }
    }


    public async Task<ClashLoginResult> Login(string token)
    {
        var loginRequest = new HttpRequestMessage(HttpMethod.Post, "https://corporateclash.net/api/launcher/v1/login");
        loginRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var loginResponse = await client.SendAsync(loginRequest);

        loginResponse.EnsureSuccessStatusCode();
        return await loginResponse.Content.ReadFromJsonAsync<ClashLoginResult>() ??
               throw new InvalidOperationException();
    }
}