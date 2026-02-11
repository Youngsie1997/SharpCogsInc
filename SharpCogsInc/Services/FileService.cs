using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;
using SharpCogsInc.Models;

namespace SharpCogsInc.Services;

public static class FileService
{
    private static readonly string JsonFileName =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SharpCogsInc",
            "accounts.json");

    private static readonly string SettingsFileName =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SharpCogsInc",
            "settings.json");


    public static async Task SaveSettingsToDisk(LinuxSettings? settings)
    {
        if (settings is not null)
        {

            Directory.CreateDirectory(Path.GetDirectoryName(SettingsFileName)!);
            await using var fs = File.Create(SettingsFileName);
            await JsonSerializer.SerializeAsync(fs, settings);
        }
    }

    public static async Task SaveToFileAsync(ClashAccount accountToSave)
    {

        var accountsToSave = LoadFromFile();

        if (accountsToSave is { Count: > 0 })
        {
            accountsToSave.Add(accountToSave);

            Directory.CreateDirectory(Path.GetDirectoryName(JsonFileName)!);
            await using var fs = File.Create(JsonFileName);
            await JsonSerializer.SerializeAsync(fs, accountsToSave);
        }
        else
        {
            List<ClashAccount> singleAccountList =
            [
                accountToSave
            ];
            Directory.CreateDirectory(Path.GetDirectoryName(JsonFileName)!);
            await using var fs = File.Create(JsonFileName);
            await JsonSerializer.SerializeAsync(fs, singleAccountList);
        }
    }



    public static List<ClashAccount?>? LoadFromFile()
    {
        try
        {

            using var fs = File.OpenRead(JsonFileName);
            return JsonSerializer.Deserialize<List<ClashAccount>>(fs)!;
        }
        catch (Exception e) when (e is FileNotFoundException || e is DirectoryNotFoundException)
        {
            return null;
        }

    }

    public static LinuxSettings? LoadSettings()
    {
        try
        {
            using var fs = File.OpenRead(SettingsFileName);
            return JsonSerializer.Deserialize<LinuxSettings>(fs)!;
        }
        catch (Exception e) when (e is FileNotFoundException or DirectoryNotFoundException)
        {
            return null;
        }
    }

    
    

    
    
    
    
    
    
    
    
    
    
    
    

    
    

    
    

    
    
    

    
    
    
    
    
    
    


    
}

        
