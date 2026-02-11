using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using SharpCogsInc.Models;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SharpCogsInc.Services;

public static class DownloadService
{



    public static  Task<string> MakeObjectKey(ClashFile file)
    {
       return  Task.Run(() =>
        {
            string platformString = GetPlatformString(file);
            var fixedPath = file.FilePath.Replace("\\", "/");
            return $"{platformString}/{fixedPath}.gz";

        });
    }

    private static  string GetPlatformString(ClashFile file)
    {
        if (file.FilePath.StartsWith("resources"))
        {
            return "resources";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ||  RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return "windows";
        }
        else
        {
            return "macos";
        }
    }
    
    
    public static async Task DownloadAndDecompressGzipAsync(string url, string outputFilePath)
    {
        using (var httpClient = new HttpClient())
        {
            // Set User-Agent
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("SharpCogsInc");
        
            string? directory = Path.GetDirectoryName(outputFilePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            using (HttpResponseMessage response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();
            
                // Get the compressed stream from HTTP
                await using (Stream compressedStream = await response.Content.ReadAsStreamAsync())
                    // Wrap it in GZipStream to decompress
                await using (GZipStream decompressionStream = new GZipStream(compressedStream, CompressionMode.Decompress))
                    // Write decompressed data to file
                await using (FileStream fileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await decompressionStream.CopyToAsync(fileStream);
                }
            }
        }
    }


    public static async Task<ClashFileHashResult> ValidateFile(ClashFile file, string filepath)
    {
        try
        {
            string? calculatedSha1 = await GetSha1(filepath);

            return new ClashFileHashResult
            {
                Sha1 = calculatedSha1,
                Message = "Validated File",
                Success = calculatedSha1 == file.Sha1,
                FilePath = filepath,
            };
        }
        catch (Exception e)
        {
            return new ClashFileHashResult
            {
                Success = false,
                Message = e.Message,
                Sha1 = null,
                FilePath = filepath,
            };
        }
    }
    
    
    
    private static async Task<string?> GetSha1(string filepath)
    {
                                                                                                           
    return await Task.Run(() =>
    {
        try
        {
            using var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read, 65535);
            return Convert.ToHexString(SHA1.HashData(fs)).ToLower();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
                                                                                                           
    });
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}