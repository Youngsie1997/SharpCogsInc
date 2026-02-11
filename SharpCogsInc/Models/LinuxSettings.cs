using System;
using System.IO;

namespace SharpCogsInc.Models;

public class LinuxSettings
{

    public LinuxSettings()
    {

        DownloadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "SharpCogsInc");

    }

    public LinuxSettings(string? tempStorage, string? downloadPath, string? wineBinary, string? winePrefix)
    {
        TempStorage = tempStorage;
        DownloadPath = downloadPath;
        WineBinary = wineBinary;
        WinePrefix = winePrefix;
    }

    public string? DownloadPath { get; set ; }
    public string? WineBinary { get; set; }
    public string? WinePrefix { get; set; }
    public string?  TempStorage { get; set; }
    

}